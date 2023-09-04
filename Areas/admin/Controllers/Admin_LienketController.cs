using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.Lien_Ket;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_LienketController : Controller
    {
        //
        // GET: /Admin/Admin_Lienket/
        Lienket_Class lk = new Lienket_Class();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            Lienket_Class lk = new Lienket_Class();
            //Session["TRANG_THAI"] = Trang_thai;
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = lk.GetCount();
            dsResult.Data = lk.GetList(request.Page, request.PageSize);
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
            return View("Edit", lk.Get(id));

        }

        //[HttpPost]
        //public ActionResult LayDuLieu(FormCollection form)
        //{
        //    Lienket_Class lk = new Lienket_Class();
        //    IList<int> list = new List<int>();
        //    foreach (var key in form.Keys)
        //    {
        //        string strKey = key.ToString();
        //        if (strKey.StartsWith("id_"))
        //        {
        //            int idKey = Convert.ToInt32(strKey.Substring(3));
        //            list.Add(idKey);
        //        }
        //    }
        //    lk.Approve(list);
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public ActionResult CapNhat(int id, Lienket_Model model)
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
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Content/Lienket"), filename);
                file.SaveAs(filepath);
                model.sIMG_URL = filename;

            }

            lk.Capnhat(model);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Lienket_Model model = new Lienket_Model();
            model.sID = id;
            model.sNGAY_XOA = DateTime.Now;
            model.sIP_XOA = Request.UserHostAddress;

            lk.Delete(model);


            return RedirectToAction("Index");
        }

    }
}
