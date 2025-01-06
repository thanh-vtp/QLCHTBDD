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
                Variants = sp.BienTheSanPhams
                    .GroupBy(bt => bt.ROM.DungLuong) // Nhóm theo ROM để loại bỏ lặp
                    .Select(g => new VariantViewModel
                    {
                        ROM = g.Key,
                        DonGia = g.FirstOrDefault().DonGia,
                        BienTheID = g.FirstOrDefault().MaBT
                    })
                    .ToList()
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
                    MaSanPham = sp.MaSP,
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
                    // Lấy danh sách các biến thể, lọc theo màu và ROM
                    Variants = sp.BienTheSanPhams
                        .GroupBy(bt => new { bt.ROM.DungLuong, bt.MauSac.TenMau }) // Nhóm theo ROM và màu
                        .Select(g => new VariantViewModel
                        {
                            RAM = g.FirstOrDefault().RAM.DungLuong,
                            ROM = g.Key.DungLuong,
                            MauSac = g.Key.TenMau,
                            DonGia = g.FirstOrDefault().DonGia,
                            BienTheID = g.FirstOrDefault().MaBT,
                            HinhAnhChinh = g.FirstOrDefault().HinhAnhSanPhams
                                .Where(ha => ha.AnhChinh == true)
                                .Select(ha => ha.DuongDanAnh)
                                .FirstOrDefault() ?? "~/Images/logo-login.jpg",
                            HinhAnhPhu = g.FirstOrDefault().HinhAnhSanPhams
                                .Where(ha => ha.AnhChinh == false)
                                .Select(ha => ha.DuongDanAnh)
                                .ToList()
                        })
                        .ToList()
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