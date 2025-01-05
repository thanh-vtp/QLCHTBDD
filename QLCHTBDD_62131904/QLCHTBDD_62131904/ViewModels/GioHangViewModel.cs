using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class GioHangViewModel
    {
        public int MaGioHang { get; set; }
        public DateTime? NgayTao { get; set; }
        public List<ChiTietGioHangViewModel> SanPhams { get; set; }
    }
}