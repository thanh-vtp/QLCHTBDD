using QLCHTBDD_62131904.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}
