using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.ChuyenMuc;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_ChuyenMucController : Controller
    {
        ChuyenMuc_Class cm = new ChuyenMuc_Class();
        ChuyenMuc_Model model = new ChuyenMuc_Model();
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
            return View("Edit", cm.Get(id));
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            ChuyenMuc_Class cm = new ChuyenMuc_Class();
            //Session["TRANG_THAI"] = Trang_thai;
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = cm.GetCount();
            dsResult.Data = cm.GetList(request.Page, request.PageSize);
            return Json(dsResult);
        }

        //[HttpPost]
        //public ActionResult LayDuLieu(FormCollection form)
        //{

        //    IList<int> list_nsp = new List<int>();
        //    foreach (var key in form.Keys)
        //    {
        //        string strKey = key.ToString();
        //        if (strKey.StartsWith("id_"))
        //        {
        //            int idKey = Convert.ToInt32(strKey.Substring(3));
        //            list_nsp.Add(idKey);
        //        }
        //    }
        //    cm.Approve(list_nsp);
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(int id, ChuyenMuc_Model model)
        {
            model.ID = id;
            model.IP_SUA = Request.UserHostAddress;
            model.NGAY_SUA = DateTime.Now;
            model.IP_TAO = Request.UserHostAddress;
            model.NGAY_TAO = DateTime.Now;
            model.TRANG_THAI = 0;

            cm.Capnhat(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            model.ID = id;
            model.NGAY_XOA = DateTime.Now;
            model.IP_XOA = Request.UserHostAddress;

            cm.Delete(model);


            return RedirectToAction("Index");
        }

    }
}
