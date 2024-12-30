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
    public class ThongSoCameras_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: ThongSoCameras_62131904
        public ActionResult Index()
        {
            var thongSoCameras = db.ThongSoCameras
                .Include(t => t.LoaiCamera)
                .Include(t => t.ThongSoBienTheDienThoai)
                .Include(t => t.ThongSoBienTheDienThoai.BienTheSanPham.SanPham);
            return View(thongSoCameras.ToList());
        }

        // GET: ThongSoCameras_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoCamera thongSoCamera = db.ThongSoCameras.Find(id);
            if (thongSoCamera == null)
            {
                return HttpNotFound();
            }
            return View(thongSoCamera);
        }

        // GET: ThongSoCameras_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiCamera = new SelectList(db.LoaiCameras, "MaLoaiCamera", "TenLoaiCamera");

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

        // POST: ThongSoCameras_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaThongSoCamera,MaTSBTDT,MaLoaiCamera")] ThongSoCamera thongSoCamera)
        {
            if (ModelState.IsValid)
            {
                db.ThongSoCameras.Add(thongSoCamera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiCamera = new SelectList(db.LoaiCameras, "MaLoaiCamera", "TenLoaiCamera", thongSoCamera.MaLoaiCamera);

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
            ViewBag.MaTSBTDT = new SelectList(thongSoBienThe, "MaTSBTDT", "DisplayText", thongSoCamera.MaTSBTDT);
            return View(thongSoCamera);
        }

        // GET: ThongSoCameras_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoCamera thongSoCamera = db.ThongSoCameras.Find(id);
            if (thongSoCamera == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiCamera = new SelectList(db.LoaiCameras, "MaLoaiCamera", "TenLoaiCamera", thongSoCamera.MaLoaiCamera);
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", thongSoCamera.MaTSBTDT);
            return View(thongSoCamera);
        }

        // POST: ThongSoCameras_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThongSoCamera,MaTSBTDT,MaLoaiCamera")] ThongSoCamera thongSoCamera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongSoCamera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiCamera = new SelectList(db.LoaiCameras, "MaLoaiCamera", "TenLoaiCamera", thongSoCamera.MaLoaiCamera);
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", thongSoCamera.MaTSBTDT);
            return View(thongSoCamera);
        }

        // GET: ThongSoCameras_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoCamera thongSoCamera = db.ThongSoCameras.Find(id);
            if (thongSoCamera == null)
            {
                return HttpNotFound();
            }
            return View(thongSoCamera);
        }

        // POST: ThongSoCameras_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongSoCamera thongSoCamera = db.ThongSoCameras.Find(id);
            db.ThongSoCameras.Remove(thongSoCamera);
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
