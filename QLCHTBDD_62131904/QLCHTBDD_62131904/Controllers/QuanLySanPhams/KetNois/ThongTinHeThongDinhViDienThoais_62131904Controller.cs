using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Models;

namespace QLCHTBDD_62131904.Controllers.QuanLySanPhams.KetNois
{
    public class ThongTinHeThongDinhViDienThoais_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: ThongTinHeThongDinhViDienThoais_62131904
        public ActionResult Index()
        {
            var thongTinHeThongDinhViDienThoais = db.ThongTinHeThongDinhViDienThoais
                .Include(t => t.HeThongDinhViGP)
                .Include(t => t.ThongSoBienTheDienThoai)
                .Include(t => t.ThongSoBienTheDienThoai.BienTheSanPham.SanPham);
                
            return View(thongTinHeThongDinhViDienThoais.ToList());
        }

        // GET: ThongTinHeThongDinhViDienThoais_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinHeThongDinhViDienThoai thongTinHeThongDinhViDienThoai = db.ThongTinHeThongDinhViDienThoais.Find(id);
            if (thongTinHeThongDinhViDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongTinHeThongDinhViDienThoai);
        }

        // GET: ThongTinHeThongDinhViDienThoais_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaHeThongDinhViGPS = new SelectList(db.HeThongDinhViGPS, "MaHeThongDinhViGPS", "TenHeThongDinhViGPS");
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

        // POST: ThongTinHeThongDinhViDienThoais_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaThongTinHeThongDinhVi,MaTSBTDT,MaHeThongDinhViGPS")] ThongTinHeThongDinhViDienThoai thongTinHeThongDinhViDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.ThongTinHeThongDinhViDienThoais.Add(thongTinHeThongDinhViDienThoai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHeThongDinhViGPS = new SelectList(db.HeThongDinhViGPS, "MaHeThongDinhViGPS", "TenHeThongDinhViGPS", thongTinHeThongDinhViDienThoai.MaHeThongDinhViGPS);
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
            ViewBag.MaTSBTDT = new SelectList(thongSoBienThe, "MaTSBTDT", "DisplayText", thongTinHeThongDinhViDienThoai.MaTSBTDT);
            return View(thongTinHeThongDinhViDienThoai);
        }

        // GET: ThongTinHeThongDinhViDienThoais_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinHeThongDinhViDienThoai thongTinHeThongDinhViDienThoai = db.ThongTinHeThongDinhViDienThoais.Find(id);
            if (thongTinHeThongDinhViDienThoai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHeThongDinhViGPS = new SelectList(db.HeThongDinhViGPS, "MaHeThongDinhViGPS", "TenHeThongDinhViGPS", thongTinHeThongDinhViDienThoai.MaHeThongDinhViGPS);
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", thongTinHeThongDinhViDienThoai.MaTSBTDT);
            return View(thongTinHeThongDinhViDienThoai);
        }

        // POST: ThongTinHeThongDinhViDienThoais_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThongTinHeThongDinhVi,MaTSBTDT,MaHeThongDinhViGPS")] ThongTinHeThongDinhViDienThoai thongTinHeThongDinhViDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongTinHeThongDinhViDienThoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHeThongDinhViGPS = new SelectList(db.HeThongDinhViGPS, "MaHeThongDinhViGPS", "TenHeThongDinhViGPS", thongTinHeThongDinhViDienThoai.MaHeThongDinhViGPS);
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", thongTinHeThongDinhViDienThoai.MaTSBTDT);
            return View(thongTinHeThongDinhViDienThoai);
        }

        // GET: ThongTinHeThongDinhViDienThoais_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinHeThongDinhViDienThoai thongTinHeThongDinhViDienThoai = db.ThongTinHeThongDinhViDienThoais.Find(id);
            if (thongTinHeThongDinhViDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongTinHeThongDinhViDienThoai);
        }

        // POST: ThongTinHeThongDinhViDienThoais_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongTinHeThongDinhViDienThoai thongTinHeThongDinhViDienThoai = db.ThongTinHeThongDinhViDienThoais.Find(id);
            db.ThongTinHeThongDinhViDienThoais.Remove(thongTinHeThongDinhViDienThoai);
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
