using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCHTBDD_62131904.Models;
using QLCHTBDD_62131904.Services;

namespace QLCHTBDD_62131904.Controllers
{
    public class QuanTriHeThongs_62131904Controller : Controller
    {
        private readonly QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: QuanTriHeThongs_62131904/Login
        public ActionResult Login()
        {
            return View();
        }
        // POST: QuanTriHeThongs_62131904/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Email và mật khẩu không được để trống.");
                return View();
            }

            // Tìm kiếm quản trị viên
            var admin = db.QuanTriHeThongs.FirstOrDefault(q => q.Email == email);
            if (admin == null)
            {
                ModelState.AddModelError("", "Email không tồn tại.");
                return View();
            }

            // Kiểm tra mật khẩu bằng cách so sánh mật khẩu đã mã hóa (hash) và mật khẩu người dùng nhập vào
            bool isPasswordValid = Authentication_Services.VerifyPassword(admin.Password, password);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Thông tin đăng nhập không đúng.");
                return View();
            }

            // Lưu thông tin đăng nhập vào Session
            Session["AdminId"] = admin.MaQT;
            Session["AdminEmail"] = admin.Email;
            Session["AdminName"] = admin.HoTen;
            Session["IsAdmin"] = admin.Admin;

            // Kiểm tra xem người dùng đã đăng nhập chưa
            if (Session["AdminId"] != null)
            {
                return RedirectToAction("Index", "SanPhams_62131904"); // Nếu đã đăng nhập, chuyển hướng tới trang chính
            }
            return View();
        }

        // GET: QuanTriHeThongs_62131904/Logout
        public ActionResult Logout()
        {
            // Xóa thông tin trong Session
            Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: QuanTriHeThongs_62131904/Register
        public ActionResult Register(string email, string password, string hoTen)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hoTen))
            {
                ModelState.AddModelError("", "All fields are required.");
                return View();
            }

            var hashedPassword = Authentication_Services.HashPassword(password);

            var newUser = new QuanTriHeThong
            {
                Email = email,
                HoTen = hoTen,
                Password = hashedPassword
            };

            db.QuanTriHeThongs.Add(newUser);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: QuanTriHeThongs_62131904/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(int userId, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                ModelState.AddModelError("", "New password is required.");
                return View();
            }

            var user = db.QuanTriHeThongs.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }

            user.Password = Authentication_Services.HashPassword(newPassword);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: QuanTriHeThongs_62131904
        public ActionResult Index()
        {
            return View(db.QuanTriHeThongs.ToList());
        }

        // GET: QuanTriHeThongs_62131904/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTriHeThong quanTriHeThong = db.QuanTriHeThongs.Find(id);
            if (quanTriHeThong == null)
            {
                return HttpNotFound();
            }
            return View(quanTriHeThong);
        }

        // GET: QuanTriHeThongs_62131904/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanTriHeThongs_62131904/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQT,Email,Admin,HoTen,Password")] QuanTriHeThong quanTriHeThong)
        {
            if (ModelState.IsValid)
            {
                quanTriHeThong.Password = Authentication_Services.HashPassword(quanTriHeThong.Password);
                db.QuanTriHeThongs.Add(quanTriHeThong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quanTriHeThong);
        }

        // GET: QuanTriHeThongs_62131904/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTriHeThong quanTriHeThong = db.QuanTriHeThongs.Find(id);
            if (quanTriHeThong == null)
            {
                return HttpNotFound();
            }
            return View(quanTriHeThong);
        }

        // POST: QuanTriHeThongs_62131904/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaQT,Email,Admin,HoTen,Password")] QuanTriHeThong quanTriHeThong)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu mật khẩu có thay đổi (người dùng có nhập mật khẩu mới)
                if (!string.IsNullOrEmpty(quanTriHeThong.Password))
                {
                    // Mã hóa mật khẩu mới
                    quanTriHeThong.Password = Authentication_Services.HashPassword(quanTriHeThong.Password);
                }
                // Cập nhật thông tin quản trị viên
                db.Entry(quanTriHeThong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quanTriHeThong);
        }

        // GET: QuanTriHeThongs_62131904/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTriHeThong quanTriHeThong = db.QuanTriHeThongs.Find(id);
            if (quanTriHeThong == null)
            {
                return HttpNotFound();
            }
            return View(quanTriHeThong);
        }

        // POST: QuanTriHeThongs_62131904/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuanTriHeThong quanTriHeThong = db.QuanTriHeThongs.Find(id);
            db.QuanTriHeThongs.Remove(quanTriHeThong);
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
