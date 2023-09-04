using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLBD.Areas.admin.Models.ThongTin;

namespace TLBD.Areas.admin.Controllers
{
    public class Admin_ThongTinController : Controller
    {
        ThongTin_Class tt = new ThongTin_Class();
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["List_TT"] = (from x in tt.Get_List_TT()
                                   select new SelectListItem
                                   {
                                       Value = x.sID.ToString()
                                   }).ToList();
            return View("Index", tt.Get());

        }

        [HttpPost]
        public ActionResult CapNhat(ThongTin_Model model)
        {
            model.sIP_SUA = Request.UserHostAddress;
            model.sNGAY_SUA = DateTime.Now;

            HttpPostedFileBase file = Request.Files["ImageUrl"] as HttpPostedFileBase;
            if (file != null && file.ContentLength > 0)
            {
                string filename = string.Format("{0:ddMMyyyyHHmmss}_{1}", DateTime.Now, file.FileName);
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Content/Map"), filename);
                file.SaveAs(filepath);
                model.MAP_IMG = filename;
            }

            tt.CapNhat(model);

            return RedirectToAction("ChangeSuccess");
        }

        public ActionResult ChangeSuccess()
        {
            return View();
        }

    }
}
