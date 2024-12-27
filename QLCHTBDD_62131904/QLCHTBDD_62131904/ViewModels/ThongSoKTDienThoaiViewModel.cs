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
        public string TenSP { get; set; }
        public string PhienBanHDH { get; set; }
        public string ChipXuLy { get; set; } // Chip xử lý
        public string TocDoCPU { get; set; } // Tốc độ CPU
        public string ChipDoHoaGPU { get; set; } // Chip đồ họa GPU
        public string DungLuongConLai { get; set; } // Dung lượng khả dụng
        public string DanhBa { get; set; } // Số lượng danh bạ
        public string DenFlashCameraSau { get; set; } // Đèn flash
        public string ChuanKhangBuiNuoc { get; set; } // Chuẩn chống nước, bụi
        public string DinhDangGhiAm { get; set; } // Định dạng ghi âm
        public string Sim { get; set; } // Loại SIM
        public string Bluetooth { get; set; } // Phiên bản Bluetooth
        public string CongKetNoiSac { get; set; } // Loại cổng kết nối
        public string JackTaiNghe { get; set; } // Jack tai nghe
        public string CongKetNoiKhac { get; set; } // Các kết nối khác
        public string ThietKe { get; set; } // Thiết kế sản phẩm
        public string ChatLieu { get; set; } // Chất liệu khung/mặt lưng
        public DateTime ThoiDiemRaMat { get; set; } // Ngày ra mắt
        //
        //public string DoPhanGiaiCameraSau { get; set; } // Độ phân giải camera sau
        //public string QuayPhimCameraSau { get; set; } // Thông số quay phim camera sau
        //public string TinhNangCameraSau { get; set; } // Tính năng camera sau
        //public string DoPhanGiaiCameraTruoc { get; set; } // Độ phân giải camera trước
        //public string TinhNangCameraTruoc { get; set; } // Tính năng camera trước
        //public string CongNgheManHinh { get; set; } // Công nghệ màn hình
        //public string DoPhanGiaiManHinh { get; set; } // Độ phân giải màn hình
        //public string KichThuocManHinh { get; set; } // Kích thước màn hình
        //public string DoSangToiDa { get; set; } // Độ sáng tối đa
        //public string MatKinhCamUng { get; set; } // Loại kính cường lực
        //// Pin & Sạc
        //public string DungLuongPin { get; set; } // Dung lượng pin
        //public string LoaiPin { get; set; } // Loại pin
        //public string HoTroSacToiDa { get; set; } // Công nghệ sạc nhanh
        //public string CongNghePin { get; set; } // Công nghệ pin
        //// Tiện ích
        //public string BaoMatNangCao { get; set; } // Tính năng bảo mật
        //public string TinhNangDacBiet { get; set; } // Tính năng đặc biệt
        //public string GhiAm { get; set; } // Khả năng ghi âm
        //public string XemPhim { get; set; } // Khả năng xem phim
        //public string NgheNhac { get; set; } // Khả năng nghe nhạc
        //// Kết nối
        //public string MangDiDong { get; set; } // Công nghệ mạng di động
        //public string Wifi { get; set; } // Công nghệ Wi-Fi
        //public string GPS { get; set; } // Công nghệ GPS
        //// Thiết kế & Chất liệu
        //public string KichThuocKhoiLuong { get; set; } // Kích thước và khối lượng
    }
}