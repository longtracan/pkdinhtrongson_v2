using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLBD.Areas.admin.Models.BaiViet;
using TLBD.Areas.admin.Models.NhomBaiViet;
using TLBD.Areas.admin.Models.ChuyenMuc;
using AttributeRouting.Web.Mvc;
using TLBD.Areas.admin.Controllers;
using SautinSoft.Document;
using SautinSoft.Document.Tables;

namespace TLBD.Controllers
{
    public class NhomBaiVietController : Controller
    {        
        NhomBaiViet_Class nbv = new NhomBaiViet_Class();
        BaiViet_Class bv = new BaiViet_Class();
        ChuyenMuc_Class cm = new ChuyenMuc_Class();

        [GET("Chuyen-Muc/{TenCM}/{TenNBV}/{id}")] /*Danh sách bài viết theo nhóm bài viết*/
        public ActionResult Index(int id, int? trang)
        {

                int kichthuoc = 12;
                int soluong = bv.GetCount(id);
                int tongtrang = (soluong - 1) / kichthuoc + 1;
                List_BaiViet_View model = new List_BaiViet_View();
                model.Nhom_BaiViet = nbv.Get(id);
                model.ChuyenMuc = cm.Get(model.Nhom_BaiViet.ID_CHUYENMUC);

                ViewData["kiemtra"] = bv.GetCount(id);

                model.BaiViet = bv.Get_BaiViet(id);
                model.List_BaiViet = bv.List_index(trang ?? 1, kichthuoc, id);

                ViewData["trang"] = trang ?? 1;
                ViewData["tongtrang"] = tongtrang;

                return View("Index", model);

            
           
        }

        [GET("Dich-Vu-Noi-Bat/{TenNBV}/{id}")]
        public ActionResult DichVuNoiBat(int id, int? trang)
        {
          
                int kichthuoc = 6;
                int soluong = bv.GetCount(id);
                int tongtrang = (soluong - 1) / kichthuoc + 1;
                List_BaiViet_View model = new List_BaiViet_View();
                model.Nhom_BaiViet = nbv.Get(id);
                model.ChuyenMuc = cm.Get(model.Nhom_BaiViet.ID_CHUYENMUC);

                ViewData["kiemtra"] = bv.GetCount(id);

                model.BaiViet = bv.Get_BaiViet(id);
                model.List_BaiViet = bv.List_index(trang ?? 1, kichthuoc, id);

                ViewData["trang"] = trang ?? 1;
                ViewData["tongtrang"] = tongtrang;

                return View("DichVuNoiBat", model);
           
        }

        [GET("Doi-Ngu-Bac-Si/{id}")]
        public ActionResult DoiNguBacSi(int id, int? trang)
        {
            int kichthuoc = 6;
            int soluong = bv.GetCount(id);
            int tongtrang = (soluong - 1) / kichthuoc + 1;
            List_BaiViet_View model = new List_BaiViet_View();
            model.Nhom_BaiViet = nbv.Get(id);
            model.ChuyenMuc = cm.Get(model.Nhom_BaiViet.ID_CHUYENMUC);

            ViewData["kiemtra"] = bv.GetCount(id);

            model.BaiViet = bv.Get_BaiViet(id);
            model.List_BaiViet = bv.List_index_tt(trang ?? 1, kichthuoc, id);

            ViewData["trang"] = trang ?? 1;
            ViewData["tongtrang"] = tongtrang;
            return View("DoiNguBacSi", model);
        }

        [GET("Danh-Sach-Bai-Viet/{id}")] /*Danh sách bài viết theo chuyên mục*/
        public ActionResult DanhSachBaiVietTheoChuyenMuc(int id, int? trang)
        {
            int kichthuoc = 12;
            int soluong = bv.GetCount1(id);
            int tongtrang = (soluong - 1) / kichthuoc + 1;
            List_BaiViet_View model = new List_BaiViet_View();
            model.Nhom_BaiViet = nbv.Get1(id); //get nhombaiviet theo chuyen muc
            model.ChuyenMuc = cm.Get(id);

            ViewData["kiemtra"] = bv.GetCount(id);

            //model.BaiViet = bv.Get_BaiViet(id);
            model.List_BaiViet = bv.List_index_cm(trang ?? 1, kichthuoc, id);

            ViewData["trang"] = trang ?? 1;
            ViewData["tongtrang"] = tongtrang;
            return View("DanhSachBaiVietTheoChuyenMuc", model);
        }

        [GET("Dang-Ky-Kham-Tong-Quat")] /* theo chuyên mục*/
        public ActionResult DangKyKhamTongQuat(int? trang)
        {
            int id = 2071;
            int kichthuoc = 1000;
            int soluong = bv.GetCount1(id);
            int tongtrang = (soluong - 1) / kichthuoc + 1;
            List_BaiViet_View model = new List_BaiViet_View();
            model.Nhom_BaiViet = nbv.Get1(id); //get nhombaiviet theo chuyen muc
            model.ChuyenMuc = cm.Get(id);

            ViewData["kiemtra"] = bv.GetCount(id);

            //model.BaiViet = bv.Get_BaiViet(id);
            //model.List_BaiViet = bv.List_index_cm(trang ?? 1, kichthuoc, id);

            ViewData["trang"] = trang ?? 1;
            ViewData["tongtrang"] = tongtrang;
            return View("DangKyKhamTongQuat", model);
        }

        [GET("Hinh-Anh/{id}")]
        public ActionResult HinhAnh(int id, int? trang) /*Danh sách bài viết theo nhóm bài viết*/
        {
            int kichthuoc = 6;
            int soluong = bv.GetCount(id);
            int tongtrang = (soluong - 1) / kichthuoc + 1;
            List_BaiViet_View model = new List_BaiViet_View();
            model.Nhom_BaiViet = nbv.Get(id);
            model.ChuyenMuc = cm.Get(model.Nhom_BaiViet.ID_CHUYENMUC);

            ViewData["kiemtra"] = bv.GetCount(id);

            model.BaiViet = bv.Get_BaiViet(id);
            model.List_BaiViet = bv.List_index_tt(trang ?? 1, kichthuoc, id);

            ViewData["trang"] = trang ?? 1;
            ViewData["tongtrang"] = tongtrang;

            return View("HinhAnh", model);
        }
    }
}
