using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Models;

namespace QLCHTBDD_62131904.Controllers.QuanLiSanPhams.Cameras
{
    public class ChiTietDoPhanGiaiCameras_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: ChiTietDoPhanGiaiCameras_62131904
        public ActionResult Index()
        {
            var chiTietDoPhanGiaiCameras = db.ChiTietDoPhanGiaiCameras
                .Include(c => c.DoPhanGiaiCamera)
                .Include(c => c.ThongSoCamera)
                .Include(c => c.ThongSoCamera.LoaiCamera)
                .Include(c => c.ThongSoCamera.ThongSoBienTheDienThoai.BienTheSanPham.SanPham);
            return View(chiTietDoPhanGiaiCameras.ToList());
        }

        // GET: ChiTietDoPhanGiaiCameras_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDoPhanGiaiCamera chiTietDoPhanGiaiCamera = db.ChiTietDoPhanGiaiCameras.Find(id);
            if (chiTietDoPhanGiaiCamera == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDoPhanGiaiCamera);
        }

        // GET: ChiTietDoPhanGiaiCameras_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaDoPhanGiai = new SelectList(db.DoPhanGiaiCameras, "MaDoPhanGiai", "DoPhanGiai");
            var thongSoCamera = db.ThongSoCameras
                .Include(x => x.ThongSoBienTheDienThoai.BienTheSanPham)
                .Include(x => x.ThongSoBienTheDienThoai.BienTheSanPham.SanPham)
                .Select(x => new
                {
                    MaThongSoCamera = x.MaThongSoCamera,
                    DisplayText = x.ThongSoBienTheDienThoai.BienTheSanPham.SanPham.TenSP + 
                    "   (Màu: " + x.ThongSoBienTheDienThoai.BienTheSanPham.MauSac.TenMau +
                    ", RAM: " + x.ThongSoBienTheDienThoai.BienTheSanPham.RAM.DungLuong +
                    ", ROM: " + x.ThongSoBienTheDienThoai.BienTheSanPham.ROM.DungLuong +
                    ", SKU: " + x.ThongSoBienTheDienThoai.BienTheSanPham.SKU +
                    "   Loại Camera: " + x.LoaiCamera.TenLoaiCamera + " )"
                })
                .ToList();

            ViewBag.MaThongSoCamera = new SelectList(thongSoCamera, "MaThongSoCamera", "DisplayText");
            return View();
        }

        // POST: ChiTietDoPhanGiaiCameras_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChiTietDoPhanGiaiCamera,MaThongSoCamera,MaDoPhanGiai")] ChiTietDoPhanGiaiCamera chiTietDoPhanGiaiCamera)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDoPhanGiaiCameras.Add(chiTietDoPhanGiaiCamera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDoPhanGiai = new SelectList(db.DoPhanGiaiCameras, "MaDoPhanGiai", "DoPhanGiai");
            var thongSoCamera = db.ThongSoCameras
                .Include(x => x.ThongSoBienTheDienThoai.BienTheSanPham)
                .Select(x => new
                {
                    MaThongSoCamera = x.MaThongSoCamera,
                    DisplayText = x.ThongSoBienTheDienThoai.BienTheSanPham.SKU + " - " + x.LoaiCamera.TenLoaiCamera

                })
                .ToList();

            ViewBag.MaThongSoCamera = new SelectList(thongSoCamera, "MaThongSoCamera", "DisplayText");
            return View(chiTietDoPhanGiaiCamera);
        }

        // GET: ChiTietDoPhanGiaiCameras_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDoPhanGiaiCamera chiTietDoPhanGiaiCamera = db.ChiTietDoPhanGiaiCameras.Find(id);
            if (chiTietDoPhanGiaiCamera == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDoPhanGiai = new SelectList(db.DoPhanGiaiCameras, "MaDoPhanGiai", "DoPhanGiai", chiTietDoPhanGiaiCamera.MaDoPhanGiai);
            ViewBag.MaThongSoCamera = new SelectList(db.ThongSoCameras, "MaThongSoCamera", "MaThongSoCamera", chiTietDoPhanGiaiCamera.MaThongSoCamera);
            return View(chiTietDoPhanGiaiCamera);
        }

        // POST: ChiTietDoPhanGiaiCameras_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChiTietDoPhanGiaiCamera,MaThongSoCamera,MaDoPhanGiai")] ChiTietDoPhanGiaiCamera chiTietDoPhanGiaiCamera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDoPhanGiaiCamera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDoPhanGiai = new SelectList(db.DoPhanGiaiCameras, "MaDoPhanGiai", "DoPhanGiai", chiTietDoPhanGiaiCamera.MaDoPhanGiai);
            ViewBag.MaThongSoCamera = new SelectList(db.ThongSoCameras, "MaThongSoCamera", "MaThongSoCamera", chiTietDoPhanGiaiCamera.MaThongSoCamera);
            return View(chiTietDoPhanGiaiCamera);
        }

        // GET: ChiTietDoPhanGiaiCameras_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDoPhanGiaiCamera chiTietDoPhanGiaiCamera = db.ChiTietDoPhanGiaiCameras.Find(id);
            if (chiTietDoPhanGiaiCamera == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDoPhanGiaiCamera);
        }

        // POST: ChiTietDoPhanGiaiCameras_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietDoPhanGiaiCamera chiTietDoPhanGiaiCamera = db.ChiTietDoPhanGiaiCameras.Find(id);
            db.ChiTietDoPhanGiaiCameras.Remove(chiTietDoPhanGiaiCamera);
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
