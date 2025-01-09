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
    public class ThongTinWifiDienThoais_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: ThongTinWifiDienThoais_62131904
        public ActionResult Index()
        {
            var thongTinWifiDienThoais = db.ThongTinWifiDienThoais
                .Include(t => t.ThongSoBienTheDienThoai)
                .Include(t => t.ThongSoBienTheDienThoai.BienTheSanPham.SanPham)
                .Include(t => t.Wifi);
            return View(thongTinWifiDienThoais.ToList());
        }

        // GET: ThongTinWifiDienThoais_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinWifiDienThoai thongTinWifiDienThoai = db.ThongTinWifiDienThoais.Find(id);
            if (thongTinWifiDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongTinWifiDienThoai);
        }

        // GET: ThongTinWifiDienThoais_62131904/Create
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
            ViewBag.MaWifi = new SelectList(db.Wifis, "MaWifi", "TenWifi");
            return View();
        }

        // POST: ThongTinWifiDienThoais_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaThongTinWifi,MaTSBTDT,MaWifi")] ThongTinWifiDienThoai thongTinWifiDienThoai)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check for duplicate entry
                    bool isDuplicate = db.ThongTinWifiDienThoais.Any(info =>
                        info.MaTSBTDT == thongTinWifiDienThoai.MaTSBTDT &&
                        info.MaWifi == thongTinWifiDienThoai.MaWifi);

                    if (isDuplicate)
                    {
                        ModelState.AddModelError("", "Thông tin WiFi đã tồn tại. Vui lòng kiểm tra lại.");
                    }
                    else
                    {
                        // Add new record
                        db.ThongTinWifiDienThoais.Add(thongTinWifiDienThoai);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");

                    // General error message
                    ModelState.AddModelError("", "Đã xảy ra lỗi trong quá trình thêm thông tin WiFi.");
                }
            }

            // Repopulate ViewBag for dropdowns
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

            ViewBag.MaTSBTDT = new SelectList(thongSoBienThe, "MaTSBTDT", "DisplayText", thongTinWifiDienThoai.MaTSBTDT);
            ViewBag.MaWifi = new SelectList(db.Wifis, "MaWifi", "TenWifi", thongTinWifiDienThoai.MaWifi);

            return View(thongTinWifiDienThoai);
        }


        // GET: ThongTinWifiDienThoais_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinWifiDienThoai thongTinWifiDienThoai = db.ThongTinWifiDienThoais.Find(id);
            if (thongTinWifiDienThoai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", thongTinWifiDienThoai.MaTSBTDT);
            ViewBag.MaWifi = new SelectList(db.Wifis, "MaWifi", "TenWifi", thongTinWifiDienThoai.MaWifi);
            return View(thongTinWifiDienThoai);
        }

        // POST: ThongTinWifiDienThoais_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThongTinWifi,MaTSBTDT,MaWifi")] ThongTinWifiDienThoai thongTinWifiDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongTinWifiDienThoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTSBTDT = new SelectList(db.ThongSoBienTheDienThoais, "MaTSBTDT", "DungLuongConLai", thongTinWifiDienThoai.MaTSBTDT);
            ViewBag.MaWifi = new SelectList(db.Wifis, "MaWifi", "TenWifi", thongTinWifiDienThoai.MaWifi);
            return View(thongTinWifiDienThoai);
        }

        // GET: ThongTinWifiDienThoais_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinWifiDienThoai thongTinWifiDienThoai = db.ThongTinWifiDienThoais.Find(id);
            if (thongTinWifiDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongTinWifiDienThoai);
        }

        // POST: ThongTinWifiDienThoais_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongTinWifiDienThoai thongTinWifiDienThoai = db.ThongTinWifiDienThoais.Find(id);
            db.ThongTinWifiDienThoais.Remove(thongTinWifiDienThoai);
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
