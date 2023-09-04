using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLBD.Areas.admin.Models.Logo;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_LogoController : Controller
    {
        Logo_Class lg = new Logo_Class();
        Logo_Model model = new Logo_Model();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            Logo_Class lg = new Logo_Class();
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = lg.GetCount();
            dsResult.Data = lg.GetList(request.Page, request.PageSize);
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
            return View("Edit", lg.Get(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, Logo_Model model)
        {
            model.sID = id;
            model.sIP_SUA = Request.UserHostAddress;
            model.sNGAY_SUA = DateTime.Now;
            model.sIP_TAO = Request.UserHostAddress;
            model.sNGAY_TAO = DateTime.Now;
            model.sTRANG_THAI = 0;
            model.sNGAY_XOA = DateTime.Now.AddYears(-1);


            HttpPostedFileBase file = Request.Files["ImageUrl"] as HttpPostedFileBase;
            if (file != null && file.ContentLength > 0)
            {
                string filename = string.Format("{0:ddMMyyyyHHmmss}_{1}", DateTime.Now, file.FileName);
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Content/Web"), filename);
                file.SaveAs(filepath);
                model.sIMG_URL = filename;

            }

            lg.Capnhat(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            model.sID = id;
            model.sNGAY_XOA = DateTime.Now;
            model.sIP_XOA = Request.UserHostAddress;

            lg.Delete(model);


            return RedirectToAction("Index");
        }

    }
}
