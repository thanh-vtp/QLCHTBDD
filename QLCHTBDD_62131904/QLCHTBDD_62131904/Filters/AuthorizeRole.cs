using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCHTBDD_62131904.Filters
{
    public class AuthorizeRole : AuthorizeAttribute
    {
        private readonly string _role;

        // Constructor để truyền vai trò
        public AuthorizeRole(string role)
        {
            _role = role;
        }

        // Phương thức kiểm tra quyền truy cập
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Kiểm tra nếu người dùng đã đăng nhập và có vai trò hợp lệ
            var userRole = httpContext.Session["UserRole"]?.ToString();
            return userRole == _role;
        }

        // Nếu không được phép, chuyển hướng đến trang Login
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!AuthorizeCore(filterContext.HttpContext))
            {
                // Nếu không có quyền, chuyển hướng đến trang Login
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
        }
    }
}