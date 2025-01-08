using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Models;

namespace QLCHTBDD_62131904.Controllers.QuanLySanPhams.SanPhams
{
    public class ThongSoDienThoais_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: ThongSoDienThoais_62131904
        public ActionResult Index()
        {
            var thongSoDienThoais = db.ThongSoDienThoais
                .Include(t => t.Bluetooth)
                .Include(t => t.ChatLieu)
                .Include(t => t.ChuanKhangBuiNuoc)
                .Include(t => t.CongKetNoiKhac)
                .Include(t => t.CongKetNoiSac)
                .Include(t => t.CongNgheManHinh)
                .Include(t => t.DinhDangGhiAm)
                .Include(t => t.DoSangToiDa)
                .Include(t => t.HoTroSacToiDa)
                .Include(t => t.JackTaiNghe)
                .Include(t => t.LoaiKinhCuongLuc)
                .Include(t => t.LoaiPin)
                .Include(t => t.MangDiDong)
                .Include(t => t.PhienBanHDH)
                .Include(t => t.SanPham)
                .Include(t => t.SIM)
                .Include(t => t.ThietKe)
                .Include(t => t.TinhNangBaoMat);
            return View(thongSoDienThoais.ToList());
        }

        // GET: ThongSoDienThoais_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoDienThoai thongSoDienThoai = db.ThongSoDienThoais.Find(id);
            if (thongSoDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongSoDienThoai);
        }

        // GET: ThongSoDienThoais_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaBluetooth = new SelectList(db.Blueteeth, "MaBluetooth", "TenBluetooth");
            ViewBag.MaChatLieu = new SelectList(db.ChatLieux, "MaChatLieu", "TenChatLieu");
            ViewBag.MaChuanKhangBuiNuoc = new SelectList(db.ChuanKhangBuiNuocs, "MaChuanKhangBuiNuoc", "TenChuanKhangBuiNuoc");
            ViewBag.MaCongKetNoiKhac = new SelectList(db.CongKetNoiKhacs, "MaCongKetNoiKhac", "TenCongKetNoiKhac");
            ViewBag.MaCongKetNoiSac = new SelectList(db.CongKetNoiSacs, "MaCongKetNoiSac", "TenCongKetNoiSac");
            ViewBag.MaCongNgheManHinh = new SelectList(db.CongNgheManHinhs, "MaCongNgheManHinh", "TenCongNgheManHinh");
            ViewBag.MaDinhDangGhiAm = new SelectList(db.DinhDangGhiAms, "MaDinhDangGhiAm", "TenDinhDangGhiAm");
            ViewBag.MaDoSang = new SelectList(db.DoSangToiDas, "MaDoSang", "DoSang");
            ViewBag.MaHoTroSac = new SelectList(db.HoTroSacToiDas, "MaHoTroSac", "CongSuat");
            ViewBag.MaJackTaiNghe = new SelectList(db.JackTaiNghes, "MaJackTaiNghe", "TenJackTaiNghe");
            ViewBag.MaLoaiKinh = new SelectList(db.LoaiKinhCuongLucs, "MaLoaiKinh", "TenLoaiKinh");
            ViewBag.MaLoaiPin = new SelectList(db.LoaiPins, "MaLoaiPin", "TenLoaiPin");
            ViewBag.MaMangDiDong = new SelectList(db.MangDiDongs, "MaMangDiDong", "TenMangDiDong");
            ViewBag.MaPhienBanHDH = new SelectList(db.PhienBanHDHs, "MaPhienBanHDH", "TenPhienBanHDH");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            ViewBag.MaSIM = new SelectList(db.SIMs, "MaSIM", "TenSIM");
            ViewBag.MaThietKe = new SelectList(db.ThietKes, "MaThietKe", "TenThietKe");
            ViewBag.MaTinhNangBaoMat = new SelectList(db.TinhNangBaoMats, "MaTinhNangBaoMat", "TenTinhNangBaoMat");
            return View();
        }

        // POST: ThongSoDienThoais_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTSDT,MaSP,MaPhienBanHDH,DanhBa,DenFlashCameraSau,MaChuanKhangBuiNuoc,MaCongNgheManHinh,MaDoSang,MaLoaiKinh,MaLoaiPin,MaHoTroSac,MaTinhNangBaoMat,MaDinhDangGhiAm,MaMangDiDong,MaSIM,MaBluetooth,MaCongKetNoiSac,MaJackTaiNghe,MaCongKetNoiKhac,MaThietKe,MaChatLieu,ThoiDiemRaMat")] ThongSoDienThoai thongSoDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.ThongSoDienThoais.Add(thongSoDienThoai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaBluetooth = new SelectList(db.Blueteeth, "MaBluetooth", "TenBluetooth", thongSoDienThoai.MaBluetooth);
            ViewBag.MaChatLieu = new SelectList(db.ChatLieux, "MaChatLieu", "TenChatLieu", thongSoDienThoai.MaChatLieu);
            ViewBag.MaChuanKhangBuiNuoc = new SelectList(db.ChuanKhangBuiNuocs, "MaChuanKhangBuiNuoc", "TenChuanKhangBuiNuoc", thongSoDienThoai.MaChuanKhangBuiNuoc);
            ViewBag.MaCongKetNoiKhac = new SelectList(db.CongKetNoiKhacs, "MaCongKetNoiKhac", "TenCongKetNoiKhac", thongSoDienThoai.MaCongKetNoiKhac);
            ViewBag.MaCongKetNoiSac = new SelectList(db.CongKetNoiSacs, "MaCongKetNoiSac", "TenCongKetNoiSac", thongSoDienThoai.MaCongKetNoiSac);
            ViewBag.MaCongNgheManHinh = new SelectList(db.CongNgheManHinhs, "MaCongNgheManHinh", "TenCongNgheManHinh", thongSoDienThoai.MaCongNgheManHinh);
            ViewBag.MaDinhDangGhiAm = new SelectList(db.DinhDangGhiAms, "MaDinhDangGhiAm", "TenDinhDangGhiAm", thongSoDienThoai.MaDinhDangGhiAm);
            ViewBag.MaDoSang = new SelectList(db.DoSangToiDas, "MaDoSang", "DoSang", thongSoDienThoai.MaDoSang);
            ViewBag.MaHoTroSac = new SelectList(db.HoTroSacToiDas, "MaHoTroSac", "CongSuat", thongSoDienThoai.MaHoTroSac);
            ViewBag.MaJackTaiNghe = new SelectList(db.JackTaiNghes, "MaJackTaiNghe", "TenJackTaiNghe", thongSoDienThoai.MaJackTaiNghe);
            ViewBag.MaLoaiKinh = new SelectList(db.LoaiKinhCuongLucs, "MaLoaiKinh", "TenLoaiKinh", thongSoDienThoai.MaLoaiKinh);
            ViewBag.MaLoaiPin = new SelectList(db.LoaiPins, "MaLoaiPin", "TenLoaiPin", thongSoDienThoai.MaLoaiPin);
            ViewBag.MaMangDiDong = new SelectList(db.MangDiDongs, "MaMangDiDong", "TenMangDiDong", thongSoDienThoai.MaMangDiDong);
            ViewBag.MaPhienBanHDH = new SelectList(db.PhienBanHDHs, "MaPhienBanHDH", "TenPhienBanHDH", thongSoDienThoai.MaPhienBanHDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", thongSoDienThoai.MaSP);
            ViewBag.MaSIM = new SelectList(db.SIMs, "MaSIM", "TenSIM", thongSoDienThoai.MaSIM);
            ViewBag.MaThietKe = new SelectList(db.ThietKes, "MaThietKe", "TenThietKe", thongSoDienThoai.MaThietKe);
            ViewBag.MaTinhNangBaoMat = new SelectList(db.TinhNangBaoMats, "MaTinhNangBaoMat", "TenTinhNangBaoMat", thongSoDienThoai.MaTinhNangBaoMat);
            return View(thongSoDienThoai);
        }

        // GET: ThongSoDienThoais_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoDienThoai thongSoDienThoai = db.ThongSoDienThoais.Find(id);
            if (thongSoDienThoai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBluetooth = new SelectList(db.Blueteeth, "MaBluetooth", "TenBluetooth", thongSoDienThoai.MaBluetooth);
            ViewBag.MaChatLieu = new SelectList(db.ChatLieux, "MaChatLieu", "TenChatLieu", thongSoDienThoai.MaChatLieu);
            ViewBag.MaChuanKhangBuiNuoc = new SelectList(db.ChuanKhangBuiNuocs, "MaChuanKhangBuiNuoc", "TenChuanKhangBuiNuoc", thongSoDienThoai.MaChuanKhangBuiNuoc);
            ViewBag.MaCongKetNoiKhac = new SelectList(db.CongKetNoiKhacs, "MaCongKetNoiKhac", "TenCongKetNoiKhac", thongSoDienThoai.MaCongKetNoiKhac);
            ViewBag.MaCongKetNoiSac = new SelectList(db.CongKetNoiSacs, "MaCongKetNoiSac", "TenCongKetNoiSac", thongSoDienThoai.MaCongKetNoiSac);
            ViewBag.MaCongNgheManHinh = new SelectList(db.CongNgheManHinhs, "MaCongNgheManHinh", "TenCongNgheManHinh", thongSoDienThoai.MaCongNgheManHinh);
            ViewBag.MaDinhDangGhiAm = new SelectList(db.DinhDangGhiAms, "MaDinhDangGhiAm", "TenDinhDangGhiAm", thongSoDienThoai.MaDinhDangGhiAm);
            ViewBag.MaDoSang = new SelectList(db.DoSangToiDas, "MaDoSang", "MaDoSang", thongSoDienThoai.MaDoSang);
            ViewBag.MaHoTroSac = new SelectList(db.HoTroSacToiDas, "MaHoTroSac", "CongSuat", thongSoDienThoai.MaHoTroSac);
            ViewBag.MaJackTaiNghe = new SelectList(db.JackTaiNghes, "MaJackTaiNghe", "TenJackTaiNghe", thongSoDienThoai.MaJackTaiNghe);
            ViewBag.MaLoaiKinh = new SelectList(db.LoaiKinhCuongLucs, "MaLoaiKinh", "TenLoaiKinh", thongSoDienThoai.MaLoaiKinh);
            ViewBag.MaLoaiPin = new SelectList(db.LoaiPins, "MaLoaiPin", "TenLoaiPin", thongSoDienThoai.MaLoaiPin);
            ViewBag.MaMangDiDong = new SelectList(db.MangDiDongs, "MaMangDiDong", "TenMangDiDong", thongSoDienThoai.MaMangDiDong);
            ViewBag.MaPhienBanHDH = new SelectList(db.PhienBanHDHs, "MaPhienBanHDH", "TenPhienBanHDH", thongSoDienThoai.MaPhienBanHDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", thongSoDienThoai.MaSP);
            ViewBag.MaSIM = new SelectList(db.SIMs, "MaSIM", "TenSIM", thongSoDienThoai.MaSIM);
            ViewBag.MaThietKe = new SelectList(db.ThietKes, "MaThietKe", "TenThietKe", thongSoDienThoai.MaThietKe);
            ViewBag.MaTinhNangBaoMat = new SelectList(db.TinhNangBaoMats, "MaTinhNangBaoMat", "TenTinhNangBaoMat", thongSoDienThoai.MaTinhNangBaoMat);
            return View(thongSoDienThoai);
        }

        // POST: ThongSoDienThoais_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTSDT,MaSP,MaPhienBanHDH,DanhBa,DenFlashCameraSau,MaChuanKhangBuiNuoc,MaCongNgheManHinh,MaDoSang,MaLoaiKinh,MaLoaiPin,MaHoTroSac,MaTinhNangBaoMat,MaDinhDangGhiAm,MaMangDiDong,MaSIM,MaBluetooth,MaCongKetNoiSac,MaJackTaiNghe,MaCongKetNoiKhac,MaThietKe,MaChatLieu,ThoiDiemRaMat")] ThongSoDienThoai thongSoDienThoai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongSoDienThoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBluetooth = new SelectList(db.Blueteeth, "MaBluetooth", "TenBluetooth", thongSoDienThoai.MaBluetooth);
            ViewBag.MaChatLieu = new SelectList(db.ChatLieux, "MaChatLieu", "TenChatLieu", thongSoDienThoai.MaChatLieu);
            ViewBag.MaChuanKhangBuiNuoc = new SelectList(db.ChuanKhangBuiNuocs, "MaChuanKhangBuiNuoc", "TenChuanKhangBuiNuoc", thongSoDienThoai.MaChuanKhangBuiNuoc);
            ViewBag.MaCongKetNoiKhac = new SelectList(db.CongKetNoiKhacs, "MaCongKetNoiKhac", "TenCongKetNoiKhac", thongSoDienThoai.MaCongKetNoiKhac);
            ViewBag.MaCongKetNoiSac = new SelectList(db.CongKetNoiSacs, "MaCongKetNoiSac", "TenCongKetNoiSac", thongSoDienThoai.MaCongKetNoiSac);
            ViewBag.MaCongNgheManHinh = new SelectList(db.CongNgheManHinhs, "MaCongNgheManHinh", "TenCongNgheManHinh", thongSoDienThoai.MaCongNgheManHinh);
            ViewBag.MaDinhDangGhiAm = new SelectList(db.DinhDangGhiAms, "MaDinhDangGhiAm", "TenDinhDangGhiAm", thongSoDienThoai.MaDinhDangGhiAm);
            ViewBag.MaDoSang = new SelectList(db.DoSangToiDas, "MaDoSang", "MaDoSang", thongSoDienThoai.MaDoSang);
            ViewBag.MaHoTroSac = new SelectList(db.HoTroSacToiDas, "MaHoTroSac", "CongSuat", thongSoDienThoai.MaHoTroSac);
            ViewBag.MaJackTaiNghe = new SelectList(db.JackTaiNghes, "MaJackTaiNghe", "TenJackTaiNghe", thongSoDienThoai.MaJackTaiNghe);
            ViewBag.MaLoaiKinh = new SelectList(db.LoaiKinhCuongLucs, "MaLoaiKinh", "TenLoaiKinh", thongSoDienThoai.MaLoaiKinh);
            ViewBag.MaLoaiPin = new SelectList(db.LoaiPins, "MaLoaiPin", "TenLoaiPin", thongSoDienThoai.MaLoaiPin);
            ViewBag.MaMangDiDong = new SelectList(db.MangDiDongs, "MaMangDiDong", "TenMangDiDong", thongSoDienThoai.MaMangDiDong);
            ViewBag.MaPhienBanHDH = new SelectList(db.PhienBanHDHs, "MaPhienBanHDH", "TenPhienBanHDH", thongSoDienThoai.MaPhienBanHDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", thongSoDienThoai.MaSP);
            ViewBag.MaSIM = new SelectList(db.SIMs, "MaSIM", "TenSIM", thongSoDienThoai.MaSIM);
            ViewBag.MaThietKe = new SelectList(db.ThietKes, "MaThietKe", "TenThietKe", thongSoDienThoai.MaThietKe);
            ViewBag.MaTinhNangBaoMat = new SelectList(db.TinhNangBaoMats, "MaTinhNangBaoMat", "TenTinhNangBaoMat", thongSoDienThoai.MaTinhNangBaoMat);
            return View(thongSoDienThoai);
        }

        // GET: ThongSoDienThoais_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongSoDienThoai thongSoDienThoai = db.ThongSoDienThoais.Find(id);
            if (thongSoDienThoai == null)
            {
                return HttpNotFound();
            }
            return View(thongSoDienThoai);
        }

        // POST: ThongSoDienThoais_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongSoDienThoai thongSoDienThoai = db.ThongSoDienThoais.Find(id);
            db.ThongSoDienThoais.Remove(thongSoDienThoai);
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
