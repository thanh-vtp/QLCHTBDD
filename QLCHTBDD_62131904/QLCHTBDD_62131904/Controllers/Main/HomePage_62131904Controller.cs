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
                    .FirstOrDefault() ?? "/Content/Images/DefaultImage.png",
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
    }
}