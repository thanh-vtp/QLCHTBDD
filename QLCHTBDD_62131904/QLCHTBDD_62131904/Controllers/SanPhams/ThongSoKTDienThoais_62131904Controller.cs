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
    public class ThongSoKTDienThoais_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: ThongSoKTDienThoais_62131904
        public ActionResult Index()
        {
            var thongSoKTDienThoais = db.ThongSoKTDienThoais.Include(t => t.Bluetooth).Include(t => t.ChatLieu).Include(t => t.ChipDoHoaGPU).Include(t => t.ChipXuLy).Include(t => t.ChuanKhangBuiNuoc).Include(t => t.CongKetNoiKhac).Include(t => t.CongKetNoiSac).Include(t => t.DinhDangGhiAm).Include(t => t.JackTaiNghe).Include(t => t.PhienBanHDH).Include(t => t.SanPham).Include(t => t.SIM).Include(t => t.ThietKe).Include(t => t.TocDoCPU);
            return View(thongSoKTDienThoais.ToList());
        }

        // GET: ThongSoKTDienThoais_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoKTDienThoai thongSoKTDienThoai = db.ThongSoKTDienThoais.Find(id);
            if (thongSoKTDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongSoKTDienThoai);
        }

        // GET: ThongSoKTDienThoais_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaBluetooth = new SelectList(db.Blueteeth, "MaBluetooth", "TenBluetooth");
            ViewBag.MaChatLieu = new SelectList(db.ChatLieux, "MaChatLieu", "TenChatLieu");
            ViewBag.MaChipDoHoaGPU = new SelectList(db.ChipDoHoaGPUs, "MaChipDoHoaGPU", "TenChipDoHoaGPU");
            ViewBag.MaChip = new SelectList(db.ChipXuLies, "MaChip", "TenChip");
            ViewBag.MaChuanKhangBuiNuoc = new SelectList(db.ChuanKhangBuiNuocs, "MaChuanKhangBuiNuoc", "TenChuanKhangBuiNuoc");
            ViewBag.MaCongKetNoiKhac = new SelectList(db.CongKetNoiKhacs, "MaCongKetNoiKhac", "TenCongKetNoiKhac");
            ViewBag.MaCongKetNoiSac = new SelectList(db.CongKetNoiSacs, "MaCongKetNoiSac", "TenCongKetNoiSac");
            ViewBag.MaDinhDangGhiAm = new SelectList(db.DinhDangGhiAms, "MaDinhDangGhiAm", "TenDinhDangGhiAm");
            ViewBag.MaJackTaiNghe = new SelectList(db.JackTaiNghes, "MaJackTaiNghe", "TenJackTaiNghe");
            ViewBag.MaPhienBanHDH = new SelectList(db.PhienBanHDHs, "MaPhienBanHDH", "TenPhienBanHDH");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            ViewBag.MaSIM = new SelectList(db.SIMs, "MaSIM", "TenSIM");
            ViewBag.MaThietKe = new SelectList(db.ThietKes, "MaThietKe", "TenThietKe");
            ViewBag.MaTocDoCPU = new SelectList(db.TocDoCPUs, "MaTocDoCPU", "TocDo");
            return View();
        }

        // POST: ThongSoKTDienThoais_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTSKT,MaSP,MaPhienBanHDH,MaChip,MaTocDoCPU,MaChipDoHoaGPU,DungLuongConLai,DanhBa,DenFlashCameraSau,MaChuanKhangBuiNuoc,MaDinhDangGhiAm,MaSIM,MaBluetooth,MaCongKetNoiSac,MaJackTaiNghe,MaCongKetNoiKhac,MaThietKe,MaChatLieu,ThoiDiemRaMat")] ThongSoKTDienThoai thongSoKTDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.ThongSoKTDienThoais.Add(thongSoKTDienThoai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaBluetooth = new SelectList(db.Blueteeth, "MaBluetooth", "TenBluetooth", thongSoKTDienThoai.MaBluetooth);
            ViewBag.MaChatLieu = new SelectList(db.ChatLieux, "MaChatLieu", "TenChatLieu", thongSoKTDienThoai.MaChatLieu);
            ViewBag.MaChipDoHoaGPU = new SelectList(db.ChipDoHoaGPUs, "MaChipDoHoaGPU", "TenChipDoHoaGPU", thongSoKTDienThoai.MaChipDoHoaGPU);
            ViewBag.MaChip = new SelectList(db.ChipXuLies, "MaChip", "TenChip", thongSoKTDienThoai.MaChip);
            ViewBag.MaChuanKhangBuiNuoc = new SelectList(db.ChuanKhangBuiNuocs, "MaChuanKhangBuiNuoc", "TenChuanKhangBuiNuoc", thongSoKTDienThoai.MaChuanKhangBuiNuoc);
            ViewBag.MaCongKetNoiKhac = new SelectList(db.CongKetNoiKhacs, "MaCongKetNoiKhac", "TenCongKetNoiKhac", thongSoKTDienThoai.MaCongKetNoiKhac);
            ViewBag.MaCongKetNoiSac = new SelectList(db.CongKetNoiSacs, "MaCongKetNoiSac", "TenCongKetNoiSac", thongSoKTDienThoai.MaCongKetNoiSac);
            ViewBag.MaDinhDangGhiAm = new SelectList(db.DinhDangGhiAms, "MaDinhDangGhiAm", "TenDinhDangGhiAm", thongSoKTDienThoai.MaDinhDangGhiAm);
            ViewBag.MaJackTaiNghe = new SelectList(db.JackTaiNghes, "MaJackTaiNghe", "TenJackTaiNghe", thongSoKTDienThoai.MaJackTaiNghe);
            ViewBag.MaPhienBanHDH = new SelectList(db.PhienBanHDHs, "MaPhienBanHDH", "TenPhienBanHDH", thongSoKTDienThoai.MaPhienBanHDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", thongSoKTDienThoai.MaSP);
            ViewBag.MaSIM = new SelectList(db.SIMs, "MaSIM", "TenSIM", thongSoKTDienThoai.MaSIM);
            ViewBag.MaThietKe = new SelectList(db.ThietKes, "MaThietKe", "TenThietKe", thongSoKTDienThoai.MaThietKe);
            ViewBag.MaTocDoCPU = new SelectList(db.TocDoCPUs, "MaTocDoCPU", "TocDo", thongSoKTDienThoai.MaTocDoCPU);
            return View(thongSoKTDienThoai);
        }

        // GET: ThongSoKTDienThoais_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoKTDienThoai thongSoKTDienThoai = db.ThongSoKTDienThoais.Find(id);
            if (thongSoKTDienThoai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBluetooth = new SelectList(db.Blueteeth, "MaBluetooth", "TenBluetooth", thongSoKTDienThoai.MaBluetooth);
            ViewBag.MaChatLieu = new SelectList(db.ChatLieux, "MaChatLieu", "TenChatLieu", thongSoKTDienThoai.MaChatLieu);
            ViewBag.MaChipDoHoaGPU = new SelectList(db.ChipDoHoaGPUs, "MaChipDoHoaGPU", "TenChipDoHoaGPU", thongSoKTDienThoai.MaChipDoHoaGPU);
            ViewBag.MaChip = new SelectList(db.ChipXuLies, "MaChip", "TenChip", thongSoKTDienThoai.MaChip);
            ViewBag.MaChuanKhangBuiNuoc = new SelectList(db.ChuanKhangBuiNuocs, "MaChuanKhangBuiNuoc", "TenChuanKhangBuiNuoc", thongSoKTDienThoai.MaChuanKhangBuiNuoc);
            ViewBag.MaCongKetNoiKhac = new SelectList(db.CongKetNoiKhacs, "MaCongKetNoiKhac", "TenCongKetNoiKhac", thongSoKTDienThoai.MaCongKetNoiKhac);
            ViewBag.MaCongKetNoiSac = new SelectList(db.CongKetNoiSacs, "MaCongKetNoiSac", "TenCongKetNoiSac", thongSoKTDienThoai.MaCongKetNoiSac);
            ViewBag.MaDinhDangGhiAm = new SelectList(db.DinhDangGhiAms, "MaDinhDangGhiAm", "TenDinhDangGhiAm", thongSoKTDienThoai.MaDinhDangGhiAm);
            ViewBag.MaJackTaiNghe = new SelectList(db.JackTaiNghes, "MaJackTaiNghe", "TenJackTaiNghe", thongSoKTDienThoai.MaJackTaiNghe);
            ViewBag.MaPhienBanHDH = new SelectList(db.PhienBanHDHs, "MaPhienBanHDH", "TenPhienBanHDH", thongSoKTDienThoai.MaPhienBanHDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", thongSoKTDienThoai.MaSP);
            ViewBag.MaSIM = new SelectList(db.SIMs, "MaSIM", "TenSIM", thongSoKTDienThoai.MaSIM);
            ViewBag.MaThietKe = new SelectList(db.ThietKes, "MaThietKe", "TenThietKe", thongSoKTDienThoai.MaThietKe);
            ViewBag.MaTocDoCPU = new SelectList(db.TocDoCPUs, "MaTocDoCPU", "TocDo", thongSoKTDienThoai.MaTocDoCPU);
            return View(thongSoKTDienThoai);
        }

        // POST: ThongSoKTDienThoais_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTSKT,MaSP,MaPhienBanHDH,MaChip,MaTocDoCPU,MaChipDoHoaGPU,DungLuongConLai,DanhBa,DenFlashCameraSau,MaChuanKhangBuiNuoc,MaDinhDangGhiAm,MaSIM,MaBluetooth,MaCongKetNoiSac,MaJackTaiNghe,MaCongKetNoiKhac,MaThietKe,MaChatLieu,ThoiDiemRaMat")] ThongSoKTDienThoai thongSoKTDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongSoKTDienThoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBluetooth = new SelectList(db.Blueteeth, "MaBluetooth", "TenBluetooth", thongSoKTDienThoai.MaBluetooth);
            ViewBag.MaChatLieu = new SelectList(db.ChatLieux, "MaChatLieu", "TenChatLieu", thongSoKTDienThoai.MaChatLieu);
            ViewBag.MaChipDoHoaGPU = new SelectList(db.ChipDoHoaGPUs, "MaChipDoHoaGPU", "TenChipDoHoaGPU", thongSoKTDienThoai.MaChipDoHoaGPU);
            ViewBag.MaChip = new SelectList(db.ChipXuLies, "MaChip", "TenChip", thongSoKTDienThoai.MaChip);
            ViewBag.MaChuanKhangBuiNuoc = new SelectList(db.ChuanKhangBuiNuocs, "MaChuanKhangBuiNuoc", "TenChuanKhangBuiNuoc", thongSoKTDienThoai.MaChuanKhangBuiNuoc);
            ViewBag.MaCongKetNoiKhac = new SelectList(db.CongKetNoiKhacs, "MaCongKetNoiKhac", "TenCongKetNoiKhac", thongSoKTDienThoai.MaCongKetNoiKhac);
            ViewBag.MaCongKetNoiSac = new SelectList(db.CongKetNoiSacs, "MaCongKetNoiSac", "TenCongKetNoiSac", thongSoKTDienThoai.MaCongKetNoiSac);
            ViewBag.MaDinhDangGhiAm = new SelectList(db.DinhDangGhiAms, "MaDinhDangGhiAm", "TenDinhDangGhiAm", thongSoKTDienThoai.MaDinhDangGhiAm);
            ViewBag.MaJackTaiNghe = new SelectList(db.JackTaiNghes, "MaJackTaiNghe", "TenJackTaiNghe", thongSoKTDienThoai.MaJackTaiNghe);
            ViewBag.MaPhienBanHDH = new SelectList(db.PhienBanHDHs, "MaPhienBanHDH", "TenPhienBanHDH", thongSoKTDienThoai.MaPhienBanHDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", thongSoKTDienThoai.MaSP);
            ViewBag.MaSIM = new SelectList(db.SIMs, "MaSIM", "TenSIM", thongSoKTDienThoai.MaSIM);
            ViewBag.MaThietKe = new SelectList(db.ThietKes, "MaThietKe", "TenThietKe", thongSoKTDienThoai.MaThietKe);
            ViewBag.MaTocDoCPU = new SelectList(db.TocDoCPUs, "MaTocDoCPU", "TocDo", thongSoKTDienThoai.MaTocDoCPU);
            return View(thongSoKTDienThoai);
        }

        // GET: ThongSoKTDienThoais_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoKTDienThoai thongSoKTDienThoai = db.ThongSoKTDienThoais.Find(id);
            if (thongSoKTDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongSoKTDienThoai);
        }

        // POST: ThongSoKTDienThoais_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongSoKTDienThoai thongSoKTDienThoai = db.ThongSoKTDienThoais.Find(id);
            db.ThongSoKTDienThoais.Remove(thongSoKTDienThoai);
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
