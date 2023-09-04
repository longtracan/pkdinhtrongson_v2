using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.Nguoi_VietBai;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Nguoi_VietBaiController : Controller
    {
        Nguoi_VietBai_Class nvb = new Nguoi_VietBai_Class();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            Nguoi_VietBai_Class nvb = new Nguoi_VietBai_Class();
            //Session["TRANG_TnvbI"] = Trang_tnvbi;
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = nvb.GetCount();
            dsResult.Data = nvb.GetList(request.Page, request.PageSize);
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
            return View("Edit", nvb.Get(id));

        }


        [HttpPost]
        public ActionResult CapNhat(int id, Nguoi_VietBai_Model model)
        {
            model.sID = id;
            model.sIP_SUA = Request.UserHostAddress;
            model.sNGAY_SUA = DateTime.Now;
            model.sIP_TAO = Request.UserHostAddress;
            model.sNGAY_TAO = DateTime.Now;
            model.sTRANG_THAI = 0;

            nvb.CapNhat(model);

            return RedirectToAction("Index");
        }        
        public ActionResult Delete(int id)
        {
            Nguoi_VietBai_Model model = new Nguoi_VietBai_Model();
            model.sID = id;
            model.sNGAY_XOA = DateTime.Now;
            model.sIP_XOA = Request.UserHostAddress;

            nvb.Delete(model);


            return RedirectToAction("Index");
        }

    }
}
