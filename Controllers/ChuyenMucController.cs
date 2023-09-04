using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLBD.Areas.admin.Models.BaiViet;
using TLBD.Areas.admin.Models.ChuyenMuc;
using TLBD.Areas.admin.Models.NhomBaiViet;

namespace TLBD.Controllers
{
    public class ChuyenMucController : Controller
    {
        NhomBaiViet_Class nbv = new NhomBaiViet_Class();
        BaiViet_Class bv = new BaiViet_Class();
        ChuyenMuc_Class cm = new ChuyenMuc_Class();

        [GET("Chuyen-Muc/{TenCM}/{id}")]
        public ActionResult Index(int id, int? trang)
        {
            int kichthuoc = 10;
            int soluong = bv.GetCount1(id);
            int tongtrang = (soluong - 1) / kichthuoc + 1;
            List_BaiViet_View model = new List_BaiViet_View();
          
            model.ChuyenMuc = cm.Get(id);

            ViewData["kiemtra"] = bv.GetCount(id);

            model.BaiViet = bv.Get_BaiViet(id);
            model.List_BaiViet = bv.List_index_cm(trang ?? 1, kichthuoc, id);

            ViewData["trang"] = trang ?? 1;
            ViewData["tongtrang"] = tongtrang;

            return View("Index", model);

        }
    }
}
