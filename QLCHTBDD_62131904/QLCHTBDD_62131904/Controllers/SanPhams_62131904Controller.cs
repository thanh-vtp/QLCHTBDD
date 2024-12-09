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
using QLCHTBDD_62131904.ViewModels;

namespace QLCHTBDD_62131904.Controllers
{
    public class SanPhams_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        public List<SanPhamViewModel> dsSanPham()
        {
            var rawProducts = db.BienTheSanPhams
        .Include(b => b.SanPham)
        .Include(b => b.SanPham.LoaiSP)
        .Include(b => b.HinhAnhSanPhams)
        .Select(b => new
        {
            MaSP = b.SanPham.MaSP,
            TenSanPham = b.SanPham.TenSP,
            MoTa = b.SanPham.MoTa,
            LoaiSanPham = b.SanPham.LoaiSP.TenLSP,
            AnhSanPham = b.HinhAnhSanPhams.Select(h => h.AnhSP).ToList(),
            SoLuong = b.SoLuong,
            DonGia = b.DonGia,
            DonViTinh = b.DonViTinh
        }).ToList(); // Lấy dữ liệu vào bộ nhớ

            // Chuyển đổi sang ViewModel
            return rawProducts.Select(p => new SanPhamViewModel
            {
                MaSP = p.MaSP,
                TenSP = p.TenSanPham,
                MoTa = p.MoTa,
                LoaiSP = p.LoaiSanPham,
                AnhSP = string.Join("", p.AnhSanPham.Select(img => $"<img src='{img}' alt='Ảnh sản phẩm' class='table-img' />")),
                SoLuong = p.SoLuong,
                DonGia = p.DonGia,
                DonViTinh = p.DonViTinh
            }).ToList();
        }

        // GET: SanPhams_62131904
        public ActionResult Index()
        {
            var products = dsSanPham();
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
                            var filePath = Path.Combine(Server.MapPath("/Images"), Path.GetFileName(file.FileName));
                            file.SaveAs(filePath);

                            HinhAnhSanPham hinhAnhSP = new HinhAnhSanPham
                            {
                                MaBienThe = bienThe.MaBienThe,
                                AnhSP = "/Images/" + Path.GetFileName(file.FileName)
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

            // Tìm sản phẩm theo ID
            var sanPham = db.SanPhams.Find(id);
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
            try
            {
                // Lấy sản phẩm cần xóa
                var sanPham = db.SanPhams.Find(id);

                if (sanPham == null)
                {
                    TempData["ErrorMessage"] = "Sản phẩm không tồn tại!";
                    return RedirectToAction("Index");
                }

                // Xóa các hình ảnh liên quan trong bảng HinhAnhSanPham
                var relatedImages = db.HinhAnhSanPhams.Where(image => image.BienTheSanPham.MaSP == id).ToList();
                foreach (var image in relatedImages)
                {
                    db.HinhAnhSanPhams.Remove(image);
                }

                // Xóa các ràng buộc khác (nếu có) ở bảng liên quan trước khi xóa sản phẩm
                var relatedVariants = db.BienTheSanPhams.Where(bienThe => bienThe.MaSP == id).ToList();
                foreach (var variant in relatedVariants)
                {
                    db.BienTheSanPhams.Remove(variant);
                }

                // Xóa sản phẩm
                db.SanPhams.Remove(sanPham);
                db.SaveChanges();

                // Thông báo thành công
                TempData["SuccessMessage"] = "Xóa sản phẩm thành công!";
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có vấn đề xảy ra trong quá trình xóa
                TempData["ErrorMessage"] = "Không thể xóa sản phẩm. Vui lòng kiểm tra lại!";
                // Log lỗi (tuỳ chọn)
                Console.WriteLine(ex.Message);
            }

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
