using QLCHTBDD_62131904.Models;
using QLCHTBDD_62131904.Services;
using QLCHTBDD_62131904.ViewModels;
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
            if (customer == null)
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại.");
                return View();
            }

            if (customer.IsActive == false)
            {
                ModelState.AddModelError("", "Tài khoản đã bị vô hiệu hóa.");
                return View();
            }

            // Kiểm tra mật khẩu (có mã hóa)
            bool isPasswordValid = AuthenticationServices.VerifyPassword(customer.Password, password);
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
            Console.WriteLine(Session["CustomerId"]);
            Session.Clear();
            // Lưu URL quay lại
            TempData["ReturnUrl"] = Request.UrlReferrer?.ToString();
            return RedirectToAction("DanhSachSanPham", "HomePage_62131904");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Email,Password,HoTen,SoDienThoai,DiaChi,NgaySinh")] KhachHang khachHang)
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
            khachHang.Password = AuthenticationServices.HashPassword(khachHang.Password);
            khachHang.RoleId = 2; // Gán mặc định quyền "Khách hàng"
            khachHang.CreatedOn = DateTime.Now; // Ngày tạo tài khoản
            khachHang.IsActive = true; // Mặc định kích hoạt tài khoản

            // Thêm khách hàng vào cơ sở dữ liệu
            db.KhachHangs.Add(khachHang);
            db.SaveChanges();

            // actionname/ controllername
            return RedirectToAction("Login", "AccountCustomer_62131904");
        }

        public ActionResult DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMatKhau(string currentPassword, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                ViewBag.Error = "Vui lòng điền đầy đủ thông tin.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu mới và xác nhận mật khẩu không khớp.";
                return View();
            }

            int customerId = (int)Session["CustomerId"];
            var customer = db.KhachHangs.Find(customerId);

            if (customer == null)
            {
                ViewBag.Error = "Tài khoản không tồn tại.";
                return View();
            }

            if (!AuthenticationServices.VerifyPassword(customer.Password, currentPassword))
            {
                ViewBag.Error = "Mật khẩu hiện tại không đúng.";
                return View();
            }

            customer.Password = AuthenticationServices.HashPassword(newPassword);
            db.SaveChanges();

            ViewBag.Success = "Mật khẩu đã được đổi thành công.";
            return View();
        }

        public ActionResult ChiTietKhachHang()
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Login", "AccountCustomer_62131904");
            }

            int customerId = (int)Session["CustomerId"];
            var customer = db.KhachHangs.FirstOrDefault(kh => kh.MaKH == customerId);

            if (customer == null)
            {
                return HttpNotFound("Không tìm thấy thông tin khách hàng.");
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatThongTin(KhachHang model)
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Login", "AccountCustomer_62131904");
            }

            int customerId = (int)Session["CustomerId"];
            var customer = db.KhachHangs.FirstOrDefault(kh => kh.MaKH == customerId);

            if (customer == null)
            {
                return HttpNotFound("Không tìm thấy khách hàng.");
            }

            // Cập nhật chỉ các trường có giá trị
            if (!string.IsNullOrEmpty(model.HoTen))
            {
                customer.HoTen = model.HoTen;
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                customer.Email = model.Email;
            }
            if (!string.IsNullOrEmpty(model.SoDienThoai))
            {
                customer.SoDienThoai = model.SoDienThoai;
            }
            if (!string.IsNullOrEmpty(model.DiaChi))
            {
                customer.DiaChi = model.DiaChi;
            }

            try
            {
                db.SaveChanges();
                TempData["SuccessMessage"] = "Thông tin đã được cập nhật thành công.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi: " + ex.Message;
            }

            return RedirectToAction("ChiTietKhachHang", "AccountCustomer_62131904");
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Vui lòng nhập địa chỉ email.");
                return View();
            }

            // Kiểm tra email không phân biệt hoa thường
            var customer = db.KhachHangs.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (customer != null)
            {
                // Tạo token reset password
                string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                customer.ResetPasswordToken = token;
                customer.TokenExpiration = DateTime.Now.AddHours(1);
                db.SaveChanges();

                // Gửi email reset password
                string resetLink = Url.Action("ConfirmResetPassword", "AccountCustomer_62131904", new { token }, protocol: Request.Url.Scheme);
                string subject = "Đặt lại mật khẩu";
                string body = $"Vui lòng nhấn vào đường link sau để đặt lại mật khẩu: <a href='{resetLink}'>Đặt lại mật khẩu</a>";
                EmailServices.SendEmail(email, subject, body);
            }

            TempData["SuccessMessage"] = "Nếu email tồn tại trong hệ thống, bạn sẽ nhận được hướng dẫn đặt lại mật khẩu.";
            return RedirectToAction("ResetPassword");
        }

        public ActionResult ConfirmResetPassword(string token)
        {
            var customer = db.KhachHangs.FirstOrDefault(c => c.ResetPasswordToken == token && c.TokenExpiration > DateTime.Now);
            if (customer == null)
            {
                TempData["ErrorMessage"] = "Token không hợp lệ hoặc đã hết hạn.";
                return RedirectToAction("ResetPassword");
            }

            return View(new ResetPasswordViewModel { Token = token });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var customer = db.KhachHangs.FirstOrDefault(c => c.ResetPasswordToken == model.Token && c.TokenExpiration > DateTime.Now);
            if (customer == null)
            {
                TempData["ErrorMessage"] = "Token không hợp lệ hoặc đã hết hạn.";
                return RedirectToAction("ResetPassword");
            }

            // Cập nhật mật khẩu mới và làm sạch token
            customer.Password = AuthenticationServices.HashPassword(model.NewPassword);
            customer.ResetPasswordToken = null;
            customer.TokenExpiration = null;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Mật khẩu đã được đặt lại thành công.";
            return RedirectToAction("Login", "AccountCustomer_62131904");
        }
    }
}