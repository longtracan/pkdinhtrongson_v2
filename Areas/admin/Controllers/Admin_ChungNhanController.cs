using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.ChungNhan;


namespace TLBD.Areas.admin.Controllers
{
    public class Admin_ChungNhanController : Controller
    {
        ChungNhan_Class clss = new ChungNhan_Class();
        ChungNhan_Model model = new ChungNhan_Model();

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
        public ActionResult CapNhat(int id, ChungNhan_Model model)
        {
            model.ID = id;
            model.TRANG_THAI = 0;

            HttpPostedFileBase file = Request.Files["ImageUrl"] as HttpPostedFileBase;
            if (file != null && file.ContentLength > 0)
            {
                string filename = string.Format("{0:ddMMyyyyHHmmss}_{1}", DateTime.Now, file.FileName);
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Images/ChungNhan"), filename);
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
