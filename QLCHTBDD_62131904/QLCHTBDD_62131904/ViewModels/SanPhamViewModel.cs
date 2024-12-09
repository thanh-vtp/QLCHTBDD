using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class SanPhamViewModel
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string MoTa { get; set; }
        public string LoaiSP { get; set; }
        public string AnhSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string DonViTinh { get; set; }
    }
}