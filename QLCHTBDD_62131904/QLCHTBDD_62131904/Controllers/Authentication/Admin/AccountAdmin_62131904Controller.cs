using QLCHTBDD_62131904.Models;
using QLCHTBDD_62131904.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCHTBDD_62131904.Controllers.Authentication
{
    public class AccountAdmin_62131904Controller : Controller
    {
        private readonly QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // AccountAdmin_62131904
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Tên đăng nhập và mật khẩu không được để trống.");
                return View();
            }

            // Tìm kiếm quản trị viên
            var admin = db.QuanTriViens.FirstOrDefault(q => q.Username == username );
            if (admin == null)
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại.");
                return View();
            }

            if (admin.Password != password)
            {
                ModelState.AddModelError("", "Thông tin đăng nhập không đúng.");
                return View();
            }

            // Lưu thông tin vào Session
            Session["AdminId"] = admin.MaQT;
            Session["Username"] = admin.Username;
            Session["Role"] = "Admin";

            // Kiểm đã đăng nhập chưa
            if (Session["AdminId"] != null)
            {
                return RedirectToAction("Index", "SanPhams_62131904"); // Nếu đã đăng nhập, chuyển hướng tới trang chính
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "AccountAdmin_62131904");
        }
    }
}