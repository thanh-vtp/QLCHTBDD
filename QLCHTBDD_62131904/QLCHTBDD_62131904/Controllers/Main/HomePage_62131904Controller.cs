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
        public ActionResult TimKiemSanPham(string sanPhams)
        {
            // Kiểm tra chuỗi tìm kiếm
            if (string.IsNullOrEmpty(sanPhams))
            {
                TempData["Message"] = "Vui lòng nhập từ khóa tìm kiếm.";
                return RedirectToAction("DanhSachSanPham");
            }

            // Tìm kiếm sản phẩm và ánh xạ tất cả biến thể vào ViewModel
            var sanPhamResult = db.SanPhams
                .Where(sp => sp.TenSP.Contains(sanPhams)) // Tìm theo từ khóa
                .Select(sp => new ProductViewModel
                {
                    MaSanPham = sp.MaSP,
                    TenSanPham = sp.TenSP,
                    MoTa = sp.MoTa, // Thêm mô tả nếu cần
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
                    Variants = sp.BienTheSanPhams // Lấy tất cả biến thể
                        .Select(bt => new VariantViewModel
                        {
                            BienTheID = bt.MaBT,
                            ROM = bt.ROM.DungLuong,
                            MauSac = bt.MauSac.TenMau,
                            DonGia = bt.DonGia
                        })
                        .ToList()
                })
                .ToList();

            if (!sanPhamResult.Any())
            {
                TempData["Message"] = "Không tìm thấy sản phẩm phù hợp.";
            }

            return View(sanPhamResult);
        }

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
            // Fetch the product and its related data
            var productData = db.SanPhams
                .Where(sp => sp.MaSP == id)
                .Select(sp => new
                {
                    MaSanPham = sp.MaSP,
                    TenSanPham = sp.TenSP,
                    MoTa = sp.MoTa,
                    HinhAnh = sp.BienTheSanPhams
                        .SelectMany(bt => bt.HinhAnhSanPhams)
                        .Where(ha => ha.AnhChinh == true)
                        .Select(ha => ha.DuongDanAnh)
                        .FirstOrDefault(),
                    HinhAnhPhu = sp.BienTheSanPhams
                        .SelectMany(bt => bt.HinhAnhSanPhams)
                        .Where(ha => ha.AnhChinh == false)
                        .Select(ha => ha.DuongDanAnh)
                        .ToList(),
                    ThongSoDienThoai = sp.ThongSoDienThoais.FirstOrDefault(),
                    Variants = sp.BienTheSanPhams
                        .GroupBy(bt => new { bt.ROM.DungLuong, bt.MauSac.TenMau })
                        .Select(g => new
                        {
                            RAM = g.FirstOrDefault().RAM.DungLuong,
                            ROM = g.Key.DungLuong,
                            MauSac = g.Key.TenMau,
                            DonGia = g.FirstOrDefault().DonGia,
                            BienTheID = g.FirstOrDefault().MaBT,
                            HinhAnhChinh = g.FirstOrDefault().HinhAnhSanPhams
                                .Where(ha => ha.AnhChinh == true)
                                .Select(ha => ha.DuongDanAnh)
                                .FirstOrDefault(),
                            HinhAnhPhu = g.FirstOrDefault().HinhAnhSanPhams
                                .Where(ha => ha.AnhChinh == false)
                                .Select(ha => ha.DuongDanAnh)
                                .ToList(),
                            ChipXuLy = g.FirstOrDefault().ThongSoBienTheDienThoais
                                .Select(tsbtdt => tsbtdt.ChipXuLy.TenChip)
                                .FirstOrDefault(),
                            TocDoCPU = g.FirstOrDefault().ThongSoBienTheDienThoais
                                .Select(tsbtdt => tsbtdt.TocDoCPU.TocDo)
                                .FirstOrDefault(),
                            GPU = g.FirstOrDefault().ThongSoBienTheDienThoais
                                .Select(tsbtdt => tsbtdt.ChipDoHoaGPU.TenChipDoHoaGPU)
                                .FirstOrDefault(),
                            DungLuongConLai = g.FirstOrDefault().ThongSoBienTheDienThoais
                                .Select(tsbtdt => tsbtdt.DungLuongConLai)
                                .FirstOrDefault(),
                            DoPhanGiaiManHinh = g.FirstOrDefault().ThongSoBienTheDienThoais
                                .Select(tsbtdt => tsbtdt.DoPhanGiaiManHinh.TenDoPhanGiai)
                                .FirstOrDefault(),
                            DungLuongPin = g.FirstOrDefault().ThongSoBienTheDienThoais
                                           .Select(tsbtdt => tsbtdt.DungLuongPin.DungLuong)
                                                        .FirstOrDefault(),
                            PixelNgang = g.FirstOrDefault().ThongSoBienTheDienThoais
                            .Select(tsbtdt => tsbtdt.PixelNgang.GTPixelNgang)
                                                        .FirstOrDefault().ToString(),
                            PixelDoc = g.FirstOrDefault().ThongSoBienTheDienThoais
                            .Select(tsbtdt => tsbtdt.PixelDoc.GTPixelDoc)
                                                        .FirstOrDefault().ToString(),
                            KichThuocManHinh = g.FirstOrDefault().ThongSoBienTheDienThoais
                            .Select(tsbtdt => tsbtdt.KichThuocManHinh.KichThuoc)
                                                        .FirstOrDefault(),
                            TanSoQuetManHinh = g.FirstOrDefault().ThongSoBienTheDienThoais
                            .Select(tsbtdt => tsbtdt.TanSoQuetManHinh.TanSo)
                                                        .FirstOrDefault().ToString(),

                            ChieuDai = g.FirstOrDefault().ThongSoBienTheDienThoais
                            .Select(tsbtdt => tsbtdt.ChieuDai.GTChieuDai)
                                                          .FirstOrDefault().ToString(),
                            ChieuNgang = g.FirstOrDefault().ThongSoBienTheDienThoais
                            .Select(tsbtdt => tsbtdt.ChieuNgang.GTChieuNgang)
                                                          .FirstOrDefault().ToString(),
                            DoDay = g.FirstOrDefault().ThongSoBienTheDienThoais
                            .Select(tsbtdt => tsbtdt.DoDay.GTDoDay)
                                                          .FirstOrDefault().ToString(),
                            KhoiLuong = g.FirstOrDefault().ThongSoBienTheDienThoais
                            .Select(tsbtdt => tsbtdt.KhoiLuong.GTKhoiLuong)
                                                          .FirstOrDefault().ToString()

                        })
                        .ToList(),
                    Camera = new
                    {
                        DoPhanGiaiCameraSau = sp.BienTheSanPhams
                            .SelectMany(bt => bt.ThongSoBienTheDienThoais)
                            .SelectMany(tsbtdt => tsbtdt.ThongSoCameras)
                            .Where(cam => cam.LoaiCamera.MaLoaiCamera == 2) // Camera Sau
                            .SelectMany(cam => cam.ChiTietDoPhanGiaiCameras)
                            .Select(dpg => dpg.DoPhanGiaiCamera.DoPhanGiai)
                            .ToList(),
                        DoPhanGiaiCameraTruoc = sp.BienTheSanPhams
                            .SelectMany(bt => bt.ThongSoBienTheDienThoais)
                            .SelectMany(tsbtdt => tsbtdt.ThongSoCameras)
                            .Where(cam => cam.LoaiCamera.MaLoaiCamera == 1) // Camera Trước
                            .SelectMany(cam => cam.ChiTietDoPhanGiaiCameras)
                            .Select(dpg => dpg.DoPhanGiaiCamera.DoPhanGiai)
                            .ToList(),
                        QuayPhimCameraSau = sp.BienTheSanPhams
                            .SelectMany(bt => bt.ThongSoBienTheDienThoais)
                            .SelectMany(tsbtdt => tsbtdt.ThongSoQuayPhims)
                            .Select(x => new
                            {
                                DoPhanGiai = x.DoPhanGiaiQuayPhim.TenDoPhanGiaiQuayPhim,
                                TocDo = x.TocDoKhungHinh.TocDo
                            })
                            .ToList(),

                        TinhNangCameraSau = sp.BienTheSanPhams
                            .SelectMany(bt => bt.ThongSoBienTheDienThoais)
                            .SelectMany(tsbtdt => tsbtdt.ThongSoCameras)
                            .Where(cam => cam.LoaiCamera.MaLoaiCamera == 2) // Camera Sau
                            .SelectMany(cam => cam.ThongSoBienTheDienThoai.ChiTietTinhNangCameras)
                            .Select(tn => tn.TinhNang.TenTinhNang)
                            .ToList(),

                        TinhNangCameraTruoc = sp.BienTheSanPhams
                        .SelectMany(bt => bt.ThongSoBienTheDienThoais)
                            .SelectMany(tsbtdt => tsbtdt.ThongSoCameras)
                            .Where(cam => cam.LoaiCamera.MaLoaiCamera == 1) // Camera Trước
                            .SelectMany(cam => cam.ThongSoBienTheDienThoai.ChiTietTinhNangCameras)
                            .Select(tn => tn.TinhNang.TenTinhNang)
                            .ToList()
                    }
                })
                .FirstOrDefault();

            if (productData == null)
            {
                return HttpNotFound();
            }

            // Map data to ViewModel
            var productViewModel = new ProductViewModel
            {
                MaSanPham = productData.MaSanPham,
                TenSanPham = productData.TenSanPham,
                MoTa = productData.MoTa,
                HinhAnh = productData.HinhAnh ?? "~/Images/logo-login.jpg",
                HinhAnhPhu = productData.HinhAnhPhu,
                HeDieuHanh = productData.ThongSoDienThoai?.PhienBanHDH?.TenPhienBanHDH,
                DanhBa = productData.ThongSoDienThoai?.DanhBa == true ? "Có" : "Không",
                DenFlash = productData.ThongSoDienThoai?.DenFlashCameraSau == true ? "Có" : "Không",
                KhacNuocBui = productData.ThongSoDienThoai?.ChuanKhangBuiNuoc?.TenChuanKhangBuiNuoc,
                CongNgheManHinh = productData.ThongSoDienThoai?.CongNgheManHinh?.TenCongNgheManHinh,
                DoSangToiDa = productData.ThongSoDienThoai?.DoSangToiDa?.DoSang.ToString(),
                LoaiKinh = productData.ThongSoDienThoai?.LoaiKinhCuongLuc?.TenLoaiKinh,
                LoaiPin = productData.ThongSoDienThoai?.LoaiPin?.TenLoaiPin,
                HoTroSac = productData.ThongSoDienThoai?.HoTroSacToiDa?.CongSuat,
                TinhNangBaoMat = productData.ThongSoDienThoai?.TinhNangBaoMat?.TenTinhNangBaoMat,
                DinhDangGhiAm = productData.ThongSoDienThoai?.DinhDangGhiAm?.TenDinhDangGhiAm,
                MangDiDong = productData.ThongSoDienThoai?.MangDiDong?.TenMangDiDong,
                Sim = productData.ThongSoDienThoai?.SIM?.TenSIM,
                Bluetooth = productData.ThongSoDienThoai?.Bluetooth?.TenBluetooth,
                CongKetNoiSac = productData.ThongSoDienThoai?.CongKetNoiSac?.TenCongKetNoiSac,
                JackTaiNghe = productData.ThongSoDienThoai?.JackTaiNghe?.TenJackTaiNghe,
                CongKetNoiKhac = productData.ThongSoDienThoai?.CongKetNoiKhac?.TenCongKetNoiKhac,
                ThietKe = productData.ThongSoDienThoai?.ThietKe?.TenThietKe,
                ChatLieu = productData.ThongSoDienThoai?.ChatLieu?.TenChatLieu,
                ThoiDiemRaMat = productData.ThongSoDienThoai?.ThoiDiemRaMat ?? DateTime.Now,

                Variants = productData.Variants.Select(v => new VariantViewModel
                {
                    RAM = v.RAM,
                    ROM = v.ROM,
                    MauSac = v.MauSac,
                    DonGia = v.DonGia,
                    BienTheID = v.BienTheID,
                    HinhAnhChinh = v.HinhAnhChinh ?? "~/Images/logo-login.jpg",
                    HinhAnhPhu = v.HinhAnhPhu,
                    ChipXuLy = v.ChipXuLy,
                    TocDoCPU = v.TocDoCPU,
                    GPU = v.GPU,
                    DungLuongConLai = v.DungLuongConLai,
                    DoPhanGiaiManHinh = v.DoPhanGiaiManHinh,
                    DungLuongPin = v.DungLuongPin,
                    KichThuocManHinh = v.KichThuocManHinh,
                    PixelNgang = v.PixelNgang,
                    PixelDoc = v.PixelDoc,
                    TanSoQuetManHinh = v.TanSoQuetManHinh,
                    ChieuDai = v.ChieuDai,
                    ChieuNgang = v.ChieuNgang,
                    DoDay = v.DoDay,
                    KhoiLuong = v.KhoiLuong

                }).ToList(),

                Camera = new CameraViewModel
                {
                    DoPhanGiaiCameraSau = productData.Camera.DoPhanGiaiCameraSau,
                    DoPhanGiaiCameraTruoc = productData.Camera.DoPhanGiaiCameraTruoc,
                    QuayPhimCameraSau = productData.Camera.QuayPhimCameraSau
                        .Select(x => $"{x.DoPhanGiai} @ {x.TocDo}fps")
                        .ToList(),
                    TinhNangCameraSau = productData.Camera.TinhNangCameraSau.ToList(),
                    TinhNangCameraTruoc = productData.Camera.TinhNangCameraTruoc.ToList(),
                }
            };

            return View(productViewModel);
        }


        public ActionResult SanPhamTheoThuongHieu(string thuongHieu)
        {
            if (string.IsNullOrEmpty(thuongHieu))
            {
                TempData["Message"] = "Vui lòng chọn một thương hiệu hợp lệ.";
                return RedirectToAction("DanhSachSanPham");
            }

            // Lọc sản phẩm theo tên thương hiệu
            var sanPhamResult = db.SanPhams
                .Where(sp => sp.HangSanXuat.TenHang.Equals(thuongHieu, StringComparison.OrdinalIgnoreCase))
                .Select(sp => new ProductViewModel
                {
                    MaSanPham = sp.MaSP,
                    TenSanPham = sp.TenSP,
                    MoTa = sp.MoTa,
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
                        .Select(bt => new VariantViewModel
                        {
                            BienTheID = bt.MaBT,
                            ROM = bt.ROM.DungLuong,
                            MauSac = bt.MauSac.TenMau,
                            DonGia = bt.DonGia
                        })
                        .ToList()
                })
                .ToList();

            if (!sanPhamResult.Any())
            {
                TempData["Message"] = $"Không tìm thấy sản phẩm thuộc thương hiệu {thuongHieu}.";
            }

            return View("DanhSachSanPham", sanPhamResult);
        }

    }
}