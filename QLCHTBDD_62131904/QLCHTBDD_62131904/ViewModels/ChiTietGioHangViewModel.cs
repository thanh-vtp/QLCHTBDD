﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCHTBDD_62131904.ViewModels
{
    public class ChiTietGioHangViewModel
    {
        public int? MaBienThe { get; set; } // Mã biến thể
        public string TenSanPham { get; set; } // Tên sản phẩm
        public string MauSac { get; set; } // Màu sắc
        public string RAM { get; set; } // RAM
        public string ROM { get; set; } // ROM
        public int SoLuong { get; set; } // Số lượng
        public decimal DonGia { get; set; } // Giá sản phẩm
        public decimal ThanhTien => DonGia * SoLuong; // Thành tiền
    }
}
