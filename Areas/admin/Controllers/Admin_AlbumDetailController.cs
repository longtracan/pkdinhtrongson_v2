using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.AlbumDetail;
using TLBD.Areas.admin.Models.Album;
using TLBD.Areas.admin.Models.Account;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_AlbumDetailController : Controller
    {
        AlbumDetail_Class cls = new AlbumDetail_Class();
        AlbumDetail_Model model = new AlbumDetail_Model();

        public ActionResult Index(int ?id)
        {
            var List_Album = (from x in Album_Class.List_AllAlbum()
                                  select new SelectListItem
                                  {
                                      Value = x.sID.ToString(),
                                      Text = x.sTIEU_DE
                                  }).ToList();

            ViewData["ALBUM"] = List_Album;
            return View();
        }

        [HttpGet]
        public ActionResult Add(int id) //id of album
        {
            var List_Album = (from x in Album_Class.List_AllAlbum()
                               select new SelectListItem
                               {
                                   Value = x.sID.ToString(),
                                   Text = x.sTIEU_DE,
                                   Selected = id == x.sID
                               }).ToList();

            ViewData["ALBUM"] = List_Album;
            return View("Edit");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var List_Album = (from x in Album_Class.List_AllAlbum()
                              select new SelectListItem
                              {
                                  Value = x.sID.ToString(),
                                  Text = x.sTIEU_DE
                              }).ToList();

            ViewData["ALBUM"] = List_Album;
            return View("Edit", cls.Get(id));
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request, int id_album)
        {
            AlbumDetail_Class cls = new AlbumDetail_Class();
            
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = cls.GetCount(id_album);
            dsResult.Data = cls.GetList(request.Page, request.PageSize, id_album);
            return Json(dsResult);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, AlbumDetail_Model model)
        {            
            model.sID = id;
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

    }
}
