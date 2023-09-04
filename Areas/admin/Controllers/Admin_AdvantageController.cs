using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.Advantage;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_AdvantageController : Controller
    {
        Advantage_Class clss = new Advantage_Class();
        Advantage_Model model = new Advantage_Model();

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
        public ActionResult CapNhat(int id, Advantage_Model model)
        {
            model.sID = id;
            model.sTRANG_THAI = 0;

            clss.Capnhat(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            model.sID = id;
            clss.Delete(model);

            return RedirectToAction("Index");
        }
    }
}
