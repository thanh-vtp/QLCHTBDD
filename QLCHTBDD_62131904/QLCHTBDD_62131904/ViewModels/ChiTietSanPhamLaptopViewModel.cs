using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class ChiTietSanPhamLaptopViewModel
    {
        // Thông tin chung về sản phẩm
        public string LoaiSanPham { get; set; } // Loại sản phẩm (Laptop, Ultrabook, v.v.)
        public int MaSP { get; set; } // Mã sản phẩm
        public string TenSP { get; set; } // Tên sản phẩm
        public string MoTa { get; set; } // Mô tả sản phẩm
        public string QuocGia { get; set; } // Quốc gia sản xuất
        public string Hang { get; set; } // Hãng sản xuất
        public string MauSac { get; set; } // Màu sắc
        public int SoLuong { get; set; } // Số lượng
        public string DonViTinh { get; set; } // Đơn vị tính
        public decimal DonGia { get; set; } // Đơn giá
        public List<string> AnhSP { get; set; } // Danh sách ảnh sản phẩm

        // Bộ xử lý
        public string CongNgheCPU { get; set; } // Công nghệ CPU (Intel, AMD)
        public string SoNhanCPU { get; set; } // Số nhân CPU
        public string SoLuongCPU { get; set; } // Số luồng CPU
        public string TocDoCPU { get; set; } // Tốc độ CPU cơ bản
        public string TocDoToiDa { get; set; } // Tốc độ CPU tối đa khi boost

        // Bộ nhớ RAM và ổ cứng
        public string LoaiRAM { get; set; } // Loại RAM (DDR4, DDR5)
        public string TocDoBusRAM { get; set; } // Tốc độ Bus RAM
        public string HoTroRAMToiDa { get; set; } // Dung lượng RAM tối đa hỗ trợ
        public string OCung { get; set; } // Loại ổ cứng và dung lượng (SSD, HDD)

        // Màn hình
        public string ManHinh { get; set; } // Kích thước và loại màn hình
        public string DoPhanGiai { get; set; } // Độ phân giải màn hình
        public string TanSoQuet { get; set; } // Tần số quét màn hình
        public string DoPhuMau { get; set; } // Độ phủ màu (sRGB, AdobeRGB)
        public string CongNgheManHinh { get; set; } // Công nghệ màn hình

        // Đồ họa và âm thanh
        public string CardManHinh { get; set; } // Thông tin card đồ họa
        public string CongNgheAmThanh { get; set; } // Công nghệ âm thanh

        // Cổng kết nối và tính năng mở rộng
        public string CongGiaoTiep { get; set; } // Các cổng giao tiếp (USB, HDMI, Ethernet)
        public string KetNoiKhongDay { get; set; } // Kết nối không dây (Wi-Fi, Bluetooth)
        public string Webcam { get; set; } // Thông tin webcam
        public string TinhNangKhac { get; set; } // Các tính năng khác
        public string DenBanPhim { get; set; } // Đèn bàn phím

        // Kích thước - Khối lượng - Pin
        public string KichThuoc { get; set; } // Kích thước laptop
        public string ChatLieu { get; set; } // Chất liệu vỏ máy
        public string ThongTinPin { get; set; } // Dung lượng pin và số cell

        // Hệ điều hành
        public string HeDieuHanh { get; set; } // Hệ điều hành cài đặt sẵn

        // Ngày ra mắt
        public DateTime? ThoiDiemRaMat { get; set; } // Ngày ra mắt sản phẩm
    }
}
