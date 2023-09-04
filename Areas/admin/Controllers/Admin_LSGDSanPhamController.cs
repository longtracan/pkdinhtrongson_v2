using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.ChuyenMuc;
using TLBD.Areas.admin.Models.NhomBaiViet;
using TLBD.Areas.admin.Models.BaiViet;
using TLBD.Areas.admin.Models.Nguoi_VietBai;
using TLBD.Areas.admin.Models.Account;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_LSGDSanPhamController : Controller
    {
        BaiViet_Class bv = new BaiViet_Class();
        BaiViet_Model model = new BaiViet_Model();

        public ActionResult Index(int? id_nsp, int? id_sp)
        {
            int id_chuyen_muc = 25;
            var List_Nhom_SanPham = (from x in NhomSanPham_Class.List_Nhom_Baiviet_ChuyenMuc(id_chuyen_muc)
                                     select new SelectListItem
                                     {
                                         Value = x.ID.ToString(),
                                         Text = x.TEN_BAIVIET,
                                         Selected = x.ID == id_nsp
                                     }).ToList();

            ViewData["NHOM_SANPHAM"] = List_Nhom_SanPham;

            if (id_nsp == 0 && List_Nhom_SanPham.Count > 0)
            {
                List_Nhom_SanPham[0].Selected = true;
            }

            var List_SanPham = (from x in SanPham_Class.GetList(id_chuyen_muc)
                                select new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = x.TIEU_DE,
                                    Selected = x.ID == id_sp
                                }).ToList();

            if (id_sp == 0 && List_SanPham.Count > 0)
                List_SanPham[0].Selected = true;

            ViewData["SANPHAM"] = List_SanPham;

            return View();
        }

        public ActionResult GetSanPham(int id_nhomsanpham)
        {
            var List_SanPham = (from x in SanPham_Class.GetList(id_nhomsanpham)
                                select new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = x.TIEU_DE
                                }).ToList();
            if (List_SanPham.Count > 0)
            {
                List_SanPham[0].Selected = true;
            }
            return Json(List_SanPham, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request, int id_sanpham, string start_date, string end_date)
        {
            DateTime sDate = Convert.ToDateTime(start_date);
            DateTime eDate = Convert.ToDateTime(end_date);

            SanPham_Class bv = new SanPham_Class();
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = bv.GetLSCount(id_sanpham, sDate.ToString("dd-MM-yyyy"), eDate.ToString("dd-MM-yyyy"));
            dsResult.Data = bv.GetLSList(request.Page, request.PageSize, id_sanpham, sDate.ToString("dd-MM-yyyy"), eDate.ToString("dd-MM-yyyy"));
            return Json(dsResult);
        }

        //[HttpPost]
        //public ActionResult LayDuLieu(FormCollection form)
        //{
        //    IList<int> list_kq = new List<int>();
        //    foreach (var key in form.Keys)
        //    {
        //        string strKey = key.ToString();
        //        if (strKey.StartsWith("id_"))
        //        {
        //            int idKey = Convert.ToInt32(strKey.Substring(3));
        //            list_kq.Add(idKey);
        //        }
        //    }
        //    bv.Approve(list_kq);
        //    return RedirectToAction("List");
        //}

        public ActionResult LSGD()
        {
            return View();
        }

        public ActionResult ReadLSGD([DataSourceRequest]DataSourceRequest request, string start_date, string end_date)
        {
            DateTime sDate = Convert.ToDateTime(start_date);
            DateTime eDate = Convert.ToDateTime(end_date);

            SanPham_Class bv = new SanPham_Class();
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = bv.GetLSGDCount(sDate.ToString("dd-MM-yyyy"), eDate.ToString("dd-MM-yyyy"));
            dsResult.Data = bv.GetLSGDList(request.Page, request.PageSize, sDate.ToString("dd-MM-yyyy"), eDate.ToString("dd-MM-yyyy"));
            return Json(dsResult);
        }


    }
}
