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
    public class  Admin_SanPhamController : Controller
    {
        SanPham_Class bv = new SanPham_Class();
        BaiViet_Model model = new BaiViet_Model();

        public ActionResult Index()
        {
            int id_chuyen_muc = 25;
            var List_Nhom_BaiViet = (from x in NhomSanPham_Class.List_Nhom_Baiviet_ChuyenMuc(id_chuyen_muc)
                                     select new SelectListItem
                                     {
                                         Value = x.ID.ToString(),
                                         Text = x.TEN_BAIVIET
                                     }).ToList();

            ViewData["NHOM_SANPHAM"] = List_Nhom_BaiViet;

            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request, int id_nhom_baiviet)
        {
            BaiViet_Class bv = new BaiViet_Class();
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = bv.GetCount(id_nhom_baiviet);
            dsResult.Data = bv.GetList(request.Page, request.PageSize, id_nhom_baiviet);
            return Json(dsResult);
        }

        [HttpGet]
        public ActionResult Add(int id_nh)
        {
            int id_cm = 25;
            NhomBaiViet_Class clss = new NhomBaiViet_Class();
            var List_Nhom_BaiViet = (from x in clss.List_Nhom_Baiviet_ChuyenMuc(id_cm)
                                     select new SelectListItem
                                     {
                                         Value = x.ID.ToString(),
                                         Text = x.TEN_BAIVIET,
                                         Selected = x.ID == id_nh
                                     }).ToList();

            ViewData["NHOM_SANPHAM"] = List_Nhom_BaiViet;

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
            int id_cm = 25;
            NhomBaiViet_Class clss = new NhomBaiViet_Class();
            var List_Nhom_BaiViet = (from x in clss.List_Nhom_Baiviet_ChuyenMuc(id_cm)
                                     select new SelectListItem
                                     {
                                         Value = x.ID.ToString(),
                                         Text = x.TEN_BAIVIET
                                     }).ToList();

            ViewData["NHOM_SANPHAM"] = List_Nhom_BaiViet;

            var List_Nguoivietbai = (from x in Nguoi_VietBai_Class.List_NguoiVietBai()
                                     select new SelectListItem
                                     {
                                         Value = x.sID.ToString(),
                                         Text = x.sTEN
                                     }).ToList();

            ViewData["NGUOI_VIETBAI"] = List_Nguoivietbai;

            return View("Edit", bv.Get(id));

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, [Bind(Exclude = "sNGAY_DANG")] BaiViet_Model model, string id_bvlq, string sNGAY_DANG)
        {
            DateTime ngaydang = Convert.ToDateTime(sNGAY_DANG, new System.Globalization.CultureInfo("vi-VN"));            
            model.ID = id;
            model.IP_SUA = Request.UserHostAddress;
            model.NGAY_SUA = DateTime.Now;
            model.IP_TAO = Request.UserHostAddress;
            model.NGAY_TAO = DateTime.Now;
            model.TRANG_THAI = 0;
            model.NGAY_DANG = ngaydang;
            model.ID_CHUYENMUC = 25;

            HttpPostedFileBase file = Request.Files["ImageUrl"] as HttpPostedFileBase;
            if (file != null && file.ContentLength > 0)
            {
                string filename = string.Format("{0:ddMMyyyyHHmmss}_{1}", DateTime.Now, file.FileName);
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Content/BaiViet"), filename);
                file.SaveAs(filepath);
                model.IMAGE_URL = filename;

            }

            file = Request.Files["ImageUrl_detail"] as HttpPostedFileBase;
            if (file != null && file.ContentLength > 0)
            {
                string filename = string.Format("{0:ddMMyyyyHHmmss}__{1}", DateTime.Now, file.FileName);
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Content/BaiViet"), filename);
                file.SaveAs(filepath);
                model.SP_IMGCHITIET = filename;
            }

            HttpPostedFileBase file_dowload = Request.Files["FileUrl"] as HttpPostedFileBase;
            if (file_dowload != null && file_dowload.ContentLength > 0)
            {
                string filename = string.Format("{0:ddMMyyyyHHmmss}_{1}", DateTime.Now, file_dowload.FileName);
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Content/File_Download"), filename);
                file_dowload.SaveAs(filepath);
                model.FILE_DOWNLOAD = filename;
            }


            var id_chuyenmuc = model.ID_CHUYENMUC;
            var id_nhom_baiviet = model.ID_NHOM_BAIVIET;

            bv.CapNhat(model);

            if (id_bvlq != null)
            {
                string[] bvlq = id_bvlq.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < bvlq.Length; i++)
                {
                    bv.CapNhat_BV_LienQuan(id, int.Parse(bvlq[i]));
                }
            }
            return RedirectToAction("Index", new { });
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
