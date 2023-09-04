using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLBD.Areas.admin.Models.Background;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_BackgroundController : Controller
    {
        Background_Class clss = new Background_Class();
        Background_Model model = new Background_Model();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = clss.GetCount();
            dsResult.Data = clss.GetList(request.Page, request.PageSize);
            return Json(dsResult);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Edit");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View("Edit", clss.Get(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, Background_Model model)
        {
            model.ID = id;
            model.TRANG_THAI = 0;


            HttpPostedFileBase file = Request.Files["ImageUrl"] as HttpPostedFileBase;
            if (file != null && file.ContentLength > 0)
            {
                string filename = string.Format("{0:ddMMyyyyHHmmss}_{1}", DateTime.Now, file.FileName);
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Content/Web"), filename);
                file.SaveAs(filepath);
                model.IMG = filename;

            }

            clss.Capnhat(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            model.ID = id;
            clss.Delete(model);
            return RedirectToAction("Index");
        }

    }
}
