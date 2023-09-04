using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.Order;
using TLBD.Areas.admin.Models.OrderItem;
using WebMatrix.WebData;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_OrderController : Controller
    {
        Order_Class cOrder = new Order_Class();
        OrderItem_Model model = new OrderItem_Model();

        public ActionResult Index()
        {
            var List_TrangThai = (from x in cOrder.GetOrderStatusList()
                                  select new SelectListItem
                                  {
                                      Value = x.ID.ToString(),
                                      Text = x.Description
                                  }).ToList();

            ViewData["TRANG_THAI"] = List_TrangThai;
            return View();
        }


        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = cOrder.Get(id);
            var List_TrangThai = (from x in cOrder.GetOrderStatusList()
                                     select new SelectListItem
                                     {
                                         Value = x.ID.ToString(),
                                         Text = x.Description,
                                         Selected = x.ID == model.StatusID
                                     }).ToList();

            ViewData["TRANG_THAI"] = List_TrangThai;
            return View("Edit", model);
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request, int id_trangthai, string start_date, string end_date)
        {
            int _userid = WebSecurity.GetUserId(User.Identity.Name);
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = cOrder.GetOrderCount(id_trangthai, start_date, end_date, _userid);
            dsResult.Data = cOrder.GetOrderList(request.Page, request.PageSize, id_trangthai, start_date, end_date, _userid);
            return Json(dsResult);
        }

        public ActionResult DetailRead([DataSourceRequest]DataSourceRequest request, string id_donhang)
        {
            DataSourceResult dsResult = new DataSourceResult();
            dsResult.Total = cOrder.GetOrderDetailCount(id_donhang);
            dsResult.Data = cOrder.GetOrderDetailList(request.Page, request.PageSize, id_donhang);
            return Json(dsResult);
        }

        [HttpPost]
        public ActionResult LayDuLieu(FormCollection form)
        {
            IList<int> list_nsp = new List<int>();
            foreach (var key in form.Keys)
            {
                string strKey = key.ToString();
                if (strKey.StartsWith("id_"))
                {
                    int idKey = Convert.ToInt32(strKey.Substring(3));
                    list_nsp.Add(idKey);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhat(OrderItem_Model model)
        {
            cOrder.Capnhat(model);
            return RedirectToAction("Index");
        }
    }
}
