using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLBD.Areas.admin.Models.BaiViet;
using TLBD.Areas.admin.Models.NhomBaiViet;
using TLBD.Areas.admin.Models.ChuyenMuc;
using AttributeRouting.Web.Mvc;
using TLBD.Areas.admin.Controllers;

namespace TLBD.Controllers
{
    public class BaiVietController : Controller
    {
        BaiViet_Class bv = new BaiViet_Class();
        NhomBaiViet_Class nbv = new NhomBaiViet_Class();
        ChuyenMuc_Class cm = new ChuyenMuc_Class();

        [GET("Chi-Tiet/{TenBV}/{id}")]
        public ActionResult Index(int id)
        {
            List_BaiViet_View model = new List_BaiViet_View();         
            model.Nhom_BaiViet = nbv.Get_BaiViet(id);
            model.BaiViet = bv.Get(id);
            model.ChuyenMuc = cm.Get(model.Nhom_BaiViet.ID_CHUYENMUC);
            return View(model);           
        }


        

    }
}
