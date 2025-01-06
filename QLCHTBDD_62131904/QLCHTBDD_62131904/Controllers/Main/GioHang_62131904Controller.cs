using QLCHTBDD_62131904.Models;
using QLCHTBDD_62131904.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace QLCHTBDD_62131904.Controllers.Main
{
    public class GioHang_62131904Controller : Controller
    {
        private readonly QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GioHang_62131904
        [HttpPost]
        public ActionResult ThemVaoGio(int maBT, int soLuong)
        {
            if (Session["CustomerId"] == null)
            {
                TempData["ReturnUrl"] = Url.Action("ThemVaoGio", new { maBT, soLuong });
                return RedirectToAction("Login", "AccountCustomer_62131904");
            }

            int maKH = (int)Session["CustomerId"];
            var gioHang = db.GioHangs.FirstOrDefault(gh => gh.MaKH == maKH);
            if (gioHang == null)
            {
                gioHang = new GioHang
                {
                    MaKH = maKH,
                    CreatedOn = DateTime.Now
                };
                db.GioHangs.Add(gioHang);
                db.SaveChanges();
            }

            var bienTheSanPham = db.BienTheSanPhams.FirstOrDefault(bt => bt.MaBT == maBT);
            if (bienTheSanPham == null)
            {
                return HttpNotFound("Biến thể sản phẩm không tồn tại.");
            }

            var chiTietGioHang = db.ChiTietGioHangs
                .FirstOrDefault(ctgh => ctgh.MaGioHang == gioHang.MaGioHang && ctgh.MaBT == maBT);

            if (chiTietGioHang == null)
            {
                chiTietGioHang = new ChiTietGioHang
                {
                    MaGioHang = gioHang.MaGioHang,
                    MaBT = maBT,
                    SoLuong = soLuong,
                    Gia = bienTheSanPham.DonGia
                };
                db.ChiTietGioHangs.Add(chiTietGioHang);
            }
            else
            {
                if (chiTietGioHang.SoLuong + soLuong > bienTheSanPham.SoLuong)
                {
                    return Content("Số lượng yêu cầu vượt quá số lượng tồn kho.");
                }

                chiTietGioHang.SoLuong += soLuong;
            }

            db.SaveChanges();
            TempData["SuccessMessage"] = "Sản phẩm đã được thêm vào giỏ hàng thành công!";
            return RedirectToAction("ChiTietGioHang", "GioHang_62131904");
        }

        public ActionResult ChiTietGioHang()
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Login", "AccountCustomer_62131904");
            }

            int maKH = (int)Session["CustomerId"];
            var gioHang = db.GioHangs.FirstOrDefault(gh => gh.MaKH == maKH);

            if (gioHang == null)
            {
                return View(new GioHangViewModel { SanPhams = new List<ChiTietGioHangViewModel>() });
            }

            var chiTietGioHang = db.ChiTietGioHangs
                .Where(ct => ct.MaGioHang == gioHang.MaGioHang)
                .Select(ct => new ChiTietGioHangViewModel
                {
                    MaBienThe = ct.MaBT,
                    TenSanPham = ct.BienTheSanPham.SanPham.TenSP,
                    MauSac = ct.BienTheSanPham.MauSac.TenMau,
                    RAM = ct.BienTheSanPham.RAM.DungLuong,
                    ROM = ct.BienTheSanPham.ROM.DungLuong,
                    SoLuong = ct.SoLuong,
                    DonGia = ct.Gia,
                }).ToList();

            return View(new GioHangViewModel { SanPhams = chiTietGioHang });
        }

        [HttpPost]
        public ActionResult CapNhatSoLuong(int MaBT, int SoLuong)
        {
            // Kiểm tra khách hàng đăng nhập
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Login", "AccountCustomer_62131904");
            }

            int maKH = (int)Session["CustomerId"];
            var gioHang = db.GioHangs.FirstOrDefault(gh => gh.MaKH == maKH);
            if (gioHang == null)
            {
                return HttpNotFound("Giỏ hàng không tồn tại.");
            }

            // Lấy chi tiết giỏ hàng
            var chiTiet = db.ChiTietGioHangs.FirstOrDefault(ct => ct.MaGioHang == gioHang.MaGioHang && ct.MaBT == MaBT);
            if (chiTiet == null)
            {
                return HttpNotFound("Sản phẩm không tồn tại trong giỏ hàng.");
            }

            // Kiểm tra số lượng tồn kho
            var bienThe = db.BienTheSanPhams.FirstOrDefault(bt => bt.MaBT == MaBT);
            if (bienThe == null)
            {
                return HttpNotFound("Biến thể sản phẩm không tồn tại.");
            }

            if (SoLuong > bienThe.SoLuong)
            {
                TempData["ErrorMessage"] = "Số lượng yêu cầu vượt quá số lượng tồn kho.";
                return RedirectToAction("ChiTietGioHang", "GioHang_62131904");
            }

            // Cập nhật số lượng
            chiTiet.SoLuong = SoLuong;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Cập nhật số lượng thành công.";
            return RedirectToAction("ChiTietGioHang", "GioHang_62131904");
        }

        [HttpPost]
        public ActionResult XoaSanPham(int MaBT)
        {
            // Kiểm tra khách hàng đăng nhập
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Login", "AccountCustomer_62131904");
            }

            int maKH = (int)Session["CustomerId"];
            var gioHang = db.GioHangs.FirstOrDefault(gh => gh.MaKH == maKH);
            if (gioHang == null)
            {
                return HttpNotFound("Giỏ hàng không tồn tại.");
            }

            // Lấy chi tiết giỏ hàng
            var chiTiet = db.ChiTietGioHangs.FirstOrDefault(ct => ct.MaGioHang == gioHang.MaGioHang && ct.MaBT == MaBT);
            if (chiTiet == null)
            {
                return HttpNotFound("Sản phẩm không tồn tại trong giỏ hàng.");
            }

            // Xóa sản phẩm khỏi giỏ hàng
            db.ChiTietGioHangs.Remove(chiTiet);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Xóa sản phẩm khỏi giỏ hàng thành công.";
            return RedirectToAction("ChiTietGioHang", "GioHang_62131904");
        }

    }
}