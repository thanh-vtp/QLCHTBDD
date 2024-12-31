using QLCHTBDD_62131904.Models;
using QLCHTBDD_62131904.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLCHTBDD_62131904.Controllers.Authentication.KhachHangs
{
    public class AccountCustomer_62131904Controller : Controller
    {
        private readonly QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // AccountCustomer_62131904
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Email và mật khẩu không được để trống.");
                return View();
            }

            // Tìm kiếm khách hàng
            var customer = db.KhachHangs.FirstOrDefault(c => c.Email == email);
            if (customer == null || customer.IsActive == false )
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại hoặc tài khoản đã bị vô hiệu hóa.");
                return View();
            }

            // Kiểm tra mật khẩu (có mã hóa)
            bool isPasswordValid = Authentication_Services.VerifyPassword(customer.Password, password);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Thông tin đăng nhập không đúng.");
                return View();
            }

            // Lưu thông tin vào Session
            Session["CustomerId"] = customer.MaKH;
            Session["Email"] = customer.Email;
            Session["Role"] = "Customer";
            Session["HoTen"] = customer.HoTen;

            return RedirectToAction("DanhSachSanPham", "HomePage_62131904");
        }

        public ActionResult Logout()
        {
            // Xóa thông tin trong Session
            Session.Clear();
            return RedirectToAction("DanhSachSanPham", "SanPhams_62131904");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Email,Password,HoTen,SoDienThoai")] KhachHang khachHang)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(khachHang.Email) || string.IsNullOrEmpty(khachHang.Password)
                || string.IsNullOrEmpty(khachHang.HoTen) || string.IsNullOrEmpty(khachHang.SoDienThoai))
            {
                ModelState.AddModelError("", "Email, mật khẩu, họ tên và số điện thoại không được để trống.");
                return View(khachHang);
            }

            // Kiểm tra email đã tồn tại
            if (db.KhachHangs.Any(k => k.Email == khachHang.Email))
            {
                ModelState.AddModelError("", "Email đã tồn tại. Vui lòng sử dụng email khác.");
                return View(khachHang);
            }

            // Hash mật khẩu trước khi lưu
            khachHang.Password = Authentication_Services.HashPassword(khachHang.Password);
            khachHang.RoleId = 2; // Gán mặc định quyền "Khách hàng"
            khachHang.IsActive = true; // Mặc định kích hoạt tài khoản

            // Thêm khách hàng vào cơ sở dữ liệu
            db.KhachHangs.Add(khachHang);
            db.SaveChanges();

            // actionname/ controllername
            return RedirectToAction("Login", "AccountCustomer_62131904");
        }

        public ActionResult DetailsAccount(int userId)
        {
            KhachHang khachHang = db.KhachHangs.Find(userId);
            if (khachHang == null)
            {
                return HttpNotFound(); // Trả về lỗi 404
            }

            return View(khachHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(int userId, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                ModelState.AddModelError("", "Mật khẩu mới không được để trống.");
                return View();
            }

            var user = db.KhachHangs.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }

            user.Password = Authentication_Services.HashPassword(newPassword);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DetailsAccount", "AccountCustomer_62131904");
        }

    }
}