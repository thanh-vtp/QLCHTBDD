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
        private readonly QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: SanPhams_62131904
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.LoaiSP);
            return View(sanPhams.ToList());
        }

        // GET: SanPhams_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm sản phẩm theo id
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            // Lấy danh sách hình ảnh của sản phẩm
            var images = db.HinhAnhSanPhams.Where(h => h.MaSP == sanPham.MaSP).ToList();

            // Truyền sản phẩm và hình ảnh vào ViewBag
            ViewBag.Images = images;

            return View(sanPham);
        }

        // GET: SanPhams_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLSP");
            return View();
        }

        // POST: SanPhams_62131904/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,MoTa,DonGia,DonViTinh,MaLSP")] SanPham sanPham)
        {
            // Xử lý file ảnh đặc trưng
            var avatarFile = Request.Files["Avatar"];
            if (ModelState.IsValid)
            {
                // Lưu sản phẩm vào cơ sở dữ liệu
                db.SanPhams.Add(sanPham);
                db.SaveChanges();

                // Lưu ảnh đặc trưng vào bảng SanPham
                if (avatarFile != null && avatarFile.ContentLength > 0)
                {
                    string avatarFileName = Path.GetFileName(avatarFile.FileName);
                    string avatarFilePath = Path.Combine(Server.MapPath("/Images/"), avatarFileName);
                    avatarFile.SaveAs(avatarFilePath);

                    // Cập nhật ảnh sản phẩm vào bảng SanPham
                    sanPham.AnhSP = "/Images/" + avatarFileName;
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                }

                // Xử lý các hình ảnh phụ
                var imgFiles = Request.Files;
                List<HinhAnhSanPham> newImages = new List<HinhAnhSanPham>();

                foreach (string fileKey in imgFiles)
                {
                    var file = imgFiles[fileKey];
                    if (file != null && file.ContentLength > 0 && fileKey != "Avatar") // Đảm bảo không lưu lại ảnh đại diện
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string filePath = Path.Combine(Server.MapPath("/Images/"), fileName);
                        file.SaveAs(filePath);

                        newImages.Add(new HinhAnhSanPham
                        {
                            AnhPhuSP = "/Images/" + fileName,
                            MaSP = sanPham.MaSP
                        });
                    }
                }

                // Lưu các hình ảnh phụ vào bảng HinhAnhSanPhams
                db.HinhAnhSanPhams.AddRange(newImages);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // Nếu model không hợp lệ, trả lại View với lỗi
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLSP", sanPham.MaLSP);
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
            // Lấy danh sách các hình ảnh hiện tại của sản phẩm
            var currentImages = db.HinhAnhSanPhams.Where(h => h.MaSP == sanPham.MaSP).ToList();
            ViewBag.CurrentImages = currentImages;

            return View(sanPham);
        }

        // POST: SanPhams_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,MoTa,DonGia,DonViTinh,AnhSP,MaLSP")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;

                // Xử lý việc tải lên các hình ảnh mới
                var imgFiles = Request.Files;
                List<HinhAnhSanPham> newImages = new List<HinhAnhSanPham>();

                foreach (string fileKey in imgFiles)
                {
                    var file = imgFiles[fileKey];
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(file.FileName);
                        string path = Server.MapPath("/Images/" + fileName);
                        file.SaveAs(path);

                        newImages.Add(new HinhAnhSanPham
                        {
                            AnhPhuSP = "/Images/" + fileName,
                            MaSP = sanPham.MaSP
                        });
                    }
                }

                // Thêm hình ảnh mới vào cơ sở dữ liệu
                foreach (var image in newImages)
                {
                    db.HinhAnhSanPhams.Add(image);
                }

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
        // POST: SanPhams_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Tìm sản phẩm theo id
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            // Lấy danh sách hình ảnh của sản phẩm
            var images = db.HinhAnhSanPhams.Where(h => h.MaSP == sanPham.MaSP).ToList();

            // Xóa tất cả hình ảnh liên quan đến sản phẩm (nếu có)
            foreach (var image in images)
            {
                // Nếu có ảnh lưu trên server, xóa ảnh khỏi thư mục
                var imagePath = Server.MapPath(image.AnhPhuSP);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                // Xóa hình ảnh trong cơ sở dữ liệu
                db.HinhAnhSanPhams.Remove(image);
            }

            // Xóa sản phẩm trong cơ sở dữ liệu
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
