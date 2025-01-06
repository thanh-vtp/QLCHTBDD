using QLCHTBDD_62131904.Models;
using QLCHTBDD_62131904.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCHTBDD_62131904.Controllers.Main
{
    public class LichSuMuaHang_62131904Controller : Controller
    {
        private readonly QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // LichSuMuaHang_62131904
        public ActionResult LichSuMuaHang()
        {
            // Kiểm tra khách hàng đã đăng nhập
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Login", "AccountCustomer_62131904");
            }

            // Lấy ID khách hàng từ Session
            int maKH = (int)Session["CustomerId"];

            // Lấy danh sách lịch sử mua hàng của khách hàng
            var lichSuMuaHang = db.DonHangs
                .Where(dh => dh.MaKH == maKH) // Lọc theo khách hàng
                .OrderByDescending(dh => dh.NgayDatHang) // Sắp xếp theo ngày đặt hàng (mới nhất lên trước)
                .Select(dh => new LichSuMuaHangViewModel
                {
                    MaDonHang = dh.MaDonHang,
                    NgayDatHang = dh.NgayDatHang,
                    TongTien = dh.TongTien,
                    TrangThai = dh.TrangThaiDonHang.TenTrangThai, // Lấy trạng thái đơn hàng
                    ChiTiet = dh.ChiTietDonHangs.Select(ct => new ChiTietDonHangViewModel
                    {
                        MaBienThe = ct.MaBT,
                        TenSanPham = ct.BienTheSanPham.SanPham.TenSP,
                        MauSac = ct.BienTheSanPham.MauSac.TenMau,
                        RAM = ct.BienTheSanPham.RAM.DungLuong,
                        ROM = ct.BienTheSanPham.ROM.DungLuong,
                        SoLuong = ct.SoLuong,
                        DonGia = ct.DonGia,
                    }).ToList()
                })
                .ToList();

            return View(lichSuMuaHang);
        }

    }
}