using QLCHTBDD_62131904.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace QLCHTBDD_62131904.Helpers
{
    public static class StaticEntityHelper
    {
        // Phương thức chung để lấy SelectList
        public static SelectList GetSelectList<T>(IQueryable<T> query, string valueField, string textField)
        {
            return new SelectList(query.ToList(), valueField, textField);
        }

        // Phương thức động để lấy SelectList từ các bảng khác nhau
        public static SelectList GetDynamicSelectList(string tableName, string valueField, string textField)
        {
            using (var db = new QLCHTBDD_62131904Entities())
            {
                IQueryable<dynamic> query;

                switch (tableName)
                {
                    case "QuocGias":
                        query = db.QuocGias.Where(qg => qg.IsActive == true);
                        break;
                    case "HangSanXuats":
                        query = db.HangSanXuats.Where(h => h.IsActive == true);
                        break;
                    case "LoaiSanPhams":
                        query = db.LoaiSanPhams.Where(lsp => lsp.IsActive == true);
                        break;
                    case "RAMs":
                        query = db.RAMs.Where(ram => ram.IsActive == true);
                        break;
                    case "ROMs":
                        query = db.ROMs.Where(rom => rom.IsActive == true);
                        break;
                    case "MauSacs":
                        query = db.MauSacs.Where(ms => ms.IsActive == true);
                        break;
                    case "PhienBanHDHs":
                        query = db.PhienBanHDHs.Where(pbh => pbh.IsActive == true);
                        break;
                    case "ChipXuLies":
                        query = db.ChipXuLies.Where(chip => chip.IsActive == true);
                        break;
                    case "ChipDoHoaGPUs":
                        query = db.ChipDoHoaGPUs.Where(gpu => gpu.IsActive == true);
                        break;
                    case "TocDoCPUs":
                        query = db.TocDoCPUs.Where(cpu => cpu.IsActive == true);
                        break;
                    case "ChuanKhangBuiNuocs":
                        query = db.ChuanKhangBuiNuocs.Where(ckbn => ckbn.IsActive == true);
                        break;
                    case "TinhNangs":
                        query = db.TinhNangs.Where(tn => tn.IsActive == true);
                        break;
                    case "SIMs":
                        query = db.SIMs.Where(sim => sim.IsActive == true);
                        break;
                    case "Blueteeth":
                        query = db.Blueteeth.Where(bt => bt.IsActive == true);
                        break;
                    case "Wifis":
                        query = db.Wifis.Where(wf => wf.IsActive == true);
                        break;
                    case "LoaiCameras":
                        query = db.LoaiCameras.Where(cam => cam.IsActive == true);
                        break;
                    case "DoPhanGiaiCameras":
                        query = db.DoPhanGiaiCameras.Where(dpg => dpg.IsActive == true);
                        break;
                    case "DoPhanGiaiQuayPhims":
                        query = db.DoPhanGiaiQuayPhims.Where(dpqp => dpqp.IsActive == true);
                        break;
                    case "TanSoQuetManHinhs":
                        query = db.TanSoQuetManHinhs.Where(tsqm => tsqm.IsActive == true);
                        break;
                    case "LoaiPins":
                        query = db.LoaiPins.Where(lp => lp.IsActive == true);
                        break;
                    case "DungLuongPins":
                        query = db.DungLuongPins.Where(dlp => dlp.IsActive == true);
                        break;
                    case "ThietKes":
                        query = db.ThietKes.Where(tk => tk.IsActive == true);
                        break;
                    case "ChatLieux":
                        query = db.ChatLieux.Where(cl => cl.IsActive == true);
                        break;
                    case "TocDoKhungHinhs":
                        query = db.TocDoKhungHinhs.Where(x => x.IsActive == true);
                        break;
                    case "CongNgheManHinhs":
                        query = db.CongNgheManHinhs.Where(x => x.IsActive == true);
                        break;
                    case "DoPhanGiaiManHinhs":
                        query = db.DoPhanGiaiManHinhs.Where(x => x.IsActive == true);
                        break;
                    case "KichThuocManHinhs":
                        query = db.KichThuocManHinhs.Where(x => x.IsActive == true);
                        break;
                    case "DoSangToiDas":
                        query = db.DoSangToiDas.Where(x => x.IsActive == true);
                        break;
                    case "LoaiKinhCuongLucs":
                        query = db.LoaiKinhCuongLucs.Where(x => x.IsActive == true);
                        break;
                    case "HoTroSacToiDas":
                        query = db.HoTroSacToiDas.Where(x => x.IsActive == true);
                        break;
                    case "CongNghePins":
                        query = db.CongNghePins.Where(x => x.IsActive == true);
                        break;
                    case "TinhNangBaoMats":
                        query = db.TinhNangBaoMats.Where(x => x.IsActive == true);
                        break;
                    case "TinhNangDacBiets":
                        query = db.TinhNangDacBiets.Where(x => x.IsActive == true);
                        break;
                    case "DinhDangGhiAms":
                        query = db.DinhDangGhiAms.Where(x => x.IsActive == true);
                        break;
                    case "DinhDangPhims":
                        query = db.DinhDangPhims.Where(x => x.IsActive == true);
                        break;
                    case "DinhDangNhacs":
                        query = db.DinhDangNhacs.Where(x => x.IsActive == true);
                        break;
                    case "MangDiDongs":
                        query = db.MangDiDongs.Where(x => x.IsActive == true);
                        break;
                    case "HeThongDinhViGPS":
                        query = db.HeThongDinhViGPS.Where(x => x.IsActive == true);
                        break;
                    case "CongKetNoiSacs":
                        query = db.CongKetNoiSacs.Where(x => x.IsActive == true);
                        break;
                    case "JackTaiNghes":
                        query = db.JackTaiNghes.Where(x => x.IsActive == true);
                        break;
                    case "CongKetNoiKhacs":
                        query = db.CongKetNoiKhacs.Where(x => x.IsActive == true);
                        break;
                    case "SanPhams":
                        query = db.SanPhams;
                        break;
                    default:
                        throw new ArgumentException($"Table '{tableName}' is not recognized.");
                }

                return GetSelectList(query, valueField, textField);
            }
        }

        // Các phương thức cụ thể
        public static SelectList GetQuocGia()
        {
            return GetDynamicSelectList("QuocGias", "MaQG", "TenQuocGia");
        }

        public static SelectList GetHangSanXuat()
        {
            return GetDynamicSelectList("HangSanXuats", "MaHang", "TenHang");
        }

        public static SelectList GetLoaiSanPham()
        {
            return GetDynamicSelectList("LoaiSanPhams", "MaLSP", "TenLSP");
        }

        public static SelectList GetRam()
        {
            return GetDynamicSelectList("RAMs", "MaRAM", "DungLuong");
        }

        public static SelectList GetRom()
        {
            return GetDynamicSelectList("ROMs", "MaROM", "DungLuong");
        }

        public static SelectList GetMauSac()
        {
            return GetDynamicSelectList("MauSacs", "MaMau", "TenMau");
        }

        public static SelectList GetPhienBanHDH()
        {
            return GetDynamicSelectList("PhienBanHDHs", "MaPhienBanHDH", "TenPhienBanHDH");
        }

        public static SelectList GetChipXuLy()
        {
            return GetDynamicSelectList("ChipXuLies", "MaChip", "TenChip");
        }

        public static SelectList GetChipDoHoa()
        {
            return GetDynamicSelectList("ChipDoHoaGPUs", "MaChipDoHoaGPU", "TenChipDoHoaGPU");
        }

        public static SelectList GetTocDoCPU()
        {
            return GetDynamicSelectList("TocDoCPUs", "MaTocDoCPU", "TocDo");
        }

        public static SelectList GetChuanKhangBuiNuoc()
        {
            return GetDynamicSelectList("ChuanKhangBuiNuocs", "MaChuanKhangBuiNuoc", "TenChuanKhangBuiNuoc");
        }

        public static SelectList GetTinhNang()
        {
            return GetDynamicSelectList("TinhNangs", "MaTinhNang", "TenTinhNang");
        }

        public static SelectList GetLoaiSim()
        {
            return GetDynamicSelectList("SIMs", "MaSIM", "TenSIM");
        }

        public static SelectList GetBluetooth()
        {
            return GetDynamicSelectList("Blueteeth", "MaBluetooth", "TenBluetooth");
        }

        public static SelectList GetWifi()
        {
            return GetDynamicSelectList("Wifis", "MaWifi", "TenWifi");
        }

        public static SelectList GetLoaiCamera()
        {
            return GetDynamicSelectList("LoaiCameras", "MaLoaiCamera", "TenLoaiCamera");
        }

        public static SelectList GetDoPhanGiaiCamera()
        {
            return GetDynamicSelectList("DoPhanGiaiCameras", "MaDoPhanGiai", "DoPhanGiai");
        }

        public static SelectList GetDoPhanGiaiQuayPhim()
        {
            return GetDynamicSelectList("DoPhanGiaiQuayPhims", "MaDoPhanGiaiQuayPhim", "TenDoPhanGiaiQuayPhim");
        }

        public static SelectList GetTanSoQuetManHinh()
        {
            return GetDynamicSelectList("TanSoQuetManHinhs", "MaTanSoQuet", "TanSo");
        }

        public static SelectList GetLoaiPin()
        {
            return GetDynamicSelectList("LoaiPins", "MaLoaiPin", "TenLoaiPin");
        }

        public static SelectList GetDungLuongPin()
        {
            return GetDynamicSelectList("DungLuongPins", "MaDungLuongPin", "DungLuong");
        }

        public static SelectList GetThietKe()
        {
            return GetDynamicSelectList("ThietKes", "MaThietKe", "TenThietKe");
        }

        public static SelectList GetChatLieu()
        {
            return GetDynamicSelectList("ChatLieux", "MaChatLieu", "TenChatLieu");
        }

        public static SelectList GetTocDoKhungHinh()
        {
            return GetDynamicSelectList("TocDoKhungHinhs", "MaTocDoKhungHinh", "TocDo");
        }

        public static SelectList GetCongNgheManHinh()
        {
            return GetDynamicSelectList("CongNgheManHinhs", "MaCongNgheManHinh", "TenCongNgheManHinh");
        }

        public static SelectList GetDoPhanGiaiManHinh()
        {
            return GetDynamicSelectList("DoPhanGiaiManHinhs", "MaDoPhanGiai", "TenDoPhanGiai");
        }

        public static SelectList GetKichThuocManHinh()
        {
            return GetDynamicSelectList("KichThuocManHinhs", "MaKichThuocManHinh", "KichThuoc");
        }

        public static SelectList GetDoSangToiDa()
        {
            return GetDynamicSelectList("DoSangToiDas", "MaDoSang", "DoSang");
        }

        public static SelectList GetLoaiKinhCuongLuc()
        {
            return GetDynamicSelectList("LoaiKinhCuongLucs", "MaLoaiKinh", "TenLoaiKinh");
        }

        public static SelectList GetHoTroSacToiDa()
        {
            return GetDynamicSelectList("HoTroSacToiDas", "MaHoTroSac", "CongSuat");
        }

        public static SelectList GetCongNghePin()
        {
            return GetDynamicSelectList("CongNghePins", "MaCongNghePin", "TenCongNghePin");
        }

        public static SelectList GetTinhNangBaoMat()
        {
            return GetDynamicSelectList("TinhNangBaoMats", "MaTinhNangBaoMat", "TenTinhNangBaoMat");
        }

        public static SelectList GetTinhNangDacBiet()
        {
            return GetDynamicSelectList("TinhNangDacBiets", "MaTinhNangDacBiet", "TenTinhNangDacBiet");
        }

        public static SelectList GetDinhDangGhiAm()
        {
            return GetDynamicSelectList("DinhDangGhiAms", "MaDinhDangGhiAm", "TenDinhDangGhiAm");
        }

        public static SelectList GetDinhDangPhim()
        {
            return GetDynamicSelectList("DinhDangPhims", "MaDinhDangPhim", "TenDinhDangPhim");
        }

        public static SelectList GetDinhDangNhac()
        {
            return GetDynamicSelectList("DinhDangNhacs", "MaDinhDangNhac", "TenDinhDangNhac");
        }

        public static SelectList GetMangDiDong()
        {
            return GetDynamicSelectList("MangDiDongs", "MaMangDiDong", "TenMangDiDong");
        }

        public static SelectList GetSim()
        {
            return GetDynamicSelectList("SIMs", "MaSIM", "TenSIM");
        }

        public static SelectList GetHeThongDinhViGPS()
        {
            return GetDynamicSelectList("HeThongDinhViGPS", "MaHeThongDinhViGPS", "TenHeThongDinhViGPS");
        }

        public static SelectList GetCongKetNoiSac()
        {
            return GetDynamicSelectList("CongKetNoiSacs", "MaCongKetNoiSac", "TenCongKetNoiSac");
        }

        public static SelectList GetJackTaiNghe()
        {
            return GetDynamicSelectList("JackTaiNghes", "MaJackTaiNghe", "TenJackTaiNghe");
        }

        public static SelectList GetCongKetNoiKhac()
        {
            return GetDynamicSelectList("CongKetNoiKhacs", "MaCongKetNoiKhac", "TenCongKetNoiKhac");
        }

        public static SelectList GetSanPham()
        {
            return GetDynamicSelectList("SanPhams", "MaSP", "TenSP");
        }
    }
}
