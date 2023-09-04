using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.Account;
using TLBD.Areas.admin.Models.GioiTinh;


namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_NguoiDungController : Controller
    {
        AccountService ac = new AccountService();      
        User_Model model = new User_Model();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            AccountService ac = new AccountService();
           
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = ac.GetCount();
            dsResult.Data = ac.GetList(request.Page, request.PageSize);
            return Json(dsResult);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var List_GioiTinh = GioiTinh_Class.GetList()
                .Select(x => new SelectListItem
                                  {
                                      Value = x.ID.ToString(),
                                      Text = x.GIOITINH,
                                  }).ToList();
            ViewData["GIOI_TINH"] = List_GioiTinh;
            return View("Edit");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var List_GioiTinh = GioiTinh_Class.GetList()
                .Select(x => new SelectListItem
                {
                    Value = x.ID.ToString(),
                    Text = x.GIOITINH,
                }).ToList();
            ViewData["GIOI_TINH"] = List_GioiTinh;

            return View("Edit", ac.Get(id));

        }

        //[HttpPost]
        //public ActionResult LayDuLieu(FormCollection form)
        //{
        //    AccountService ac = new AccountService();
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
        //    ac.Approve(list);
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public ActionResult CapNhat(int id, User_Model model)
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
                string filepath = System.IO.Path.Combine(Server.MapPath("~/Content/User"), filename);
                file.SaveAs(filepath);
                model.sIMG_URL = filename;
            }

            ac.Capnhat(model);            

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            model.sID = id;
            model.sNGAY_XOA = DateTime.Now;
            model.sIP_XOA = Request.UserHostAddress;

            ac.Delete(model);


            return RedirectToAction("Index");
        }
    }
}
