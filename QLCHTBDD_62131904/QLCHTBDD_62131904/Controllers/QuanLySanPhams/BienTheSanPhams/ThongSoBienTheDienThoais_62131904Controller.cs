using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Models;

namespace QLCHTBDD_62131904.Controllers.QuanLySanPhams.BienTheSanPhams
{
    public class ThongSoBienTheDienThoais_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: ThongSoBienTheDienThoais_62131904
        public ActionResult Index()
        {
            var thongSoBienTheDienThoais = db.ThongSoBienTheDienThoais.Include(t => t.BienTheSanPham).Include(t => t.ChieuDai).Include(t => t.ChieuNgang).Include(t => t.ChipDoHoaGPU).Include(t => t.ChipXuLy).Include(t => t.DoDay).Include(t => t.DoPhanGiaiManHinh).Include(t => t.DungLuongPin).Include(t => t.KhoiLuong).Include(t => t.KichThuocManHinh).Include(t => t.PixelDoc).Include(t => t.PixelNgang).Include(t => t.TanSoQuetManHinh).Include(t => t.TocDoCPU);
            return View(thongSoBienTheDienThoais.ToList());
        }

        // GET: ThongSoBienTheDienThoais_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoBienTheDienThoai thongSoBienTheDienThoai = db.ThongSoBienTheDienThoais.Find(id);
            if (thongSoBienTheDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongSoBienTheDienThoai);
        }

        // GET: ThongSoBienTheDienThoais_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaBT = new SelectList(db.BienTheSanPhams, "MaBT", "SKU");
            ViewBag.MaChieuDai = new SelectList(db.ChieuDais, "MaChieuDai", "GTChieuDai");
            ViewBag.MaChieuNgang = new SelectList(db.ChieuNgangs, "MaChieuNgang", "GTChieuNgang");
            ViewBag.MaChipDoHoaGPU = new SelectList(db.ChipDoHoaGPUs, "MaChipDoHoaGPU", "TenChipDoHoaGPU");
            ViewBag.MaChip = new SelectList(db.ChipXuLies, "MaChip", "TenChip");
            ViewBag.MaDoDay = new SelectList(db.DoDays, "MaDoDay", "GTDoDay");
            ViewBag.MaDoPhanGiai = new SelectList(db.DoPhanGiaiManHinhs, "MaDoPhanGiai", "TenDoPhanGiai");
            ViewBag.MaDungLuongPin = new SelectList(db.DungLuongPins, "MaDungLuongPin", "DungLuong");
            ViewBag.MaKhoiLuong = new SelectList(db.KhoiLuongs, "MaKhoiLuong", "GTKhoiLuong");
            ViewBag.MaKichThuocManHinh = new SelectList(db.KichThuocManHinhs, "MaKichThuocManHinh", "KichThuoc");
            ViewBag.MaPixelDoc = new SelectList(db.PixelDocs, "MaPixelDoc", "GTPixelDoc");
            ViewBag.MaPixelNgang = new SelectList(db.PixelNgangs, "MaPixelNgang", "GTPixelNgang");
            ViewBag.MaTanSoQuet = new SelectList(db.TanSoQuetManHinhs, "MaTanSoQuet", "TanSo");
            ViewBag.MaTocDoCPU = new SelectList(db.TocDoCPUs, "MaTocDoCPU", "TocDo");
            return View();
        }

        // POST: ThongSoBienTheDienThoais_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTSBTDT,MaBT,MaChip,MaTocDoCPU,MaChipDoHoaGPU,DungLuongConLai,MaDoPhanGiai,MaPixelNgang,MaPixelDoc,MaKichThuocManHinh,MaTanSoQuet,MaDungLuongPin,MaChieuDai,MaChieuNgang,MaDoDay,MaKhoiLuong")] ThongSoBienTheDienThoai thongSoBienTheDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.ThongSoBienTheDienThoais.Add(thongSoBienTheDienThoai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaBT = new SelectList(db.BienTheSanPhams, "MaBT", "SKU", thongSoBienTheDienThoai.MaBT);
            ViewBag.MaChieuDai = new SelectList(db.ChieuDais, "MaChieuDai", "GTChieuDai", thongSoBienTheDienThoai.MaChieuDai);
            ViewBag.MaChieuNgang = new SelectList(db.ChieuNgangs, "MaChieuNgang", "GTChieuNgang", thongSoBienTheDienThoai.MaChieuNgang);
            ViewBag.MaChipDoHoaGPU = new SelectList(db.ChipDoHoaGPUs, "MaChipDoHoaGPU", "TenChipDoHoaGPU", thongSoBienTheDienThoai.MaChipDoHoaGPU);
            ViewBag.MaChip = new SelectList(db.ChipXuLies, "MaChip", "TenChip", thongSoBienTheDienThoai.MaChip);
            ViewBag.MaDoDay = new SelectList(db.DoDays, "MaDoDay", "GTDoDay", thongSoBienTheDienThoai.MaDoDay);
            ViewBag.MaDoPhanGiai = new SelectList(db.DoPhanGiaiManHinhs, "MaDoPhanGiai", "TenDoPhanGiai", thongSoBienTheDienThoai.MaDoPhanGiai);
            ViewBag.MaDungLuongPin = new SelectList(db.DungLuongPins, "MaDungLuongPin", "DungLuong", thongSoBienTheDienThoai.MaDungLuongPin);
            ViewBag.MaKhoiLuong = new SelectList(db.KhoiLuongs, "MaKhoiLuong", "GTKhoiLuong", thongSoBienTheDienThoai.MaKhoiLuong);
            ViewBag.MaKichThuocManHinh = new SelectList(db.KichThuocManHinhs, "MaKichThuocManHinh", "KichThuoc", thongSoBienTheDienThoai.MaKichThuocManHinh);
            ViewBag.MaPixelDoc = new SelectList(db.PixelDocs, "MaPixelDoc", "GTPixelDoc", thongSoBienTheDienThoai.MaPixelDoc);
            ViewBag.MaPixelNgang = new SelectList(db.PixelNgangs, "MaPixelNgang", "GTPixelNgang", thongSoBienTheDienThoai.MaPixelNgang);
            ViewBag.MaTanSoQuet = new SelectList(db.TanSoQuetManHinhs, "MaTanSoQuet", "TanSo", thongSoBienTheDienThoai.MaTanSoQuet);
            ViewBag.MaTocDoCPU = new SelectList(db.TocDoCPUs, "MaTocDoCPU", "TocDo", thongSoBienTheDienThoai.MaTocDoCPU);
            return View(thongSoBienTheDienThoai);
        }

        // GET: ThongSoBienTheDienThoais_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoBienTheDienThoai thongSoBienTheDienThoai = db.ThongSoBienTheDienThoais.Find(id);
            if (thongSoBienTheDienThoai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBT = new SelectList(db.BienTheSanPhams, "MaBT", "DonViTinh", thongSoBienTheDienThoai.MaBT);
            ViewBag.MaChieuDai = new SelectList(db.ChieuDais, "MaChieuDai", "MaChieuDai", thongSoBienTheDienThoai.MaChieuDai);
            ViewBag.MaChieuNgang = new SelectList(db.ChieuNgangs, "MaChieuNgang", "MaChieuNgang", thongSoBienTheDienThoai.MaChieuNgang);
            ViewBag.MaChipDoHoaGPU = new SelectList(db.ChipDoHoaGPUs, "MaChipDoHoaGPU", "TenChipDoHoaGPU", thongSoBienTheDienThoai.MaChipDoHoaGPU);
            ViewBag.MaChip = new SelectList(db.ChipXuLies, "MaChip", "TenChip", thongSoBienTheDienThoai.MaChip);
            ViewBag.MaDoDay = new SelectList(db.DoDays, "MaDoDay", "MaDoDay", thongSoBienTheDienThoai.MaDoDay);
            ViewBag.MaDoPhanGiai = new SelectList(db.DoPhanGiaiManHinhs, "MaDoPhanGiai", "TenDoPhanGiai", thongSoBienTheDienThoai.MaDoPhanGiai);
            ViewBag.MaDungLuongPin = new SelectList(db.DungLuongPins, "MaDungLuongPin", "DungLuong", thongSoBienTheDienThoai.MaDungLuongPin);
            ViewBag.MaKhoiLuong = new SelectList(db.KhoiLuongs, "MaKhoiLuong", "MaKhoiLuong", thongSoBienTheDienThoai.MaKhoiLuong);
            ViewBag.MaKichThuocManHinh = new SelectList(db.KichThuocManHinhs, "MaKichThuocManHinh", "KichThuoc", thongSoBienTheDienThoai.MaKichThuocManHinh);
            ViewBag.MaPixelDoc = new SelectList(db.PixelDocs, "MaPixelDoc", "MaPixelDoc", thongSoBienTheDienThoai.MaPixelDoc);
            ViewBag.MaPixelNgang = new SelectList(db.PixelNgangs, "MaPixelNgang", "MaPixelNgang", thongSoBienTheDienThoai.MaPixelNgang);
            ViewBag.MaTanSoQuet = new SelectList(db.TanSoQuetManHinhs, "MaTanSoQuet", "MaTanSoQuet", thongSoBienTheDienThoai.MaTanSoQuet);
            ViewBag.MaTocDoCPU = new SelectList(db.TocDoCPUs, "MaTocDoCPU", "TocDo", thongSoBienTheDienThoai.MaTocDoCPU);
            return View(thongSoBienTheDienThoai);
        }

        // POST: ThongSoBienTheDienThoais_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTSBTDT,MaBT,MaChip,MaTocDoCPU,MaChipDoHoaGPU,DungLuongConLai,MaDoPhanGiai,MaPixelNgang,MaPixelDoc,MaKichThuocManHinh,MaTanSoQuet,MaDungLuongPin,MaChieuDai,MaChieuNgang,MaDoDay,MaKhoiLuong")] ThongSoBienTheDienThoai thongSoBienTheDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongSoBienTheDienThoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBT = new SelectList(db.BienTheSanPhams, "MaBT", "DonViTinh", thongSoBienTheDienThoai.MaBT);
            ViewBag.MaChieuDai = new SelectList(db.ChieuDais, "MaChieuDai", "MaChieuDai", thongSoBienTheDienThoai.MaChieuDai);
            ViewBag.MaChieuNgang = new SelectList(db.ChieuNgangs, "MaChieuNgang", "MaChieuNgang", thongSoBienTheDienThoai.MaChieuNgang);
            ViewBag.MaChipDoHoaGPU = new SelectList(db.ChipDoHoaGPUs, "MaChipDoHoaGPU", "TenChipDoHoaGPU", thongSoBienTheDienThoai.MaChipDoHoaGPU);
            ViewBag.MaChip = new SelectList(db.ChipXuLies, "MaChip", "TenChip", thongSoBienTheDienThoai.MaChip);
            ViewBag.MaDoDay = new SelectList(db.DoDays, "MaDoDay", "MaDoDay", thongSoBienTheDienThoai.MaDoDay);
            ViewBag.MaDoPhanGiai = new SelectList(db.DoPhanGiaiManHinhs, "MaDoPhanGiai", "TenDoPhanGiai", thongSoBienTheDienThoai.MaDoPhanGiai);
            ViewBag.MaDungLuongPin = new SelectList(db.DungLuongPins, "MaDungLuongPin", "DungLuong", thongSoBienTheDienThoai.MaDungLuongPin);
            ViewBag.MaKhoiLuong = new SelectList(db.KhoiLuongs, "MaKhoiLuong", "MaKhoiLuong", thongSoBienTheDienThoai.MaKhoiLuong);
            ViewBag.MaKichThuocManHinh = new SelectList(db.KichThuocManHinhs, "MaKichThuocManHinh", "KichThuoc", thongSoBienTheDienThoai.MaKichThuocManHinh);
            ViewBag.MaPixelDoc = new SelectList(db.PixelDocs, "MaPixelDoc", "MaPixelDoc", thongSoBienTheDienThoai.MaPixelDoc);
            ViewBag.MaPixelNgang = new SelectList(db.PixelNgangs, "MaPixelNgang", "MaPixelNgang", thongSoBienTheDienThoai.MaPixelNgang);
            ViewBag.MaTanSoQuet = new SelectList(db.TanSoQuetManHinhs, "MaTanSoQuet", "MaTanSoQuet", thongSoBienTheDienThoai.MaTanSoQuet);
            ViewBag.MaTocDoCPU = new SelectList(db.TocDoCPUs, "MaTocDoCPU", "TocDo", thongSoBienTheDienThoai.MaTocDoCPU);
            return View(thongSoBienTheDienThoai);
        }

        // GET: ThongSoBienTheDienThoais_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoBienTheDienThoai thongSoBienTheDienThoai = db.ThongSoBienTheDienThoais.Find(id);
            if (thongSoBienTheDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongSoBienTheDienThoai);
        }

        // POST: ThongSoBienTheDienThoais_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongSoBienTheDienThoai thongSoBienTheDienThoai = db.ThongSoBienTheDienThoais.Find(id);
            db.ThongSoBienTheDienThoais.Remove(thongSoBienTheDienThoai);
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
