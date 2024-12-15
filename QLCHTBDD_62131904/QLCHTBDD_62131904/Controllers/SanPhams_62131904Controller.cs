using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Models;
using QLCHTBDD_62131904.ViewModels;


namespace QLCHTBDD_62131904.Controllers
{
    public class SanPhams_62131904Controller : Controller
    {
        private QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        public List<DanhSachSanPhamViewModel> dsSanPham()
        {
            return db.BienTheSanPhams
                .Include(b => b.SanPham)
                .Include(b => b.SanPham.LoaiSanPham)
                .Include(b => b.HinhAnhSanPhams)
                .Select(b => new DanhSachSanPhamViewModel
                {
                    MaSP = b.SanPham.MaSP,
                    TenSP = b.SanPham.TenSP,
                    AnhSP = b.HinhAnhSanPhams.Select(h => h.AnhSP).ToList(),
                    SoLuong = b.SoLuong,
                    DonGia = b.DonGia,
                    DonViTinh = b.DonViTinh,
                    LoaiSanPham = b.SanPham.LoaiSanPham.TenLSP,
                }).ToList();
        }

        // GET: SanPhams_62131904
        public ActionResult Index()
        {
            var sanPhams = dsSanPham();
            return View(sanPhams);
        }

        // GET: SanPhams_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams_62131904/Create
        public ActionResult Create()
        {
            ViewBag.MaQG = new SelectList(db.QuocGias.Where(qg => qg.IsActive == true), "MaQG", "TenQuocGia");
            ViewBag.MaHang = new SelectList(db.HangSanXuats.Where(hsx => hsx.IsActive == true), "MaHang", "TenHang");
            ViewBag.MaLSP = new SelectList(db.LoaiSanPhams.Where(lsp => lsp.IsActive == true), "MaLSP", "TenLSP");
            ViewBag.MaMau = new SelectList(db.MauSacs.Where(ms => ms.IsActive == true), "MaMau", "TenMau");
            ViewBag.MaRAM = new SelectList(db.RAMs.Where(ram => ram.IsActive == true), "MaRAM", "DungLuong");
            ViewBag.MaROM = new SelectList(db.ROMs.Where(rom => rom.IsActive == true), "MaROM", "DungLuong");
            ViewBag.MaHDH = new SelectList(db.HeDieuHanhs.Where(hdh => hdh.IsActive == true), "MaHDH", "TenHDH");
            ViewBag.MaBluetooth = new SelectList(db.Blueteeth.Where(bt => bt.IsActive == true), "MaBluetooth", "PhienBan");
            ViewBag.MaChuanKhangBuiNuoc = new SelectList(db.ChuanKhangBuiNuocs.Where(ckbn => ckbn.IsActive == true), "MaChuanKhangBuiNuoc", "TenChuan");
            ViewBag.MaChip = new SelectList(db.ChipXuLies.Where(chip => chip.IsActive == true), "MaChip", "TenChip");
            ViewBag.MaDoPhanGiai = new SelectList(db.DoPhanGiaiCameras.Where(dpg => dpg.IsActive == true), "MaDoPhanGiai", "DoPhanGiai");
            ViewBag.MaCongKetNoiSac = new SelectList(db.CongKetNoiSacs.Where(cks => cks.IsActive == true), "MaCongKetNoiSac", "LoaiKetNoi");
            ViewBag.MaCongKetNoiKhac = new SelectList(db.CongKetNoiKhacs.Where(ckk => ckk.IsActive == true), "MaCongKetNoiKhac", "LoaiKetNoi");
            return View();
        }

        // POST: SanPhams_62131904/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "TenSP,MoTa,MaLSP,MaHang")] SanPham product,
                int? MaQG,
                int? MaMau,
                int? MaRAM,
                int? MaROM,
                int? MaBluetooth,
                int? MaHDH,
                int? MaChuanKhangBuiNuoc,
                int? MaChip,
                int? MaDoPhanGiai,
                int? MaCongKetNoiSac,
                int? MaCongKetNoiKhac,
                int? SoLuong,
                string DonViTinh,
                int? DonGia,
                HttpPostedFileBase[] uploadedImages,
                ThongSoKTDienThoai phoneModel
        )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Thêm sản phẩm mới
                    db.SanPhams.Add(product);
                    db.SaveChanges();

                    // 2. Thêm biến thể sản phẩm
                    var variant = new BienTheSanPham
                    {
                        MaSP = product.MaSP,
                        MaMau = MaMau,
                        MaRAM = MaRAM,
                        MaROM = MaROM,
                        SoLuong = SoLuong.Value,
                        DonViTinh = DonViTinh,
                        DonGia = DonGia.Value,
                    };

                    db.BienTheSanPhams.Add(variant);
                    db.SaveChanges();

                    // 3. Thêm hình ảnh sản phẩm
                    if (uploadedImages != null)
                    {
                        foreach (var image in uploadedImages)
                        {
                            if (image != null && image.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(image.FileName);
                                var filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                                image.SaveAs(filePath);

                                var productImage = new HinhAnhSanPham
                                {
                                    MaBT = variant.MaBT,
                                    AnhSP = "/Images/" + fileName
                                };

                                db.HinhAnhSanPhams.Add(productImage);
                            }
                        }
                        db.SaveChanges();
                    }


                    // 4. Thêm thông số kỹ thuật theo loại sản phẩm
                    if (phoneModel != null)
                    {
                        var phoneSpecs = new ThongSoKTDienThoai
                        {
                            MaBT = variant.MaBT, // Khóa ngoại liên kết đến biến thể sản phẩm (MÃ BIẾN THỂ)
                            MaHDH = MaHDH,
                            MaChip = MaChip,
                            MaBluetooth = MaBluetooth,
                            MaChuanKhangBuiNuoc = MaChuanKhangBuiNuoc,
                            MaCongKetNoiSac = MaCongKetNoiSac,
                            MaCongKetNoiKhac = MaCongKetNoiKhac,
                            DoPhanGiaiManHinh = phoneModel.DoPhanGiaiManHinh,
                            KichThuocManHinh = phoneModel.KichThuocManHinh,
                            DungLuongPin = phoneModel.DungLuongPin,
                            LoaiPin = phoneModel.LoaiPin,
                            TocDoCPU = phoneModel.TocDoCPU,
                            ChipDoHoaGPU = phoneModel.ChipDoHoaGPU,
                            DungLuongConLai = phoneModel.DungLuongConLai,
                            BaoMatNangCao = phoneModel.BaoMatNangCao,
                            ThoiDiemRaMat = phoneModel.ThoiDiemRaMat,
                            JackTaiNghe = phoneModel.JackTaiNghe,
                            DenFlashCameraSau = phoneModel.DenFlashCameraSau,
                            GhiAm = phoneModel.GhiAm,
                            NgheNhac = phoneModel.NgheNhac,
                            MatKinhCamUng = phoneModel.MatKinhCamUng,
                            ThietKe = phoneModel.ThietKe,
                            ChatLieu = phoneModel.ChatLieu,
                           
                        };

                        db.ThongSoKTDienThoais.Add(phoneSpecs);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi lưu dữ liệu: " + ex.Message);
                }
            }

            // Trả lại view với model nếu có lỗi
            return View(phoneModel != null ? (object)phoneModel : null);
        }


        // GET: SanPhams_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang", sanPham.MaHang);
            ViewBag.MaLSP = new SelectList(db.LoaiSanPhams, "MaLSP", "TenLSP", sanPham.MaLSP);
            return View(sanPham);
        }

        // POST: SanPhams_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,MoTa,MaLSP,MaHang")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang", sanPham.MaHang);
            ViewBag.MaLSP = new SelectList(db.LoaiSanPhams, "MaLSP", "TenLSP", sanPham.MaLSP);
            return View(sanPham);
        }

        // GET: SanPhams_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
