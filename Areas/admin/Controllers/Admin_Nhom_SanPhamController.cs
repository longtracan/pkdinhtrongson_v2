using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.NhomBaiViet;
using TLBD.Areas.admin.Models.Loai_HienThi;
using TLBD.Areas.admin.Models.ChuyenMuc;
using TLBD.Areas.admin.Models.Account;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_NhomSanPhamController : Controller
    {
        NhomSanPham_Class nbv = new NhomSanPham_Class();
        NhomBaiViet_Model model = new NhomBaiViet_Model();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            //var List_Loai_HienThi = (from x in Loai_HienThi_Class.GetList_Loai_HienThi()
            //                   select new SelectListItem
            //                   {
            //                       Value = x.sID.ToString(),
            //                       Text = x.sLOAI_HIENTHI
            //                   }).ToList();

            //ViewData["LOAI_HIENTHI"] = List_Loai_HienThi;


            //var username = User.Identity.Name;
            //var userInfo = AccountService.Get(username);
            //var List_ChuyenMuc = (from x in ChuyenMuc_Class.List_ChuyenMuc(userInfo.sID)
            //                      select new SelectListItem
            //                      {
            //                          Value = x.sID.ToString(),
            //                          Text = x.sTEN_CHUYENMUC
            //                      }).ToList();

            //ViewData["CHUYEN_MUC"] = List_ChuyenMuc;
            return View("Edit");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //var List_Loai_HienThi = (from x in Loai_HienThi_Class.GetList_Loai_HienThi()
            //                         select new SelectListItem
            //                         {
            //                             Value = x.sID.ToString(),
            //                             Text = x.sLOAI_HIENTHI
            //                         }).ToList();

            //ViewData["LOAI_HIENTHI"] = List_Loai_HienThi;


            //var username = User.Identity.Name;
            //var userInfo = AccountService.Get(username);
            //var List_ChuyenMuc = (from x in ChuyenMuc_Class.List_ChuyenMuc(userInfo.sID)
            //                      select new SelectListItem
            //                      {
            //                          Value = x.sID.ToString(),
            //                          Text = x.sTEN_CHUYENMUC
            //                      }).ToList();

            //ViewData["CHUYEN_MUC"] = List_ChuyenMuc;
            return View("Edit", nbv.Get(id));
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            int id_chuyenmuc = 25;
            NhomSanPham_Class nbv = new NhomSanPham_Class();
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = nbv.GetCount(id_chuyenmuc);
            dsResult.Data = nbv.GetList(request.Page, request.PageSize, id_chuyenmuc);
            return Json(dsResult);
        }

        //[HttpPost]
        //public ActionResult LayDuLieu(FormCollection form)
        //{

        //    IList<int> list_nsp = new List<int>();
        //    foreach (var key in form.Keys)
        //    {
        //        string strKey = key.ToString();
        //        if (strKey.StartsWith("id_"))
        //        {
        //            int idKey = Convert.ToInt32(strKey.Substring(3));
        //            list_nsp.Add(idKey);
        //        }
        //    }
        //    nbv.Approve(list_nsp);
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, NhomBaiViet_Model model)
        {            
            model.ID = id;
            model.IP_SUA = Request.UserHostAddress;
            model.NGAY_SUA = DateTime.Now;
            model.IP_TAO = Request.UserHostAddress;
            model.NGAY_TAO = DateTime.Now;
            model.TRANG_THAI = 0;
            model.LOAI_HIENTHI = 4;
            model.ID_CHUYENMUC = 25;

            var id_chuyenmuc = model.ID_CHUYENMUC;

            nbv.Capnhat(model);
            return RedirectToAction("Index", new { id = id_chuyenmuc });
        }

        public ActionResult Delete(int id, NhomBaiViet_Model model)
        {
            var nhombaiviet = nbv.Get(id);
            model.ID = id;
            model.NGAY_XOA = DateTime.Now;
            model.IP_XOA = Request.UserHostAddress;

            var id_chuyenmuc = nhombaiviet.ID_CHUYENMUC;

            nbv.Delete(model);

            return RedirectToAction("Index", new { id = id_chuyenmuc });
        }

    }
}
