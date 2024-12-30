using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Models;

namespace QLCHTBDD_62131904.Controllers.QuanLySanPhams.TienIchs
{
    public class ThongTinDinhDangPhimVaNhacDienThoais_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: ThongTinDinhDangPhimVaNhacDienThoais_62131904
        public ActionResult Index()
        {
            var thongTinDinhDangPhimVaNhacDienThoais = db.ThongTinDinhDangPhimVaNhacDienThoais
                .Include(t => t.DinhDangNhac)
                .Include(t => t.DinhDangPhim)
                .Include(t => t.ThongSoBienTheDienThoai)
                .Include(t => t.ThongSoBienTheDienThoai.BienTheSanPham.SanPham);
            return View(thongTinDinhDangPhimVaNhacDienThoais.ToList());
        }

        // GET: ThongTinDinhDangPhimVaNhacDienThoais_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinDinhDangPhimVaNhacDienThoai thongTinDinhDangPhimVaNhacDienThoai = db.ThongTinDinhDangPhimVaNhacDienThoais.Find(id);
            if (thongTinDinhDangPhimVaNhacDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongTinDinhDangPhimVaNhacDienThoai);
        }

        // GET: ThongTinDinhDangPhimVaNhacDienThoais_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaDinhDangNhac = new SelectList(db.DinhDangNhacs, "MaDinhDangNhac", "TenDinhDangNhac");
            ViewBag.MaDinhDangPhim = new SelectList(db.DinhDangPhims, "MaDinhDangPhim", "TenDinhDangPhim");
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

        // POST: ThongTinDinhDangPhimVaNhacDienThoais_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaThongTinDinhDang,MaTSBTDT,MaDinhDangPhim,MaDinhDangNhac")] ThongTinDinhDangPhimVaNhacDienThoai thongTinDinhDangPhimVaNhacDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.ThongTinDinhDangPhimVaNhacDienThoais.Add(thongTinDinhDangPhimVaNhacDienThoai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDinhDangNhac = new SelectList(db.DinhDangNhacs, "MaDinhDangNhac", "TenDinhDangNhac", thongTinDinhDangPhimVaNhacDienThoai.MaDinhDangNhac);
            ViewBag.MaDinhDangPhim = new SelectList(db.DinhDangPhims, "MaDinhDangPhim", "TenDinhDangPhim", thongTinDinhDangPhimVaNhacDienThoai.MaDinhDangPhim);
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
            ViewBag.MaTSBTDT = new SelectList(thongSoBienThe, "MaTSBTDT", "DisplayText", thongTinDinhDangPhimVaNhacDienThoai.MaTSBTDT);
            return View(thongTinDinhDangPhimVaNhacDienThoai);
        }

        // GET: ThongTinDinhDangPhimVaNhacDienThoais_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinDinhDangPhimVaNhacDienThoai thongTinDinhDangPhimVaNhacDienThoai = db.ThongTinDinhDangPhimVaNhacDienThoais.Find(id);
            if (thongTinDinhDangPhimVaNhacDienThoai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDinhDangNhac = new SelectList(db.DinhDangNhacs, "MaDinhDangNhac", "TenDinhDangNhac", thongTinDinhDangPhimVaNhacDienThoai.MaDinhDangNhac);
            ViewBag.MaDinhDangPhim = new SelectList(db.DinhDangPhims, "MaDinhDangPhim", "TenDinhDangPhim", thongTinDinhDangPhimVaNhacDienThoai.MaDinhDangPhim);
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", thongTinDinhDangPhimVaNhacDienThoai.MaTSBTDT);
            return View(thongTinDinhDangPhimVaNhacDienThoai);
        }

        // POST: ThongTinDinhDangPhimVaNhacDienThoais_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThongTinDinhDang,MaTSBTDT,MaDinhDangPhim,MaDinhDangNhac")] ThongTinDinhDangPhimVaNhacDienThoai thongTinDinhDangPhimVaNhacDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongTinDinhDangPhimVaNhacDienThoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDinhDangNhac = new SelectList(db.DinhDangNhacs, "MaDinhDangNhac", "TenDinhDangNhac", thongTinDinhDangPhimVaNhacDienThoai.MaDinhDangNhac);
            ViewBag.MaDinhDangPhim = new SelectList(db.DinhDangPhims, "MaDinhDangPhim", "TenDinhDangPhim", thongTinDinhDangPhimVaNhacDienThoai.MaDinhDangPhim);
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", thongTinDinhDangPhimVaNhacDienThoai.MaTSBTDT);
            return View(thongTinDinhDangPhimVaNhacDienThoai);
        }

        // GET: ThongTinDinhDangPhimVaNhacDienThoais_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinDinhDangPhimVaNhacDienThoai thongTinDinhDangPhimVaNhacDienThoai = db.ThongTinDinhDangPhimVaNhacDienThoais.Find(id);
            if (thongTinDinhDangPhimVaNhacDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongTinDinhDangPhimVaNhacDienThoai);
        }

        // POST: ThongTinDinhDangPhimVaNhacDienThoais_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongTinDinhDangPhimVaNhacDienThoai thongTinDinhDangPhimVaNhacDienThoai = db.ThongTinDinhDangPhimVaNhacDienThoais.Find(id);
            db.ThongTinDinhDangPhimVaNhacDienThoais.Remove(thongTinDinhDangPhimVaNhacDienThoai);
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
