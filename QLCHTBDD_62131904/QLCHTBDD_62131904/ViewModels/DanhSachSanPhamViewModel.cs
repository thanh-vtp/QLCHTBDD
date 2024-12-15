using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class DanhSachSanPhamViewModel
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string LoaiSanPham { get; set; }
        public List<string> AnhSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string DonViTinh { get; set; }
    }
}