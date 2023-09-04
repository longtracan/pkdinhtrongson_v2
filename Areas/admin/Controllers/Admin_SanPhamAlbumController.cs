using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.Account;
using TLBD.Areas.admin.Models.SanPham_Album;
using TLBD.Areas.admin.Models.AlbumDetail;
using TLBD.Areas.admin.Models.NhomBaiViet;
using TLBD.Areas.admin.Models.BaiViet;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_SanPhamAlbumController : Controller
    {
        SanPham_Album_Class cls = new SanPham_Album_Class();
        AlbumDetail_Model model = new AlbumDetail_Model();

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

        [HttpGet]
        public ActionResult Add(int ?id) //id of album
        {
            int id_chuyen_muc = 25;
            var List_Nhom_SanPham = (from x in NhomSanPham_Class.List_Nhom_Baiviet_ChuyenMuc(id_chuyen_muc)
                                     select new SelectListItem
                                     {
                                         Value = x.ID.ToString(),
                                         Text = x.TEN_BAIVIET
                                     }).ToList();
            ViewData["NHOM_SANPHAM"] = List_Nhom_SanPham;

            var List_SanPham = (from x in SanPham_Class.GetList(id_chuyen_muc)
                                select new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = x.TIEU_DE
                                }).ToList();
            ViewData["SANPHAM"] = List_SanPham;

            return View("Edit");
        }
        [HttpGet]
        public ActionResult Edit(int ?id)
        {
            int id_chuyen_muc = 25;

            var model = cls.Get(id.Value);

            var List_Nhom_SanPham = (from x in NhomSanPham_Class.List_Nhom_Baiviet_ChuyenMuc(id_chuyen_muc)
                                     select new SelectListItem
                                     {
                                         Value = x.ID.ToString(),
                                         Text = x.TEN_BAIVIET,
                                         Selected = x.ID == model.sID_NHOM
                                     }).ToList();
            ViewData["NHOM_SANPHAM"] = List_Nhom_SanPham;

            var List_SanPham = (from x in SanPham_Class.GetList(model.sID_NHOM)
                                select new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = x.TIEU_DE,
                                    Selected = x.ID == model.sID_ALBUM
                                }).ToList();
            ViewData["SANPHAM"] = List_SanPham;

            return View("Edit", model);
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request, int id_sanpham)
        {
            SanPham_Album_Class cls = new SanPham_Album_Class();
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = cls.GetCount(id_sanpham);
            dsResult.Data = cls.GetList(request.Page, request.PageSize, id_sanpham);
            return Json(dsResult);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(int? id, AlbumDetail_Model model)
        {            
            model.sID = id.Value;
            model.sTRANG_THAI = 0;
            var id_album = model.sID_ALBUM;
            HttpPostedFileBase file = Request.Files["ImageUrl"] as HttpPostedFileBase;
            if (file != null && file.ContentLength > 0)
            {
                string filename = string.Format("{0:ddMMyyyyHHmmss}_{1}", DateTime.Now, file.FileName);
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Content/Album"), filename);
                file.SaveAs(filepath);
                model.sIMAGE_URL = filename;
            }
            cls.Capnhat(model);
            return RedirectToAction("Index", new { id = id_album });
        }

        public ActionResult Delete(int id, AlbumDetail_Model model)
        {
            var AlbumDetail = cls.Get(id);
            model.sID = id;
            var id_album = AlbumDetail.sID_ALBUM;
            cls.Delete(model);
            return RedirectToAction("Index", new { id = id_album });
        }

        public ActionResult FillData(int nsp_id)
        {
            var data = SanPham_Class.GetList(nsp_id);
            var result = Json(data, JsonRequestBehavior.AllowGet);
            return result;
        }

    }
}
