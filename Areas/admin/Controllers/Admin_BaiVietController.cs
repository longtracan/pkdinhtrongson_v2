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
    public class  Admin_BaiVietController : Controller
    {
        BaiViet_Class bv = new BaiViet_Class();
        BaiViet_Model model = new BaiViet_Model();

        public ActionResult Index(int ?id_cm, int ?id_nh)
        {
            var username = User.Identity.Name;
            var userInfo = AccountService.Get(username);

            var List_ChuyenMuc = (from x in ChuyenMuc_Class.List_ChuyenMuc(userInfo.sID)
                                  select new SelectListItem
                                  {
                                      Value = x.ID.ToString(),
                                      Text = x.TEN_CHUYENMUC,
                                      Selected = x.ID == id_cm

                                  }).ToList();

            if (id_cm == 0 && List_ChuyenMuc.Count > 0) 
                List_ChuyenMuc[0].Selected = true;

            ViewData["CHUYEN_MUC"] = List_ChuyenMuc;
            if (id_nh == 0 && List_ChuyenMuc.Count > 0)
                List_ChuyenMuc[0].Selected = true;

            

            var List_Nhom_BaiViet = (from x in NhomBaiViet_Class.GetList_Nhom_Baiviet()
                                     select new SelectListItem
                                     {
                                         Value = x.ID.ToString(),
                                         Text = x.TEN_BAIVIET,
                                         Selected = x.ID == id_nh
                                     }).ToList();

            if (id_nh == 0 && List_Nhom_BaiViet.Count > 0)
                List_Nhom_BaiViet[0].Selected = true;

            ViewData["NHOM_BAIVIET"] = List_Nhom_BaiViet;

            return View();
        }

        public ActionResult GetNhom_BaiViet(int id_chuyen_muc)
        {
            NhomBaiViet_Class clss = new NhomBaiViet_Class();
            var List_Nhom_BaiViet = (from x in clss.List_Nhom_Baiviet_ChuyenMuc(id_chuyen_muc)
                                     select new SelectListItem
                                     {
                                         Value = x.ID.ToString(),
                                         Text = x.TEN_BAIVIET
                                     }).ToList();
            if (List_Nhom_BaiViet.Count > 0)
            {
                List_Nhom_BaiViet[0].Selected = true;
            }
            return Json(List_Nhom_BaiViet, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request, int id_nhom_baiviet)
        {
            BaiViet_Class bv = new BaiViet_Class();
            //Session["TRANG_THAI"] = Trang_thai;
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = bv.GetCount(id_nhom_baiviet);
            dsResult.Data = bv.GetList(request.Page, request.PageSize, id_nhom_baiviet);
            return Json(dsResult);
        }

        [HttpGet]
        public ActionResult Add(int id_cm, int id_nh)
        {
            NhomBaiViet_Class nbvClss = new NhomBaiViet_Class();
            var username = User.Identity.Name;
            var userInfo = AccountService.Get(username);
            var List_ChuyenMuc = (from x in ChuyenMuc_Class.List_ChuyenMuc(userInfo.sID)
                                  select new SelectListItem
                                  {
                                      Value = x.ID.ToString(),
                                      Text = x.TEN_CHUYENMUC,
                                      Selected = x.ID == id_cm
                                  }).ToList();

            ViewData["CHUYEN_MUC"] = List_ChuyenMuc;


            var List_Nhom_BaiViet = (from x in nbvClss.List_Nhom_Baiviet_ChuyenMuc(id_cm)
                                     select new SelectListItem
                                     {
                                         Value = x.ID.ToString(),
                                         Text = x.TEN_BAIVIET,
                                         Selected = x.ID == id_nh
                                     }).ToList();

            ViewData["NHOM_BAIVIET"] = List_Nhom_BaiViet;

            var List_Nguoivietbai = (from x in Nguoi_VietBai_Class.List_NguoiVietBai()
                                     select new SelectListItem
                                     {
                                         Value = x.sID.ToString(),
                                         Text = x.sTEN
                                     }).ToList();

            ViewData["NGUOI_VIETBAI"] = List_Nguoivietbai;
            return View("Edit");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            NhomBaiViet_Class nbvClss = new NhomBaiViet_Class();
            var username = User.Identity.Name;
            var userInfo = AccountService.Get(username);
            var model = bv.Get(id);
            var List_ChuyenMuc = (from x in ChuyenMuc_Class.List_ChuyenMuc(userInfo.sID)
                                  select new SelectListItem
                                  {
                                      Value = x.ID.ToString(),
                                      Text = x.TEN_CHUYENMUC,
                                      Selected = x.ID == model.ID_CHUYENMUC
                                  }).ToList();

            ViewData["CHUYEN_MUC"] = List_ChuyenMuc;

            var List_Nhom_BaiViet = (from x in nbvClss.List_Nhom_Baiviet_ChuyenMuc(model.ID_CHUYENMUC)
                                     select new SelectListItem
                                     {
                                         Value = x.ID.ToString(),
                                         Text = x.TEN_BAIVIET,
                                         Selected = x.ID == model.ID_NHOM_BAIVIET
                                     }).ToList();

            ViewData["NHOM_BAIVIET"] = List_Nhom_BaiViet;

            var List_Nguoivietbai = (from x in Nguoi_VietBai_Class.List_NguoiVietBai()
                                     select new SelectListItem
                                     {
                                         Value = x.sID.ToString(),
                                         Text = x.sTEN
                                     }).ToList();

            ViewData["NGUOI_VIETBAI"] = List_Nguoivietbai;

            return View("Edit", model);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, [Bind(Exclude = "NGAY_DANG")] BaiViet_Model model, string id_bvlq, string NGAY_DANG)
        {
            DateTime ngaydang = Convert.ToDateTime(NGAY_DANG, new System.Globalization.CultureInfo("vi-VN"));            
            model.ID = id;
            model.IP_SUA = Request.UserHostAddress;
            model.NGAY_SUA = DateTime.Now;
            model.IP_TAO = Request.UserHostAddress;
            model.NGAY_TAO = DateTime.Now;
            model.TRANG_THAI = 0;
            model.NGAY_DANG = ngaydang;     

            HttpPostedFileBase file = Request.Files["ImageUrl"] as HttpPostedFileBase;
            if (file != null && file.ContentLength > 0)
            {
                string filename = string.Format("{0:ddMMyyyyHHmmss}_{1}", DateTime.Now, file.FileName);
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Content/BaiViet"), filename);
                file.SaveAs(filepath);
                model.IMAGE_URL = filename;
            }

            var id_chuyenmuc = model.ID_CHUYENMUC;
            var id_nhom_baiviet = model.ID_NHOM_BAIVIET;

            bv.CapNhat(model);
            return RedirectToAction("Index", new { id_cm = id_chuyenmuc, id_nh = id_nhom_baiviet });
        }

        public ActionResult Delete(int id)
        {
            var baiviet = bv.Get(id);

            var id_chuyenmuc = baiviet.ID_CHUYENMUC;
            var id_nhom_baiviet = baiviet.ID_NHOM_BAIVIET;

            model.ID = id;
            model.NGAY_XOA = DateTime.Now;
            model.IP_XOA = Request.UserHostAddress;

            bv.Delete(model);

            return RedirectToAction("Index", new { id_cm = id_chuyenmuc, id_nh = id_nhom_baiviet });
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
    }
}
