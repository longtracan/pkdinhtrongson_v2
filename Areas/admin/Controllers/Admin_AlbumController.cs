using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.Album;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_AlbumController : Controller
    {
        Album_Class cls = new Album_Class();
        Album_Model model = new Album_Model();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public ActionResult Add()
        {           
            return View("Edit");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {           
            return View("Edit", cls.Get(id));
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            Album_Class cls = new Album_Class();
 
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = cls.GetCount();
            dsResult.Data = cls.GetList(request.Page, request.PageSize);
            return Json(dsResult);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, Album_Model model)
        {            
            model.sID = id;
            model.sIP_SUA = Request.UserHostAddress;
            model.sNGAY_SUA = DateTime.Now;
            model.sIP_TAO = Request.UserHostAddress;
            model.sNGAY_TAO = DateTime.Now;
            model.sTRANG_THAI = 0;

            cls.Capnhat(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            model.sID = id;
            model.sNGAY_XOA = DateTime.Now;
            model.sIP_XOA = Request.UserHostAddress;

            cls.Delete(model);


            return RedirectToAction("Index");
        }

    }
}
