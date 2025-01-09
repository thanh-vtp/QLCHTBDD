using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Models;

namespace QLCHTBDD_62131904.Controllers.QuanLySanPhams.QuayPhims
{
    public class ThongSoQuayPhims_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: ThongSoQuayPhims_62131904
        public ActionResult Index()
        {
            var thongSoQuayPhims = db.ThongSoQuayPhims
                .Include(t => t.DoPhanGiaiQuayPhim)
                .Include(t => t.ThongSoBienTheDienThoai)
                .Include(t => t.ThongSoBienTheDienThoai.BienTheSanPham.SanPham)
                .Include(t => t.TocDoKhungHinh);
            return View(thongSoQuayPhims.ToList());
        }

        // GET: ThongSoQuayPhims_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoQuayPhim thongSoQuayPhim = db.ThongSoQuayPhims.Find(id);
            if (thongSoQuayPhim == null)
            {
                return HttpNotFound();
            }
            return View(thongSoQuayPhim);
        }

        // GET: ThongSoQuayPhims_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaDoPhanGiaiQuayPhim = new SelectList(db.DoPhanGiaiQuayPhims, "MaDoPhanGiaiQuayPhim", "TenDoPhanGiaiQuayPhim");
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
            ViewBag.MaTocDoKhungHinh = new SelectList(db.TocDoKhungHinhs, "MaTocDoKhungHinh", "TocDo");
            return View();
        }

        // POST: ThongSoQuayPhims_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaThongSoQuayPhim,MaTSBTDT,MaDoPhanGiaiQuayPhim,MaTocDoKhungHinh")] ThongSoQuayPhim thongSoQuayPhim)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check for duplicates
                    bool isDuplicate = db.ThongSoQuayPhims.Any(tsqp =>
                        tsqp.MaTSBTDT == thongSoQuayPhim.MaTSBTDT &&
                        tsqp.MaDoPhanGiaiQuayPhim == thongSoQuayPhim.MaDoPhanGiaiQuayPhim &&
                        tsqp.MaTocDoKhungHinh == thongSoQuayPhim.MaTocDoKhungHinh);

                    if (isDuplicate)
                    {
                        ModelState.AddModelError("", "Thông số quay phim đã tồn tại. Vui lòng kiểm tra lại.");
                    }
                    else
                    {
                        // Add new entry
                        db.ThongSoQuayPhims.Add(thongSoQuayPhim);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");

                    // Add general error message
                    ModelState.AddModelError("", "Đã xảy ra lỗi trong quá trình thêm thông số quay phim.");
                }
            }

            // Repopulate ViewBag for dropdowns
            ViewBag.MaDoPhanGiaiQuayPhim = new SelectList(db.DoPhanGiaiQuayPhims, "MaDoPhanGiaiQuayPhim", "TenDoPhanGiaiQuayPhim", thongSoQuayPhim.MaDoPhanGiaiQuayPhim);

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

            ViewBag.MaTSBTDT = new SelectList(thongSoBienThe, "MaTSBTDT", "DisplayText", thongSoQuayPhim.MaTSBTDT);
            ViewBag.MaTocDoKhungHinh = new SelectList(db.TocDoKhungHinhs, "MaTocDoKhungHinh", "TocDo", thongSoQuayPhim.MaTocDoKhungHinh);

            return View(thongSoQuayPhim);
        }


        // GET: ThongSoQuayPhims_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoQuayPhim thongSoQuayPhim = db.ThongSoQuayPhims.Find(id);
            if (thongSoQuayPhim == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDoPhanGiaiQuayPhim = new SelectList(db.DoPhanGiaiQuayPhims, "MaDoPhanGiaiQuayPhim", "TenDoPhanGiaiQuayPhim", thongSoQuayPhim.MaDoPhanGiaiQuayPhim);
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", thongSoQuayPhim.MaTSBTDT);
            ViewBag.MaTocDoKhungHinh = new SelectList(db.TocDoKhungHinhs, "MaTocDoKhungHinh", "MaTocDoKhungHinh", thongSoQuayPhim.MaTocDoKhungHinh);
            return View(thongSoQuayPhim);
        }

        // POST: ThongSoQuayPhims_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThongSoQuayPhim,MaTSBTDT,MaDoPhanGiaiQuayPhim,MaTocDoKhungHinh")] ThongSoQuayPhim thongSoQuayPhim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongSoQuayPhim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDoPhanGiaiQuayPhim = new SelectList(db.DoPhanGiaiQuayPhims, "MaDoPhanGiaiQuayPhim", "TenDoPhanGiaiQuayPhim", thongSoQuayPhim.MaDoPhanGiaiQuayPhim);
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", thongSoQuayPhim.MaTSBTDT);
            ViewBag.MaTocDoKhungHinh = new SelectList(db.TocDoKhungHinhs, "MaTocDoKhungHinh", "MaTocDoKhungHinh", thongSoQuayPhim.MaTocDoKhungHinh);
            return View(thongSoQuayPhim);
        }

        // GET: ThongSoQuayPhims_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoQuayPhim thongSoQuayPhim = db.ThongSoQuayPhims.Find(id);
            if (thongSoQuayPhim == null)
            {
                return HttpNotFound();
            }
            return View(thongSoQuayPhim);
        }

        // POST: ThongSoQuayPhims_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongSoQuayPhim thongSoQuayPhim = db.ThongSoQuayPhims.Find(id);
            db.ThongSoQuayPhims.Remove(thongSoQuayPhim);
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
