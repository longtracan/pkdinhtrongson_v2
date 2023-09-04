using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Drawing;
using TLBD.Areas.admin.Models.PhanQuyen_Menu;
using TLBD.Areas.admin.Models.Account;

namespace TLBD.Areas.admin.Controllers
{
    [Authorize]
    public class Admin_PhanQuyenController : Controller
    {
        PhanQuyen_Menu_Class pq = new PhanQuyen_Menu_Class();      

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu(int id)
        {        
            return PartialView("Menu", id);
        }

        public ActionResult NguoiDung()
        {
            return PartialView("NguoiDung");
        }

        [HttpPost]
        public ActionResult CapNhat(int id_user, int id_menu,  int trang_thai)
        {
            PhanQuyen_Menu_Model model = new PhanQuyen_Menu_Model();
            model.sID_USER = id_user;
            model.sID_MENU = id_menu;
            model.sTRANG_THAI = trang_thai;

            pq.Capnhat(model);

            return Content("");
        }

        [HttpPost]
        public ActionResult Reset_Quyen(int id_user)
        {
            PhanQuyen_Menu_Model model = new PhanQuyen_Menu_Model();
            model.sID_USER = id_user;
            pq.Update(model);
            return Content("");
        }
        

        public ActionResult List_Menu_PhanQuyen()
        {
            var username = User.Identity.Name;
            var userInfo = AccountService.Get(username);            
            return PartialView("Partial_Admin_Menu", userInfo.sID);
        }

        public ActionResult Menu_Admin()
        {
            return PartialView("Admin_Menu");
        }

        public ActionResult Menu_user()
        {
            var username = User.Identity.Name;
            var userInfo = AccountService.Get(username);
            return PartialView("Menu_User", userInfo.sID);
        }

        public ActionResult Chao_DangNhap()
        {
            var username = User.Identity.Name;
            var userInfo = AccountService.Get(username);

            if (userInfo == null)
            {
                return RedirectToAction("Logon", "Account");
            }
         
            return PartialView("Chao_DangNhap", userInfo.sTEN_USER);
           
        }

    }
}
