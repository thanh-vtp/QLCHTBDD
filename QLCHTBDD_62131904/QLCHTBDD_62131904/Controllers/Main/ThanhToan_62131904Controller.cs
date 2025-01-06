using QLCHTBDD_62131904.Models;
using QLCHTBDD_62131904.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace QLCHTBDD_62131904.Controllers.Main
{
    public class ThanhToan_62131904Controller : Controller
    {
        private readonly QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // ThanhToan_62131904
        public ActionResult ThanhToan()
        {
            // Kiểm tra khách hàng đã đăng nhập
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Login", "AccountCustomer_62131904");
            }

            // Lấy ID khách hàng từ Session
            int maKH = (int)Session["CustomerId"];

            // Lấy thông tin giỏ hàng
            var gioHang = db.GioHangs
                .Include(gh => gh.ChiTietGioHangs.Select(ct => ct.BienTheSanPham.SanPham))
                .FirstOrDefault(gh => gh.MaKH == maKH);

            if (gioHang == null || !gioHang.ChiTietGioHangs.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("ChiTietGioHang", "GioHang_62131904");
            }

            // Chuẩn bị dữ liệu cho view thanh toán
            var thanhToanViewModel = new ThanhToanViewModel
            {
                MaKH = maKH,
                TenKhachHang = gioHang.KhachHang.HoTen,
                SanPhams = gioHang.ChiTietGioHangs.Select(ct => new ChiTietGioHangViewModel
                {
                    MaBienThe = ct.MaBT,
                    TenSanPham = ct.BienTheSanPham.SanPham.TenSP,
                    MauSac = ct.BienTheSanPham.MauSac.TenMau,
                    RAM = ct.BienTheSanPham.RAM.DungLuong,
                    ROM = ct.BienTheSanPham.ROM.DungLuong,
                    SoLuong = ct.SoLuong,
                    DonGia = ct.Gia,
                }).ToList(),
                TongTien = gioHang.ChiTietGioHangs.Sum(ct => ct.SoLuong * ct.Gia)
            };

            return View(thanhToanViewModel);
        }

        [HttpPost]
        public ActionResult XacNhanThanhToan()
        {
            // Kiểm tra khách hàng đã đăng nhập
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Login", "AccountCustomer_62131904");
            }

            int maKH = (int)Session["CustomerId"];

            // Lấy thông tin giỏ hàng
            var gioHang = db.GioHangs
                .Include(gh => gh.ChiTietGioHangs)
                .FirstOrDefault(gh => gh.MaKH == maKH);

            if (gioHang == null || !gioHang.ChiTietGioHangs.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("ChiTietGioHang", "GioHang_62131904");
            }

            // Tạo đơn hàng mới
            var donHang = new DonHang
            {
                MaKH = maKH,
                NgayDatHang = DateTime.Now,
                MaTrangThai = 1, // Mã trạng thái: Đang xử lý
                TongTien = gioHang.ChiTietGioHangs.Sum(ct => ct.SoLuong * ct.Gia)
            };
            db.DonHangs.Add(donHang);
            db.SaveChanges();

            // Lưu chi tiết đơn hàng
            foreach (var chiTiet in gioHang.ChiTietGioHangs)
            {
                db.ChiTietDonHangs.Add(new ChiTietDonHang
                {
                    MaDonHang = donHang.MaDonHang,
                    MaBT = chiTiet.MaBT,
                    SoLuong = chiTiet.SoLuong,
                    DonGia = chiTiet.Gia
                });
            }

            // Xóa giỏ hàng sau khi thanh toán
            db.ChiTietGioHangs.RemoveRange(gioHang.ChiTietGioHangs);
            db.GioHangs.Remove(gioHang);

            db.SaveChanges();

            TempData["SuccessMessage"] = "Thanh toán thành công! Đơn hàng của bạn đang được xử lý.";
            return RedirectToAction("LichSuMuaHang", "LichSuMuaHang_62131904");
        }


    }
}