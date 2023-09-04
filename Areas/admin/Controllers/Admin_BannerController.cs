using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.Banner;


namespace TLBD.Areas.admin.Controllers
{
    public class Admin_BannerController : Controller
    {
        Banner_Class bn = new Banner_Class();
        Banner_Model model = new Banner_Model();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            Banner_Class bn = new Banner_Class();
            //Session["TRANG_THAI"] = Trang_thai;
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = bn.GetCount();
            dsResult.Data = bn.GetList(request.Page, request.PageSize);
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
            return View("Edit", bn.Get(id));

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, Banner_Model model)
        {
            model.ID = id;
            model.IP_SUA = Request.UserHostAddress;
            model.NGAY_SUA = DateTime.Now;
            model.IP_TAO = Request.UserHostAddress;
            model.NGAY_TAO = DateTime.Now;
            model.TRANG_THAI = 0;
            model.NGAY_XOA = DateTime.Now.AddYears(-1);

            HttpPostedFileBase file = Request.Files["ImageUrl"] as HttpPostedFileBase;
            if (file != null && file.ContentLength > 0)
            {
                string filename = string.Format("{0:ddMMyyyyHHmmss}_{1}", DateTime.Now, file.FileName);
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Content/Banner"), filename);
                file.SaveAs(filepath);
                model.IMG_URL = filename;

            }
            
            bn.Capnhat(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            model.ID = id;
            model.NGAY_XOA = DateTime.Now;
            model.IP_XOA = Request.UserHostAddress;

            bn.Delete(model);


            return RedirectToAction("Index");
        }

    }
}
