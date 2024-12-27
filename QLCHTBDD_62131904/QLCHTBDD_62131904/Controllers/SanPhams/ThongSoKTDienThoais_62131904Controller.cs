using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Helpers;
using QLCHTBDD_62131904.Models;
using QLCHTBDD_62131904.ViewModels;

namespace QLCHTBDD_62131904.Controllers.SanPhams
{
    public class ThongSoKTDienThoais_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // get danh sach thong so kĩ thuật điện thoại
        private List<ThongSoKTDienThoaiViewModel> GetListThongSoKTDienThoaiViewModel()
        {
            return db.ThongSoKTDienThoais
                .Include(b => b.Bluetooth)
                .Include(b => b.ChatLieu)
                .Include(b => b.ChipDoHoaGPU)
                .Include(b => b.ChipXuLy)
                .Include(b => b.ChuanKhangBuiNuoc)
                .Include(b => b.CongKetNoiKhac)
                .Include(b => b.CongKetNoiSac)
                .Include(b => b.DinhDangGhiAm)
                .Include(b => b.JackTaiNghe)
                .Include(b => b.PhienBanHDH)
                .Include(b => b.SanPham)
                .Include(b => b.SIM)
                .Include(b => b.ThietKe)
                .Include(b => b.TocDoCPU)
                .Select(b => new ThongSoKTDienThoaiViewModel
                {
                    MaTSTK = b.MaTSKT,
                    TenSP = b.SanPham.TenSP,
                    //Bluetooth = b.Bluetooth.TenBluetooth,
                    //ChatLieu = b.ChatLieu.TenChatLieu,
                    //ChipDoHoaGPU = b.ChipDoHoaGPU.TenChipDoHoaGPU,
                    //ChipXuLy = b.ChipXuLy.TenChip,
                    //ChuanKhangBuiNuoc = b.ChuanKhangBuiNuoc.TenChuanKhangBuiNuoc,
                    //CongKetNoiKhac = b.CongKetNoiKhac.TenCongKetNoiKhac,
                    //CongKetNoiSac = b.CongKetNoiSac.TenCongKetNoiSac,
                    //DinhDangGhiAm = b.DinhDangGhiAm.TenDinhDangGhiAm,
                    //JackTaiNghe = b.JackTaiNghe.TenJackTaiNghe,
                    //PhienBanHDH = b.PhienBanHDH.TenPhienBanHDH,
                    //Sim = b.SIM.TenSIM,
                    //ThietKe = b.ThietKe.TenThietKe,
                    //TocDoCPU = b.TocDoCPU.TocDo,
                    //DungLuongConLai = b.DungLuongConLai,
                    //DanhBa = b.DanhBa == true ? "Có" : "Không",
                    //DenFlashCameraSau = b.DenFlashCameraSau ==  true ? "Có" : "Không",
                    ThoiDiemRaMat = b.ThoiDiemRaMat ?? DateTime.Now
                }).ToList();
        }

        // lấy sản phẩm
        private ThongSoKTDienThoaiViewModel GetThongSoKTDienThoaiViewModel(ThongSoKTDienThoai thongSo)
        {
            return new ThongSoKTDienThoaiViewModel
            {
                MaTSTK = thongSo.MaTSKT,
                TenSP = thongSo.SanPham.TenSP,
                Bluetooth = thongSo.Bluetooth?.TenBluetooth,
                ChatLieu = thongSo.ChatLieu?.TenChatLieu,
                ChipDoHoaGPU = thongSo.ChipDoHoaGPU?.TenChipDoHoaGPU,
                ChipXuLy = thongSo.ChipXuLy?.TenChip,
                ChuanKhangBuiNuoc = thongSo.ChuanKhangBuiNuoc?.TenChuanKhangBuiNuoc,
                CongKetNoiKhac = thongSo.CongKetNoiKhac?.TenCongKetNoiKhac,
                CongKetNoiSac = thongSo.CongKetNoiSac?.TenCongKetNoiSac,
                DinhDangGhiAm = thongSo.DinhDangGhiAm?.TenDinhDangGhiAm,
                JackTaiNghe = thongSo.JackTaiNghe?.TenJackTaiNghe,
                PhienBanHDH = thongSo.PhienBanHDH?.TenPhienBanHDH,
                Sim = thongSo.SIM?.TenSIM,
                ThietKe = thongSo.ThietKe?.TenThietKe,
                TocDoCPU = thongSo.TocDoCPU?.TocDo,
                DungLuongConLai = thongSo.DungLuongConLai,
                DanhBa = thongSo.DanhBa == true ? "Có" : "Không",
                DenFlashCameraSau = thongSo.DenFlashCameraSau == true ? "Có" : "Không",
                ThoiDiemRaMat = thongSo.ThoiDiemRaMat ?? DateTime.Now
            };
        }

        // create view model
        public ThongSoKTDienThoaiViewModel CreateThongSoKTDienThoaiViewModel(ThongSoKTDienThoai thongSo)
        {
            // Tạo ViewModel
            var viewModel = new ThongSoKTDienThoaiViewModel
            {
                MaTSTK = thongSo.MaTSKT,
                TenSP = db.SanPhams.FirstOrDefault(x => x.MaSP == thongSo.MaSP)?.TenSP,
                Bluetooth = db.Blueteeth.FirstOrDefault(x => x.MaBluetooth == thongSo.MaBluetooth)?.TenBluetooth,
                ChatLieu = db.ChatLieux.FirstOrDefault(x => x.MaChatLieu == thongSo.MaChatLieu)?.TenChatLieu,
                ChipDoHoaGPU = db.ChipDoHoaGPUs.FirstOrDefault(x => x.MaChipDoHoaGPU == thongSo.MaChipDoHoaGPU)?.TenChipDoHoaGPU,
                ChipXuLy = db.ChipXuLies.FirstOrDefault(x => x.MaChip == thongSo.MaChip)?.TenChip,
                ChuanKhangBuiNuoc = db.ChuanKhangBuiNuocs.FirstOrDefault(x => x.MaChuanKhangBuiNuoc == thongSo.MaChuanKhangBuiNuoc)?.TenChuanKhangBuiNuoc,
                CongKetNoiKhac = db.CongKetNoiKhacs.FirstOrDefault(x => x.MaCongKetNoiKhac == thongSo.MaCongKetNoiKhac)?.TenCongKetNoiKhac,
                CongKetNoiSac = db.CongKetNoiSacs.FirstOrDefault(x => x.MaCongKetNoiSac == thongSo.MaCongKetNoiSac)?.TenCongKetNoiSac,
                DinhDangGhiAm = db.DinhDangGhiAms.FirstOrDefault(x => x.MaDinhDangGhiAm == thongSo.MaDinhDangGhiAm)?.TenDinhDangGhiAm,
                JackTaiNghe = db.JackTaiNghes.FirstOrDefault(x => x.MaJackTaiNghe == thongSo.MaJackTaiNghe)?.TenJackTaiNghe,
                PhienBanHDH = db.PhienBanHDHs.FirstOrDefault(x => x.MaPhienBanHDH == thongSo.MaPhienBanHDH)?.TenPhienBanHDH,
                Sim = db.SIMs.FirstOrDefault(x => x.MaSIM == thongSo.MaSIM)?.TenSIM,
                ThietKe = db.ThietKes.FirstOrDefault(x => x.MaThietKe == thongSo.MaThietKe)?.TenThietKe,
                TocDoCPU = db.TocDoCPUs.FirstOrDefault(x => x.MaTocDoCPU == thongSo.MaTocDoCPU)?.TocDo,
                DungLuongConLai = thongSo.DungLuongConLai,
                DanhBa = thongSo.DanhBa ==  true ? "Có" : "Không",
                DenFlashCameraSau = thongSo.DenFlashCameraSau == true ? "Có" : "Không",
                ThoiDiemRaMat = thongSo.ThoiDiemRaMat ?? DateTime.Now
            };

            return viewModel;
        }

        // GET: ThongSoKTDienThoais_62131904
        public ActionResult Index()
        {
            var thongSoKTDienThoais = GetListThongSoKTDienThoaiViewModel();
            return View(thongSoKTDienThoais);
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
            var viewModel = new ThongSoKTDienThoaiViewModel();
            ViewBag.MaBluetooth = StaticEntityHelper.GetBluetooth();
            ViewBag.MaChatLieu = StaticEntityHelper.GetChatLieu(); ;
            ViewBag.MaChipDoHoaGPU = StaticEntityHelper.GetChipDoHoa();
            ViewBag.MaChipXuly = StaticEntityHelper.GetChipXuLy();
            ViewBag.MaChuanKhangBuiNuoc = StaticEntityHelper.GetChuanKhangBuiNuoc();
            ViewBag.MaCongKetNoiKhac = StaticEntityHelper.GetCongKetNoiKhac();
            ViewBag.MaCongKetNoiSac = StaticEntityHelper.GetCongKetNoiSac();
            ViewBag.MaDinhDangGhiAm = StaticEntityHelper.GetDinhDangGhiAm();
            ViewBag.MaJackTaiNghe = StaticEntityHelper.GetJackTaiNghe();
            ViewBag.MaPhienBanHDH = StaticEntityHelper.GetPhienBanHDH();
            ViewBag.MaSP = StaticEntityHelper.GetSanPham();
            ViewBag.MaSIM = StaticEntityHelper.GetSim();
            ViewBag.MaThietKe = StaticEntityHelper.GetThietKe();
            ViewBag.MaTocDoCPU = StaticEntityHelper.GetTocDoCPU();
            return View(viewModel);
        }

        // POST: ThongSoKTDienThoais_62131904/Create
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
            var viewModel = CreateThongSoKTDienThoaiViewModel(thongSoKTDienThoai);
            ViewBag.MaBluetooth = new SelectList(StaticEntityHelper.GetBluetooth(), "MaBluetooth", "TenBluetooth", thongSoKTDienThoai.MaBluetooth);
            ViewBag.MaChatLieu = new SelectList(StaticEntityHelper.GetChatLieu(), "MaChatLieu", "TenChatLieu", thongSoKTDienThoai.MaChatLieu);
            ViewBag.MaChipDoHoaGPU = new SelectList(StaticEntityHelper.GetChipDoHoa(), "MaChipDoHoaGPU", "TenChipDoHoaGPU", thongSoKTDienThoai.MaChipDoHoaGPU);
            ViewBag.MaChipXuly = new SelectList(StaticEntityHelper.GetChipXuLy(), "MaChip", "TenChip", thongSoKTDienThoai.MaChip);
            ViewBag.MaChuanKhangBuiNuoc = new SelectList(StaticEntityHelper.GetChuanKhangBuiNuoc(), "MaChuanKhangBuiNuoc", "TenChuanKhangBuiNuoc", thongSoKTDienThoai.MaChuanKhangBuiNuoc);
            ViewBag.MaCongKetNoiKhac = new SelectList(StaticEntityHelper.GetCongKetNoiKhac(), "MaCongKetNoiKhac", "TenCongKetNoiKhac", thongSoKTDienThoai.MaCongKetNoiKhac);
            ViewBag.MaCongKetNoiSac = new SelectList(StaticEntityHelper.GetCongKetNoiSac(), "MaCongKetNoiSac", "TenCongKetNoiSac", thongSoKTDienThoai.MaCongKetNoiSac);
            ViewBag.MaDinhDangGhiAm = new SelectList(StaticEntityHelper.GetDinhDangGhiAm(), "MaDinhDangGhiAm", "TenDinhDangGhiAm", thongSoKTDienThoai.MaDinhDangGhiAm);
            ViewBag.MaJackTaiNghe = new SelectList(StaticEntityHelper.GetJackTaiNghe(), "MaJackTaiNghe", "TenJackTaiNghe", thongSoKTDienThoai.MaJackTaiNghe);
            ViewBag.MaPhienBanHDH = new SelectList(StaticEntityHelper.GetPhienBanHDH(), "MaPhienBanHDH", "TenPhienBanHDH", thongSoKTDienThoai.MaPhienBanHDH);
            ViewBag.MaSP = new SelectList(StaticEntityHelper.GetSanPham(), "MaSP", "TenSP", thongSoKTDienThoai.MaSP);
            ViewBag.MaSIM = new SelectList(StaticEntityHelper.GetSim(), "MaSIM", "TenSIM", thongSoKTDienThoai.MaSIM);
            ViewBag.MaThietKe = new SelectList(StaticEntityHelper.GetThietKe(), "MaThietKe", "TenThietKe", thongSoKTDienThoai.MaThietKe);
            ViewBag.MaTocDoCPU = new SelectList(StaticEntityHelper.GetTocDoCPU(), "MaTocDoCPU", "TocDo", thongSoKTDienThoai.MaTocDoCPU);
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
            ViewBag.MaChipXuly = new SelectList(db.ChipXuLies, "MaChip", "TenChip", thongSoKTDienThoai.MaChip);
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
