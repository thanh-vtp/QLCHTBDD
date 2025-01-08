using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class VariantViewModel
    {
        public int BienTheID { get; set; }
        public string RAM { get; set; }
        public string ROM { get; set; }
        public string MauSac { get; set; }
        public string DonViTinh { get; set; }
        public decimal DonGia { get; set; }
        public decimal Soluong { get; set; }
        public string HinhAnhChinh { get; set; } // Ảnh chính của biến thể
        public List<string> HinhAnhPhu { get; set; } // Danh sách ảnh phụ của biến thể
        public string TrangThai { get; set; }
        public string SKU { get; set; }
        public string ChipXuLy { get; set; }
        public string TocDoCPU { get; set; }
        public string GPU { get; set; }
        public string DungLuongConLai { get; set; }
        public string DoPhanGiaiManHinh { get; set; }
        public string PixelNgang { get; set; }
        public string PixelDoc { get; set; }
        public string KichThuocManHinh { get; set; } 
        public string TanSoQuetManHinh { get; set; }
        public string DungLuongPin { get; set; }
        public string ChieuDai { get; set; }
        public string ChieuNgang { get; set; }
        public string DoDay { get; set; }
        public string KhoiLuong { get; set; }
    }
}