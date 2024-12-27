using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Models;
using QLCHTBDD_62131904.ViewModels;
using QLCHTBDD_62131904.Helpers;
using System.EnterpriseServices.Internal;
using System.Data.Entity.Infrastructure;

namespace QLCHTBDD_62131904.Controllers.SanPhams
{
    public class SanPhams_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // get danh sach san pham
        private List<SanPhamViewModel> GetListSanPhamViewModel()
        {
            return db.SanPhams
                .Include(b => b.LoaiSanPham)
                .Include(b => b.HangSanXuat)
                .Select(b => new SanPhamViewModel
                {
                    MaSP = b.MaSP,
                    TenSP = b.TenSP,
                    TenLoaiSanPham = b.LoaiSanPham.TenLSP,
                    TenHangSanXuat = b.HangSanXuat.TenHang,
                }).ToList();
        }

        // lấy sản phẩm
        private SanPhamViewModel GetSanPhamViewModel(SanPham sanPham)
        {
            return new SanPhamViewModel
            {
                MaSP = sanPham.MaSP,
                TenSP = sanPham.TenSP,
                MoTa = sanPham.MoTa,
                TenLoaiSanPham = sanPham.LoaiSanPham?.TenLSP,
                TenHangSanXuat = sanPham.HangSanXuat?.TenHang 
            };
        }

        // GET: SanPhams_62131904
        public ActionResult Index()
        {
            var sanPhams = GetListSanPhamViewModel();
            return View(sanPhams);
        }

        // GET: SanPhams_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SanPham sanPhamResult = db.SanPhams.Find(id);
            if (sanPhamResult == null)
            {
                return HttpNotFound();
            }

            var sanPham = GetSanPhamViewModel(sanPhamResult);
            return View(sanPham);
        }

        // create view model
        public SanPhamViewModel CreateSanPhamViewModel(SanPham sanPham)
        {
            var viewModel = new SanPhamViewModel
            {
                MaSP = sanPham.MaSP,
                TenSP = sanPham.TenSP,
                MoTa = sanPham.MoTa,
                TenHangSanXuat = db.HangSanXuats.FirstOrDefault(h => h.MaHang == sanPham.MaHang)?.TenHang,
                TenLoaiSanPham = db.LoaiSanPhams.FirstOrDefault(l => l.MaLSP == sanPham.MaLSP)?.TenLSP,
                // Bạn có thể thêm các thuộc tính khác vào đây nếu cần
            };

            return viewModel;
        }

        // GET: SanPhams_62131904/Create
        public ActionResult Create()
        {
            var viewModel = new SanPhamViewModel();
            ViewBag.MaHang = StaticEntityHelper.GetHangSanXuat();
            ViewBag.MaLSP = StaticEntityHelper.GetLoaiSanPham();
            return View(viewModel);
        }

        // POST: SanPhams_62131904/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,MoTa,MaLSP,MaHang")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var viewModel = CreateSanPhamViewModel(sanPham);
            ViewBag.MaHang = new SelectList(StaticEntityHelper.GetHangSanXuat(), "MaHang", "TenHang", sanPham.MaHang);
            ViewBag.MaLSP = new SelectList(StaticEntityHelper.GetLoaiSanPham(), "MaLSP", "TenLSP", sanPham.MaLSP);
            return View(sanPham);
        }

        // GET: SanPhams_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPhamResult = db.SanPhams.Find(id);
            if (sanPhamResult == null)
            {
                return HttpNotFound();
            }

            var sanPham = GetSanPhamViewModel(sanPhamResult);
            ViewBag.MaHang = StaticEntityHelper.GetHangSanXuat();
            ViewBag.MaLSP = StaticEntityHelper.GetLoaiSanPham();
            return View(sanPham);
        }

        // POST: SanPhams_62131904/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,MoTa,MaLSP,MaHang")] SanPham sanPham)
        {
            //Console.WriteLine(sanPham.MaSP);
            //Console.WriteLine(sanPham.TenSP);
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHang = StaticEntityHelper.GetHangSanXuat();
            ViewBag.MaLSP = StaticEntityHelper.GetLoaiSanPham();
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
