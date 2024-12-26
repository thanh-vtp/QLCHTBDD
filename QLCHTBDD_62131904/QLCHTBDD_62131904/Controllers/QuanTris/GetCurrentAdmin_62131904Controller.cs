using QLCHTBDD_62131904.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLCHTBDD_62131904.Controllers.QuanTris
{
    public class GetCurrentAdmin_62131904Controller : Controller
    {
        QLCHTBDD_62131904Entities db = new QLCHTBDD_62131904Entities();

        // GET: GetCurrentAdmin_62131904
        public ActionResult GetCurrentAdmin()
        {
            // Kiểm tra xem người dùng có đang đăng nhập hay không (kiểm tra Session)
            if (Session["AdminId"] == null)
            {
                // Nếu không đăng nhập, chuyển hướng về trang đăng nhập
                return RedirectToAction("Login", "QuanTriHeThongs_62131904");
            }

            // Lấy thông tin quản trị viên từ Session
            int adminId = (int)Session["AdminId"];
            string adminEmail = Session["AdminEmail"].ToString();
            string adminName = Session["AdminName"].ToString();
            bool isAdmin = (bool)Session["IsAdmin"];

            // Tạo đối tượng quanTriHeThong để trả về thông tin quản trị viên
            var admin = new QuanTriHeThong
            {
                MaQT = adminId,
                Email = adminEmail,
                HoTen = adminName,
                Admin = isAdmin
            };

            // Trả về View và hiển thị thông tin quản trị viên
            return View(admin);
        }
    }
}