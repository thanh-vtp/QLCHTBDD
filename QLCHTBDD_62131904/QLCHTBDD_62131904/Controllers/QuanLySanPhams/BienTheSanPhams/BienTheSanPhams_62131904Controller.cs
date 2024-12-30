using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Models;

namespace QLCHTBDD_62131904.Controllers.SanPhams
{
    public class BienTheSanPhams_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: BienTheSanPhams_62131904
        public ActionResult Index()
        {
            var bienTheSanPhams = db.BienTheSanPhams.Include(b => b.MauSac).Include(b => b.RAM).Include(b => b.ROM).Include(b => b.TrangThaiSanPham).Include(b => b.SanPham);
            return View(bienTheSanPhams.ToList());
        }

        // GET: BienTheSanPhams_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BienTheSanPham bienTheSanPham = db.BienTheSanPhams.Find(id);
            if (bienTheSanPham == null)
            {
                return HttpNotFound();
            }
            return View(bienTheSanPham);
        }

        // GET: BienTheSanPhams_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaMau = new SelectList(db.MauSacs, "MaMau", "TenMau");
            ViewBag.MaRAM = new SelectList(db.RAMs, "MaRAM", "DungLuong");
            ViewBag.MaROM = new SelectList(db.ROMs, "MaROM", "DungLuong");
            ViewBag.MaTrangThai = new SelectList(db.TrangThaiSanPhams, "MaTrangThai", "TenTrangThai");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: BienTheSanPhams_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBT,MaSP,MaMau,MaRAM,MaROM,SoLuong,DonViTinh,DonGia,SKU,MaTrangThai")] BienTheSanPham bienTheSanPham)
        {
            if (ModelState.IsValid)
            {
                db.BienTheSanPhams.Add(bienTheSanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaMau = new SelectList(db.MauSacs, "MaMau", "TenMau", bienTheSanPham.MaMau);
            ViewBag.MaRAM = new SelectList(db.RAMs, "MaRAM", "DungLuong", bienTheSanPham.MaRAM);
            ViewBag.MaROM = new SelectList(db.ROMs, "MaROM", "DungLuong", bienTheSanPham.MaROM);
            ViewBag.MaTrangThai = new SelectList(db.TrangThaiSanPhams, "MaTrangThai", "TenTrangThai", bienTheSanPham.MaTrangThai);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", bienTheSanPham.MaSP);
            return View(bienTheSanPham);
        }

        // GET: BienTheSanPhams_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BienTheSanPham bienTheSanPham = db.BienTheSanPhams.Find(id);
            if (bienTheSanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMau = new SelectList(db.MauSacs, "MaMau", "TenMau", bienTheSanPham.MaMau);
            ViewBag.MaRAM = new SelectList(db.RAMs, "MaRAM", "DungLuong", bienTheSanPham.MaRAM);
            ViewBag.MaROM = new SelectList(db.ROMs, "MaROM", "DungLuong", bienTheSanPham.MaROM);
            ViewBag.MaTrangThai = new SelectList(db.TrangThaiSanPhams, "MaTrangThai", "TenTrangThai", bienTheSanPham.MaTrangThai);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", bienTheSanPham.MaSP);
            return View(bienTheSanPham);
        }

        // POST: BienTheSanPhams_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBT,MaSP,MaMau,MaRAM,MaROM,SoLuong,DonViTinh,DonGia,SKU,MaTrangThai")] BienTheSanPham bienTheSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bienTheSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaMau = new SelectList(db.MauSacs, "MaMau", "TenMau", bienTheSanPham.MaMau);
            ViewBag.MaRAM = new SelectList(db.RAMs, "MaRAM", "DungLuong", bienTheSanPham.MaRAM);
            ViewBag.MaROM = new SelectList(db.ROMs, "MaROM", "DungLuong", bienTheSanPham.MaROM);
            ViewBag.MaTrangThai = new SelectList(db.TrangThaiSanPhams, "MaTrangThai", "TenTrangThai", bienTheSanPham.MaTrangThai);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", bienTheSanPham.MaSP);
            return View(bienTheSanPham);
        }

        // GET: BienTheSanPhams_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BienTheSanPham bienTheSanPham = db.BienTheSanPhams.Find(id);
            if (bienTheSanPham == null)
            {
                return HttpNotFound();
            }
            return View(bienTheSanPham);
        }

        // POST: BienTheSanPhams_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BienTheSanPham bienTheSanPham = db.BienTheSanPhams.Find(id);
            db.BienTheSanPhams.Remove(bienTheSanPham);
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
