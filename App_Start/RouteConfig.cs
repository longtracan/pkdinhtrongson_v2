using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TLBD
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "BaiViet_Route",
                url: "Chi-Tiet/{TenBV}/{id}",
                defaults: new { controller = "BaiViet", action = "Index", TenBV = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "NhomBaiViet_Route",
                url: "Chuyen-Muc/{TenCM}/{TenNBV}/{id}",
                defaults: new { controller = "NhomBaiViet", action = "Index", TenCM = UrlParameter.Optional, TenNBV = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "DichVuNoiBat_Route",
                url: "Dich-Vu-Noi-Bat/{TenNBV}/{id}",
                defaults: new { controller = "NhomBaiViet", action = "DichVuNoiBat", TenNBV = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DanhSachBaiViet_Route",
                url: "Danh-Sach-Bai-Viet/{id}",
                defaults: new { controller = "NhomBaiViet", action = "DanhSachBaiVietTheoChuyenMuc",  id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DoiNguBacSi_Route",
                url: "Doi-Ngu-Bac-Si/{id}",
                defaults: new { controller = "NhomBaiViet", action = "DoiNguBacSi", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "HinhAnh_Route",
                url: "Hinh-Anh/{id}",
                defaults: new { controller = "NhomBaiViet", action = "HinhAnh", id = UrlParameter.Optional }
            );


            routes.MapRoute(
               name: "ChuyenMuc_Route",
               url: "Chuyen-Muc/{TenCM}/{id}",
               defaults: new { controller = "ChuyenMuc", action = "Index", TenCM = UrlParameter.Optional, id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Album_Route",
                url: "Hinh-anh",
                defaults: new { controller = "Album", action = "Index" }
            );
            routes.MapRoute(
                name: "AlbumDetail_Route",
                url: "Hinh-anh/Chi-tiet/{TenAlbum}/{id}",
                defaults: new { controller = "Album", action = "Detail", TenAlbum = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "LienHe_Route",
                url: "Lien-he",
                defaults: new { controller = "LienHe", action = "Index" }
            );

        }
    }
}