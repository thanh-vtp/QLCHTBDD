using QLCHTBDD_62131904.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class ThongSoKTDienThoaiViewModel
    {
        public int MaTSTK { get; set; }
        public int? MaSP { get; set; }
        public int PhienBanHDH { get; set; }
        public int ChipXuLy { get; set; } // Chip xử lý
        public int TocDoCPU { get; set; } // Tốc độ CPU
        public int ChipDoHoaGPU { get; set; } // Chip đồ họa GPU
        public int DungLuongConLai { get; set; } // Dung lượng khả dụng
        public bool DanhBa { get; set; } // Số lượng danh bạ
        public bool DenFlashCameraSau { get; set; } // Đèn flash
        public int ChuanKhangBuiNuoc { get; set; } // Chuẩn chống nước, bụi
        public int DinhDangGhiAm { get; set; } // Định dạng ghi âm
        public int Sim { get; set; } // Loại SIM
        public int Bluetooth { get; set; } // Phiên bản Bluetooth
        public int CongKetNoiSac { get; set; } // Loại cổng kết nối
        public int JackTaiNghe { get; set; } // Jack tai nghe
        public int CongKetNoiKhac { get; set; } // Các kết nối khác
        public int ThietKe { get; set; } // Thiết kế sản phẩm
        public int ChatLieu { get; set; } // Chất liệu khung/mặt lưng
        public DateTime ThoiDiemRaMat { get; set; } // Ngày ra mắt
    }
}