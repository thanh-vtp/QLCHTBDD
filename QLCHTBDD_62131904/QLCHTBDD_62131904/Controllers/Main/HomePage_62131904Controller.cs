using Microsoft.Ajax.Utilities;
using QLCHTBDD_62131904.Models;
using QLCHTBDD_62131904.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCHTBDD_62131904.Controllers.Main
{
    public class HomePage_62131904Controller : Controller
    {
        private readonly QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // HomePage_62131904
        public ActionResult DanhSachSanPham()
        {
            var products = db.SanPhams
            .Where(sp => sp.BienTheSanPhams.Any()) // Chỉ lấy sản phẩm có biến thể
            .Select(sp => new ProductViewModel
            {
                MaSanPham = sp.MaSP,
                TenSanPham = sp.TenSP,
                DoPhanGiaiManHinh = sp.BienTheSanPhams
                    .Select(bt => bt.ThongSoBienTheDienThoais
                        .Select(ts => ts.DoPhanGiaiManHinh.TenDoPhanGiai)
                        .FirstOrDefault())
                    .FirstOrDefault(),
                KichThuocManHinh = sp.BienTheSanPhams
                    .Select(bt => bt.ThongSoBienTheDienThoais
                        .Select(ts => ts.KichThuocManHinh.KichThuoc)
                        .FirstOrDefault())
                    .FirstOrDefault(),
                HinhAnh = sp.BienTheSanPhams
                    .SelectMany(bt => bt.HinhAnhSanPhams)
                    .Where(ha => ha.AnhChinh == true)
                    .Select(ha => ha.DuongDanAnh)
                     .FirstOrDefault() ?? "~/Images/logo-login.jpg",
                Variants = sp.BienTheSanPhams.Select(bt => new VariantViewModel
                {
                    ROM = bt.ROM.DungLuong,
                    DonGia = bt.DonGia,
                    BienTheID = bt.MaBT
                }).ToList()
            })
            .ToList();

            return View(products);
        }

        public ActionResult ChiTietSanPham(int id)
        {
            var product = db.SanPhams
                .Where(sp => sp.MaSP == id)
                .Select(sp => new ProductViewModel
                {
                    TenSanPham = sp.TenSP,
                    MoTa = sp.MoTa,
                    // Lấy ảnh chính hoặc ảnh mặc định nếu không có
                    HinhAnh = sp.BienTheSanPhams
                        .SelectMany(bt => bt.HinhAnhSanPhams)
                        .Where(ha => ha.AnhChinh == true)
                        .Select(ha => ha.DuongDanAnh)
                        .FirstOrDefault() ?? "~/Images/logo-login.jpg",
                    // Lấy tất cả ảnh phụ
                    HinhAnhPhu = sp.BienTheSanPhams
                        .SelectMany(bt => bt.HinhAnhSanPhams)
                        .Where(ha => ha.AnhChinh == false)
                        .Select(ha => ha.DuongDanAnh)
                        .ToList(),
                    // Lấy danh sách các biến thể
                    Variants = sp.BienTheSanPhams.Select(bt => new VariantViewModel
                    {
                        RAM = bt.RAM.DungLuong,
                        ROM = bt.ROM.DungLuong,
                        MauSac = bt.MauSac.TenMau,
                        DonGia = bt.DonGia,
                        BienTheID = bt.MaBT,
                        HinhAnhChinh = bt.HinhAnhSanPhams
                            .Where(ha => ha.AnhChinh == true)
                            .Select(ha => ha.DuongDanAnh)
                            .FirstOrDefault() ?? "~/Images/logo-login.jpg",
                        HinhAnhPhu = bt.HinhAnhSanPhams
                            .Where(ha => ha.AnhChinh == false)
                            .Select(ha => ha.DuongDanAnh)
                            .ToList()
                    }).ToList()
                })
                .FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }
    }
}