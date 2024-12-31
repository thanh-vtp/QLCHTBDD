using Microsoft.Ajax.Utilities;
using QLCHTBDD_62131904.Models;
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
            var products = db.BienTheSanPhams
                .Join(db.SanPhams,
                      bt => bt.MaSP,
                      sp => sp.MaSP,
                      (bt, sp) => new { BienThe = bt, SanPham = sp })
                .Join(db.ThongSoBienTheDienThoais,
                      result => result.BienThe.MaBT,
                      tsbt => tsbt.MaBT,
                      (result, tsbt) => new { result.BienThe, result.SanPham, ThongSo = tsbt })
                .Join(db.DoPhanGiaiManHinhs,
                      result => result.ThongSo.MaDoPhanGiai,
                      dpmh => dpmh.MaDoPhanGiai,
                      (result, dpmh) => new { result.BienThe, result.SanPham, result.ThongSo, DoPhanGiai = dpmh.TenDoPhanGiai })
                .Join(db.KichThuocManHinhs,
                      result => result.ThongSo.MaKichThuocManHinh,
                      kthuoc => kthuoc.MaKichThuocManHinh,
                      (result, kthuoc) => new { result.BienThe, result.SanPham, result.DoPhanGiai, KichThuoc = kthuoc.KichThuoc })
                .Join(db.ROMs,
                      result => result.BienThe.MaROM,
                      rom => rom.MaROM,
                      (result, rom) => new
                      {
                          TenSanPham = result.SanPham.TenSP,
                          DoPhanGiaiManHinh = result.DoPhanGiai,
                          KichThuocManHinh = result.KichThuoc,
                          ROM = rom.DungLuong,
                          DonGia = result.BienThe.DonGia,
                          BienTheID = result.BienThe.MaBT
                      })
                .GroupBy(p => new { p.TenSanPham, p.DoPhanGiaiManHinh, p.KichThuocManHinh })
                .Select(group => new
                {
                    TenSanPham = group.Key.TenSanPham,
                    DoPhanGiaiManHinh = group.Key.DoPhanGiaiManHinh,
                    KichThuocManHinh = group.Key.KichThuocManHinh,
                    Variants = group.Select(g => new { g.ROM, g.DonGia, g.BienTheID }).ToList()
                })
                .ToList();

            // Truyền danh sách sản phẩm (có thể rỗng) vào View
            return View(products);
        }

    }
}