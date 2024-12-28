using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Models;

namespace QLCHTBDD_62131904.Controllers.SanPhams
{
    public class HinhAnhSanPhams_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: HinhAnhSanPhams_62131904
        public ActionResult Index()
        {
            var hinhAnhSanPhams = db.HinhAnhSanPhams.Include(h => h.BienTheSanPham).Include(h => h.BienTheSanPham.SanPham);
            return View(hinhAnhSanPhams.ToList());
        }

        // GET: HinhAnhSanPhams_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnhSanPham hinhAnhSanPham = db.HinhAnhSanPhams.Find(id);
            if (hinhAnhSanPham == null)
            {
                return HttpNotFound();
            }
            return View(hinhAnhSanPham);
        }

        // GET: HinhAnhSanPhams_62131904/Create
        public ActionResult Create()
        {
            var bienTheSanPhams = db.BienTheSanPhams
                .Include(bt => bt.SanPham)
                .Include(bt => bt.MauSac) 
                .Include(bt => bt.RAM)    
                .Include(bt => bt.ROM)   
                .Select(bt => new
                {
                    MaBT = bt.MaBT,
                    DisplayText = bt.SanPham.TenSP + " - Màu: " + bt.MauSac.TenMau +
                                  "/ RAM: " + bt.RAM.DungLuong +
                                  "/ ROM: " + bt.ROM.DungLuong +
                                  "/ SKU: (" + bt.SKU + ")"
                })
                .ToList();

            ViewBag.MaBT = new SelectList(bienTheSanPhams, "MaBT", "DisplayText");
            return View();
        }

        // POST: HinhAnhSanPhams_62131904/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int MaBT, List<string> TenAnh, List<HttpPostedFileBase> DuongDanAnh, string AnhChinh)
        {
            if (DuongDanAnh != null && TenAnh != null && TenAnh.Count == DuongDanAnh.Count)
            {
                // Kiểm tra trùng lặp tên ảnh
                if (TenAnh.Distinct().Count() != TenAnh.Count)
                {
                    ModelState.AddModelError("", "Tên ảnh không được trùng lặp.");
                }

                // Kiểm tra số lượng ảnh vượt quá giới hạn
                if (TenAnh.Count > 5)
                {
                    ModelState.AddModelError("", "Số lượng ảnh không được vượt quá 5.");
                }

                if (!ModelState.IsValid)
                {
                    return View();
                }

                for (int i = 0; i < DuongDanAnh.Count; i++)
                {
                    var file = DuongDanAnh[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        // Tạo đường dẫn lưu ảnh
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images"), fileName);

                        // Lưu file vào thư mục Images
                        file.SaveAs(path);

                        // Kiểm tra ảnh chính
                        bool isMainImage = AnhChinh == TenAnh[i]; // Nếu tên ảnh khớp với ảnh chính

                        // Lưu thông tin vào cơ sở dữ liệu
                        var hinhAnh = new HinhAnhSanPham
                        {
                            MaBT = MaBT,
                            TenAnh = TenAnh[i],
                            DuongDanAnh = $"/Images/{fileName}", // Đường dẫn được lưu trong DB
                            AnhChinh = isMainImage == true ? true : false
                        };

                        db.HinhAnhSanPhams.Add(hinhAnh);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Nếu có lỗi, trả lại form với dữ liệu đã nhập
            var bienTheSanPhams = db.BienTheSanPhams
                .Include(bt => bt.SanPham)
                .Include(bt => bt.MauSac)
                .Include(bt => bt.RAM)
                .Include(bt => bt.ROM)
                .Select(bt => new
                {
                    MaBT = bt.MaBT,
                    DisplayText = bt.SanPham.TenSP + " - Màu: " + bt.MauSac.TenMau +
                                  "/ RAM: " + bt.RAM.DungLuong +
                                  "/ ROM: " + bt.ROM.DungLuong +
                                  "/ SKU: (" + bt.SKU + ")"
                })
                .ToList();

            ViewBag.MaBT = new SelectList(bienTheSanPhams, "MaBT", "DisplayText", MaBT);
            return View();
        }

        // GET: HinhAnhSanPhams_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnhSanPham hinhAnhSanPham = db.HinhAnhSanPhams.Find(id);
            if (hinhAnhSanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBT = new SelectList(db.BienTheSanPhams, "MaBT", "DonViTinh", hinhAnhSanPham.MaBT);
            return View(hinhAnhSanPham);
        }

        // POST: HinhAnhSanPhams_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHA,MaBT,DuongDanAnh,TenAnh,AnhChinh")] HinhAnhSanPham hinhAnhSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hinhAnhSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBT = new SelectList(db.BienTheSanPhams, "MaBT", "DonViTinh", hinhAnhSanPham.MaBT);
            return View(hinhAnhSanPham);
        }

        // GET: HinhAnhSanPhams_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnhSanPham hinhAnhSanPham = db.HinhAnhSanPhams.Find(id);
            if (hinhAnhSanPham == null)
            {
                return HttpNotFound();
            }
            return View(hinhAnhSanPham);
        }

        // POST: HinhAnhSanPhams_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HinhAnhSanPham hinhAnhSanPham = db.HinhAnhSanPhams.Find(id);
            db.HinhAnhSanPhams.Remove(hinhAnhSanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
