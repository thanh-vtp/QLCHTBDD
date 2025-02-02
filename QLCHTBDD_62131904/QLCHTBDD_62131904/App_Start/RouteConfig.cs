﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLCHTBDD_62131904
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "HomePage_62131904", action = "DanhSachSanPham", id = UrlParameter.Optional }
                defaults: new { controller = "QuanLyKhachHang_62131904", action = "DanhSachKhanhHang", id = UrlParameter.Optional }
            );
        }
    }
}
