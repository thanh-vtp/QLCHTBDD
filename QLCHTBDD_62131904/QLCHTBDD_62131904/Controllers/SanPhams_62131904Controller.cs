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

namespace QLCHTBDD_62131904.Controllers
{
    public class SanPhams_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: SanPhams_62131904
        public ActionResult Index()
        {
            var rawProducts = db.BienTheSanPhams
        .Include(b => b.SanPham)
        .Include(b => b.SanPham.LoaiSP)
        .Include(b => b.HinhAnhSanPhams)
        .Select(b => new
        {
            TenSanPham = b.SanPham.TenSP,
            MoTa = b.SanPham.MoTa,
            LoaiSanPham = b.SanPham.LoaiSP.TenLSP,
            AnhSanPham = b.HinhAnhSanPhams.Select(h => h.AnhSP).ToList(), // Chỉ lấy danh sách URL ảnh
            SoLuong = b.SoLuong,
            DonGia = b.DonGia,
            DonViTinh = b.DonViTinh
        }).ToList(); // Chuyển dữ liệu sang bộ nhớ

            // Xử lý danh sách ảnh trong bộ nhớ
            var products = rawProducts.Select(p => new
            {
                p.TenSanPham,
                p.MoTa,
                p.LoaiSanPham,
                AnhSanPhamHtml = string.Join("", p.AnhSanPham.Select(img => $"<img src='{img}' alt='Ảnh sản phẩm' width='50' style='margin: 0 5px;' />")),
                p.SoLuong,
                p.DonGia,
                p.DonViTinh
            }).ToList();

            return View(products);
        }

        // GET: SanPhams_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLSP");

            // Load danh sách Màu sắc và ROM để người dùng có thể chọn cho biến thể sản phẩm
            ViewBag.MaMau = new SelectList(db.MauSacs, "MaMau", "TenMau");
            ViewBag.MaROM = new SelectList(db.ROMs, "MaROM", "DungLuong");

            return View();
        }

        // POST: SanPhams_62131904/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "TenSP,MoTa,MaLSP")] SanPham sanPham,
            int maMau,
            int maRom,
            int soLuong,
            string donViTinh,
            int donGia,
            HttpPostedFileBase[] hinhAnh,
            string manHinh,
            string camera,
            string pin,
            string chipset,
            string ram,
            string heDieuHanh
            )
        {
            if (ModelState.IsValid)
            {
                // Thêm sản phẩm vào bảng SanPham
                db.SanPhams.Add(sanPham);
                db.SaveChanges();

                // Tạo biến thể sản phẩm
                BienTheSanPham bienThe = new BienTheSanPham
                {
                    MaSP = sanPham.MaSP,
                    MaMau = maMau,
                    MaROM = maRom,
                    SoLuong = soLuong,
                    DonGia = donGia,
                    DonViTinh = donViTinh
                };

                db.BienTheSanPhams.Add(bienThe);
                db.SaveChanges();

                // Thêm hình ảnh vào bảng HinhAnhSanPham
                if (hinhAnh != null)
                {
                    foreach (var file in hinhAnh)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var filePath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                            file.SaveAs(filePath);

                            HinhAnhSanPham hinhAnhSP = new HinhAnhSanPham
                            {
                                MaBienThe = bienThe.MaBienThe,
                                AnhSP = "~/Images/" + Path.GetFileName(file.FileName)
                            };

                            db.HinhAnhSanPhams.Add(hinhAnhSP);
                        }
                    }
                    db.SaveChanges();
                }

                // Thêm thông số kỹ thuật vào bảng ThongSoKiThuat
                ThongSoKiThuat thongSoKiThuat = new ThongSoKiThuat
                {
                    MaBienThe = bienThe.MaBienThe,
                    ManHinh = manHinh,
                    Camera = camera,
                    Pin = pin,
                    Chipset = chipset,
                    RAM = ram,
                    HeDieuHanh = heDieuHanh,
                };

                db.ThongSoKiThuats.Add(thongSoKiThuat);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Nếu có lỗi, hiển thị lại dropdowns cho người dùng
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLSP", sanPham.MaLSP);
            ViewBag.MaMau = new SelectList(db.MauSacs, "MaMau", "TenMau", maMau);
            ViewBag.MaROM = new SelectList(db.ROMs, "MaROM", "DungLuong", maRom);

            return View(sanPham);
        }

        // GET: SanPhams_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLSP", sanPham.MaLSP);
            return View(sanPham);
        }

        // POST: SanPhams_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,MoTa,MaLSP")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLSP", sanPham.MaLSP);
            return View(sanPham);
        }

        // GET: SanPhams_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
