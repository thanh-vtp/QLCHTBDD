using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Models;

namespace QLCHTBDD_62131904.Controllers.QuanLySanPhams.Cameras
{
    public class ChiTietTinhNangCameras_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: ChiTietTinhNangCameras_62131904
        public ActionResult Index()
        {
            var chiTietTinhNangCameras = db.ChiTietTinhNangCameras.Include(c => c.LoaiCamera).Include(c => c.TinhNang).Include(c => c.ThongSoBienTheDienThoai)
                .Include(c => c.ThongSoBienTheDienThoai.BienTheSanPham.SanPham);
            return View(chiTietTinhNangCameras.ToList());
        }

        // GET: ChiTietTinhNangCameras_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietTinhNangCamera chiTietTinhNangCamera = db.ChiTietTinhNangCameras.Find(id);
            if (chiTietTinhNangCamera == null)
            {
                return HttpNotFound();
            }
            return View(chiTietTinhNangCamera);
        }

        // GET: ChiTietTinhNangCameras_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiCamera = new SelectList(db.LoaiCameras, "MaLoaiCamera", "TenLoaiCamera");
            ViewBag.MaTinhNang = new SelectList(db.TinhNangs, "MaTinhNang", "TenTinhNang");

            var thongSoBienThe = db.ThongSoBienTheDienThoais
            .Include(x => x.BienTheSanPham)
            .Include(x => x.BienTheSanPham.SanPham)
            .Select(x => new
            {
                MaTSBTDT = x.MaTSBTDT,
                DisplayText = x.BienTheSanPham.SanPham.TenSP + " - Màu: " + x.BienTheSanPham.MauSac.TenMau +
                "/ RAM: " + x.BienTheSanPham.RAM.DungLuong +
                "/ ROM: " + x.BienTheSanPham.ROM.DungLuong +
                "/ SKU: (" + x.BienTheSanPham.SKU + ")"

            })
            .ToList();
            ViewBag.MaTSBTDT = new SelectList(thongSoBienThe, "MaTSBTDT", "DisplayText");
            return View();
        }

        // POST: ChiTietTinhNangCameras_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChiTietTinhNangCamera,MaTSBTDT,MaLoaiCamera,MaTinhNang")] ChiTietTinhNangCamera chiTietTinhNangCamera)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check for duplicates
                    bool isDuplicate = db.ChiTietTinhNangCameras.Any(ct =>
                        ct.MaTSBTDT == chiTietTinhNangCamera.MaTSBTDT &&
                        ct.MaLoaiCamera == chiTietTinhNangCamera.MaLoaiCamera &&
                        ct.MaTinhNang == chiTietTinhNangCamera.MaTinhNang);

                    if (isDuplicate)
                    {
                        ModelState.AddModelError("", "Chi tiết tính năng camera đã tồn tại. Vui lòng kiểm tra lại.");
                    }
                    else
                    {
                        // Add and save the new entry
                        db.ChiTietTinhNangCameras.Add(chiTietTinhNangCamera);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                    ModelState.AddModelError("", "Đã xảy ra lỗi trong quá trình thêm chi tiết tính năng camera.");
                }
            }

            // Repopulate ViewBag for dropdowns
            ViewBag.MaLoaiCamera = new SelectList(db.LoaiCameras, "MaLoaiCamera", "TenLoaiCamera", chiTietTinhNangCamera.MaLoaiCamera);
            ViewBag.MaTinhNang = new SelectList(db.TinhNangs, "MaTinhNang", "TenTinhNang", chiTietTinhNangCamera.MaTinhNang);

            var thongSoBienThe = db.ThongSoBienTheDienThoais
                .Include(x => x.BienTheSanPham)
                .Include(x => x.BienTheSanPham.SanPham)
                .Select(x => new
                {
                    MaTSBTDT = x.MaTSBTDT,
                    DisplayText = x.BienTheSanPham.SanPham.TenSP + " - Màu: " + x.BienTheSanPham.MauSac.TenMau +
                                  "/ RAM: " + x.BienTheSanPham.RAM.DungLuong +
                                  "/ ROM: " + x.BienTheSanPham.ROM.DungLuong +
                                  "/ SKU: (" + x.BienTheSanPham.SKU + ")"
                })
                .ToList();

            ViewBag.MaTSBTDT = new SelectList(thongSoBienThe, "MaTSBTDT", "DisplayText", chiTietTinhNangCamera.MaTSBTDT);

            return View(chiTietTinhNangCamera);
        }

        // GET: ChiTietTinhNangCameras_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietTinhNangCamera chiTietTinhNangCamera = db.ChiTietTinhNangCameras.Find(id);
            if (chiTietTinhNangCamera == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiCamera = new SelectList(db.LoaiCameras, "MaLoaiCamera", "TenLoaiCamera", chiTietTinhNangCamera.MaLoaiCamera);
            ViewBag.MaTinhNang = new SelectList(db.TinhNangs, "MaTinhNang", "TenTinhNang", chiTietTinhNangCamera.MaTinhNang);
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", chiTietTinhNangCamera.MaTSBTDT);
            return View(chiTietTinhNangCamera);
        }

        // POST: ChiTietTinhNangCameras_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChiTietTinhNangCamera,MaTSBTDT,MaLoaiCamera,MaTinhNang")] ChiTietTinhNangCamera chiTietTinhNangCamera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietTinhNangCamera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiCamera = new SelectList(db.LoaiCameras, "MaLoaiCamera", "TenLoaiCamera", chiTietTinhNangCamera.MaLoaiCamera);
            ViewBag.MaTinhNang = new SelectList(db.TinhNangs, "MaTinhNang", "TenTinhNang", chiTietTinhNangCamera.MaTinhNang);
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", chiTietTinhNangCamera.MaTSBTDT);
            return View(chiTietTinhNangCamera);
        }

        // GET: ChiTietTinhNangCameras_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietTinhNangCamera chiTietTinhNangCamera = db.ChiTietTinhNangCameras.Find(id);
            if (chiTietTinhNangCamera == null)
            {
                return HttpNotFound();
            }
            return View(chiTietTinhNangCamera);
        }

        // POST: ChiTietTinhNangCameras_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietTinhNangCamera chiTietTinhNangCamera = db.ChiTietTinhNangCameras.Find(id);
            db.ChiTietTinhNangCameras.Remove(chiTietTinhNangCamera);
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
