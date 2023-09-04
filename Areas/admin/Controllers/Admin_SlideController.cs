using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.Slide;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_SlideController : Controller
    {
        Slide_Class sl = new Slide_Class();
        Slide_Model model = new Slide_Model();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            Slide_Class sl = new Slide_Class();
            //Session["TRANG_THAI"] = Trang_thai;
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = sl.GetCount();
            dsResult.Data = sl.GetList(request.Page, request.PageSize);
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
            return View("Edit", sl.Get(id));

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, Slide_Model model)
        {            
            model.sID = id;
            model.sIP_SUA = Request.UserHostAddress;
            model.sNGAY_SUA = DateTime.Now;
            model.sIP_TAO = Request.UserHostAddress;
            model.sNGAY_TAO = DateTime.Now;
            model.sTRANG_THAI = 0;           


            HttpPostedFileBase file = Request.Files["ImageUrl"] as HttpPostedFileBase;
            if (file != null && file.ContentLength > 0)
            {
                string filename = string.Format("{0:ddMMyyyyHHmmss}_{1}", DateTime.Now, file.FileName);
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Content/Slide"), filename);
                file.SaveAs(filepath);
                model.sIMG_URL = filename;
            }

            sl.Capnhat(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            model.sID = id;
            model.sNGAY_XOA = DateTime.Now;
            model.sIP_XOA = Request.UserHostAddress;

            sl.Delete(model);


            return RedirectToAction("Index");
        }
    }
}
