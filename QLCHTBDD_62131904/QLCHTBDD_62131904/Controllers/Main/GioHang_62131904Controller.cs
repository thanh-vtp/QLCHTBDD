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
        public ActionResult ThemVaoGio(int maSP, int soLuong)
        {
            // 1. Kiểm tra khách hàng đăng nhập
            if (Session["CustomerId"] == null)
            {
                TempData["ReturnUrl"] = Url.Action("ThemVaoGio", new { maSP, soLuong });
                return RedirectToAction("Login", "AccountCustomer_62131904");
            }

            int maKH = (int)Session["CustomerId"];
            Console.WriteLine(maKH);

            // 2. Kiểm tra giỏ hàng
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

            // Kiểm tra sản phẩm
            var sanPham = db.SanPhams
            .Include(sp => sp.BienTheSanPhams)
            .FirstOrDefault(sp => sp.MaSP == maSP);

            Console.WriteLine($"maSP = {maSP}");


            if (sanPham == null)
            {
                return HttpNotFound("Sản phẩm không tồn tại.");
            }

            var bienTheSanPham = sanPham.BienTheSanPhams.FirstOrDefault();
            if (bienTheSanPham == null)
            {
                return HttpNotFound("Sản phẩm không có biến thể hợp lệ.");
            }

            // 4. Thêm hoặc cập nhật sản phẩm trong giỏ
            var chiTietGioHang = db.ChiTietGioHangs.FirstOrDefault(ctgh => ctgh.MaGioHang == gioHang.MaGioHang && ctgh.MaSP == maSP);

            if (chiTietGioHang == null)
            {
                chiTietGioHang = new ChiTietGioHang
                {
                    MaGioHang = gioHang.MaGioHang,
                    MaSP = maSP,
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
            // Kiểm tra khách hàng đã đăng nhập
            int maKH;

            if (Session["CustomerId"] != null)
            {
                maKH = (int)Session["CustomerId"];
            }
            else
            {
                return RedirectToAction("Login", "AccountCustomer_62131904");
            }

            // Lấy giỏ hàng của khách hàng
            var gioHang = db.GioHangs
                            .Where(gh => gh.MaKH == maKH)
                            .Select(gh => new GioHangViewModel
                            {
                                MaGioHang = gh.MaGioHang,
                                NgayTao = gh.CreatedOn,
                                SanPhams = gh.ChiTietGioHangs.Select(ctgh => new ChiTietGioHangViewModel
                                {
                                    TenSanPham = ctgh.SanPham.TenSP,
                                    SoLuong = ctgh.SoLuong,
                                    Gia = ctgh.Gia,
                                    ThanhTien = ctgh.SoLuong * ctgh.Gia
                                }).ToList()
                            })
                            .FirstOrDefault();

            if (gioHang == null || gioHang.SanPhams.Count == 0)
            {
                ViewBag.Message = "Giỏ hàng của bạn đang trống.";
                return View();
            }

            return View(gioHang);
        }

        [HttpPost]
        public ActionResult XoaSanPham(int maSP)
        {
            // Kiểm tra khách hàng đã đăng nhập
            int maKH;

            if (Session["CustomerId"] != null)
            {
                maKH = (int)Session["CustomerId"];
            }
            else
            {
                return RedirectToAction("Login", "AccountCustomer_62131904");
            }

            // Lấy giỏ hàng của khách hàng
            var gioHang = db.GioHangs.FirstOrDefault(gh => gh.MaKH == maKH);
            if (gioHang == null)
            {
                return HttpNotFound("Giỏ hàng không tồn tại.");
            }

            // Tìm sản phẩm trong giỏ hàng
            var chiTietGioHang = db.ChiTietGioHangs.FirstOrDefault(ctgh => ctgh.MaGioHang == gioHang.MaGioHang && ctgh.MaSP == maSP);

            if (chiTietGioHang != null)
            {
                db.ChiTietGioHangs.Remove(chiTietGioHang);
                db.SaveChanges();
            }

            return RedirectToAction("ChiTietGioHang", "GioHang_62131904");
        }
    }
}