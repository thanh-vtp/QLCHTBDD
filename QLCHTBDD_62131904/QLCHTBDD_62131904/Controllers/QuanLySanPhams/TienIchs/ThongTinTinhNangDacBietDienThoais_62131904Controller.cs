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
    public class ThongTinTinhNangDacBietDienThoais_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: ThongTinTinhNangDacBietDienThoais_62131904
        public ActionResult Index()
        {
            var thongTinTinhNangDacBietDienThoais = db.ThongTinTinhNangDacBietDienThoais
                .Include(t => t.ThongSoBienTheDienThoai)
                .Include(t => t.ThongSoBienTheDienThoai.BienTheSanPham.SanPham)
                .Include(t => t.TinhNangDacBiet);
            return View(thongTinTinhNangDacBietDienThoais.ToList());
        }

        // GET: ThongTinTinhNangDacBietDienThoais_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinTinhNangDacBietDienThoai thongTinTinhNangDacBietDienThoai = db.ThongTinTinhNangDacBietDienThoais.Find(id);
            if (thongTinTinhNangDacBietDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongTinTinhNangDacBietDienThoai);
        }

        // GET: ThongTinTinhNangDacBietDienThoais_62131904/Create
        public ActionResult Create()
        {
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
            ViewBag.MaTinhNangDacBiet = new SelectList(db.TinhNangDacBiets, "MaTinhNangDacBiet", "TenTinhNangDacBiet");
            return View();
        }

        // POST: ThongTinTinhNangDacBietDienThoais_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaThongTinTinhNangDacBiet,MaTSBTDT,MaTinhNangDacBiet")] ThongTinTinhNangDacBietDienThoai thongTinTinhNangDacBietDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.ThongTinTinhNangDacBietDienThoais.Add(thongTinTinhNangDacBietDienThoai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", thongTinTinhNangDacBietDienThoai.MaTSBTDT);
            ViewBag.MaTinhNangDacBiet = new SelectList(db.TinhNangDacBiets, "MaTinhNangDacBiet", "TenTinhNangDacBiet", thongTinTinhNangDacBietDienThoai.MaTinhNangDacBiet);
            return View(thongTinTinhNangDacBietDienThoai);
        }

        // GET: ThongTinTinhNangDacBietDienThoais_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinTinhNangDacBietDienThoai thongTinTinhNangDacBietDienThoai = db.ThongTinTinhNangDacBietDienThoais.Find(id);
            if (thongTinTinhNangDacBietDienThoai == null)
            {
                return HttpNotFound();
            }
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
            ViewBag.MaTSBTDT = new SelectList(thongSoBienThe, "MaTSBTDT", "DisplayText", thongTinTinhNangDacBietDienThoai.MaTSBTDT);
            ViewBag.MaTinhNangDacBiet = new SelectList(db.TinhNangDacBiets, "MaTinhNangDacBiet", "TenTinhNangDacBiet", thongTinTinhNangDacBietDienThoai.MaTinhNangDacBiet);
            return View(thongTinTinhNangDacBietDienThoai);
        }

        // POST: ThongTinTinhNangDacBietDienThoais_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThongTinTinhNangDacBiet,MaTSBTDT,MaTinhNangDacBiet")] ThongTinTinhNangDacBietDienThoai thongTinTinhNangDacBietDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongTinTinhNangDacBietDienThoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", thongTinTinhNangDacBietDienThoai.MaTSBTDT);
            ViewBag.MaTinhNangDacBiet = new SelectList(db.TinhNangDacBiets, "MaTinhNangDacBiet", "TenTinhNangDacBiet", thongTinTinhNangDacBietDienThoai.MaTinhNangDacBiet);
            return View(thongTinTinhNangDacBietDienThoai);
        }

        // GET: ThongTinTinhNangDacBietDienThoais_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinTinhNangDacBietDienThoai thongTinTinhNangDacBietDienThoai = db.ThongTinTinhNangDacBietDienThoais.Find(id);
            if (thongTinTinhNangDacBietDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongTinTinhNangDacBietDienThoai);
        }

        // POST: ThongTinTinhNangDacBietDienThoais_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongTinTinhNangDacBietDienThoai thongTinTinhNangDacBietDienThoai = db.ThongTinTinhNangDacBietDienThoais.Find(id);
            db.ThongTinTinhNangDacBietDienThoais.Remove(thongTinTinhNangDacBietDienThoai);
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
