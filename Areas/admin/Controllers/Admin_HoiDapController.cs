using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.CauHoi;
using TLBD.Areas.admin.Models.Account;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_HoiDapController : Controller
    {
        CauHoi_Class cls = new CauHoi_Class();
        CauHoi_Model model = new CauHoi_Model();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request, string start_date, string end_date)
        {
            DateTime sDate = Convert.ToDateTime(start_date);
            DateTime eDate = Convert.ToDateTime(end_date);

            CauHoi_Class cls = new CauHoi_Class();            
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = cls.GetCount(sDate.ToString("dd-MM-yyyy"), eDate.ToString("dd-MM-yyyy"));
            dsResult.Data = cls.GetList(request.Page, request.PageSize, sDate.ToString("dd-MM-yyyy"), eDate.ToString("dd-MM-yyyy"));
            return Json(dsResult);
        }

        //[HttpGet]
        //public ActionResult Add()
        //{            
        //    return View("Edit");
        //}

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View("Edit", cls.Get(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, CauHoi_Model model)
        {
            model.ID = id;
            cls.CapNhat(model);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            model.ID = id;
            cls.Delete(model);
            return RedirectToAction("Index");
        }
    }
}
