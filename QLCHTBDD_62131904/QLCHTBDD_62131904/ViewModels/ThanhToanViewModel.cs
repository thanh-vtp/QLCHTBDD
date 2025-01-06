using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class ThanhToanViewModel
    {
        public int MaKH { get; set; }
        public string TenKhachHang { get; set; }
        public List<ChiTietGioHangViewModel> SanPhams { get; set; }
        public decimal TongTien { get; set; }
    }
}