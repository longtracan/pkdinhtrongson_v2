using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLBD.Areas.admin.Models.PhanQuyen_NhomBaiViet;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_PhanQuyen_NhomBaiVietController : Controller
    {
        PhanQuyen_NhomBaiViet_Class pq = new PhanQuyen_NhomBaiViet_Class();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChuyenMuc(int id)
        {
            return PartialView("ChuyenMuc", id);
        }

        public ActionResult NguoiDung()
        {
            return PartialView("NguoiDung");
        }

        [HttpPost]
        public ActionResult CapNhat(int id_user, int id_nhombaiviet, int trang_thai)
        {
            PhanQuyen_NhomBaiViet_Model model = new PhanQuyen_NhomBaiViet_Model();
            model.sID_USER = id_user;
            model.sID_NHOMBAIVIET = id_nhombaiviet;
            model.sTRANG_THAI = trang_thai;

            pq.Capnhat(model);

            return Content("");
        }

        [HttpPost]
        public ActionResult Reset_Quyen(int id_user)
        {
            PhanQuyen_NhomBaiViet_Model model = new PhanQuyen_NhomBaiViet_Model();
            model.sID_USER = id_user;
            pq.Update(model);
            return Content("");
        }

    }
}
