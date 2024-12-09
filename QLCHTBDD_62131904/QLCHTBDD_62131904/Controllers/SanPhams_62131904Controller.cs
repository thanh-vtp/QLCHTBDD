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
            return db.BienTheSanPhams
                .Include(b => b.SanPham)
                .Include(b => b.SanPham.LoaiSP)
                .Include(b => b.HinhAnhSanPhams)
                .Select(b => new SanPhamViewModel
                {
                    MaSP = b.SanPham.MaSP,
                    TenSP = b.SanPham.TenSP,
                    MoTa = b.SanPham.MoTa,
                    LoaiSP = b.SanPham.LoaiSP.TenLSP,
                    AnhSP = b.HinhAnhSanPhams.Select(h => h.AnhSP).ToList(),
                    SoLuong = b.SoLuong,
                    DonGia = b.DonGia,
                    DonViTinh = b.DonViTinh
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
            ViewBag.MaMau = new SelectList(db.MauSacs, "MaMau", "TenMau");
            ViewBag.MaROM = new SelectList(db.ROMs, "MaROM", "DungLuong");
            return View();
        }

        // POST: SanPhams_62131904/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "TenSP,MoTa,MaLSP")] SanPham product,
            int colorId,
            int romId,
            int quantity,
            string unitOfMeasure,
            int price,
            HttpPostedFileBase[] uploadedImages,
            string screen,
            string camera,
            string battery,
            string chipset,
            string ram,
            string operatingSystem
        )
        {
            if (ModelState.IsValid)
            {
                // 1. Add Product
                db.SanPhams.Add(product);
                db.SaveChanges();

                // 2. Add Product Variant
                var variant = new BienTheSanPham
                {
                    MaSP = product.MaSP,
                    MaMau = colorId,
                    MaROM = romId,
                    SoLuong = quantity,
                    DonGia = price,
                    DonViTinh = unitOfMeasure
                };
                db.BienTheSanPhams.Add(variant);
                db.SaveChanges();

                // 3. Add Product Images
                if (uploadedImages != null)
                {
                    foreach (var image in uploadedImages)
                    {
                        if (image != null && image.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(image.FileName);
                            var filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                            image.SaveAs(filePath);

                            var productImage = new HinhAnhSanPham
                            {
                                MaBienThe = variant.MaBienThe,
                                AnhSP = "/Images/" + fileName
                            };

                            db.HinhAnhSanPhams.Add(productImage);
                        }
                    }
                    db.SaveChanges();
                }

                // 4. Add Technical Specifications
                var technicalSpecs = new ThongSoKiThuat
                {
                    MaBienThe = variant.MaBienThe,
                    ManHinh = screen,
                    Camera = camera,
                    Pin = battery,
                    Chipset = chipset,
                    RAM = ram,
                    HeDieuHanh = operatingSystem
                };
                db.ThongSoKiThuats.Add(technicalSpecs);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Reload Dropdowns if Validation Fails

            return View(product);
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
