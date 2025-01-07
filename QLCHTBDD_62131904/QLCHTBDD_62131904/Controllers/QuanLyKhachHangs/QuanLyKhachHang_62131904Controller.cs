using QLCHTBDD_62131904.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace QLCHTBDD_62131904.Controllers.QuanLyKhachHangs
{
    public class QuanLyKhachHang_62131904Controller : Controller
    {
        private readonly QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: QuanLyKhachHang_62131904

        public ActionResult DanhSachKhanhHang()
        {
            var khachHangs = db.KhachHangs;
            return View(khachHangs.ToList());
        }
    }
}