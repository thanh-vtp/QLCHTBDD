using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class LichSuMuaHangViewModel
    {
        public int MaDonHang { get; set; } // Mã đơn hàng
        public DateTime NgayDatHang { get; set; } // Ngày đặt hàng
        public decimal TongTien { get; set; } // Tổng tiền đơn hàng
        public string TrangThai { get; set; } // Trạng thái đơn hàng
        public List<ChiTietDonHangViewModel> ChiTiet { get; set; } // Danh sách chi tiết đơn hàng
    }
}