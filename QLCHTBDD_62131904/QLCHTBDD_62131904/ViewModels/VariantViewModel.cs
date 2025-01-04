using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class VariantViewModel
    {
        public string RAM { get; set; }
        public string ROM { get; set; }
        public string MauSac { get; set; }
        public decimal DonGia { get; set; }
        public string HinhAnhChinh { get; set; } // Ảnh chính của biến thể
        public List<string> HinhAnhPhu { get; set; } // Danh sách ảnh phụ của biến thể
        public int BienTheID { get; set; }
    }
}