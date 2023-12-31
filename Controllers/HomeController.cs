﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLBD.Areas.admin.Models.Lien_Ket;
using TLBD.Areas.admin.Models.ChuyenMuc;
using TLBD.Areas.admin.Models.NhomBaiViet;
using TLBD.Areas.admin.Models.BaiViet;
using TLBD.Areas.admin.Models.ThongTin;
using TLBD.Areas.admin.Models.Slide;
using TLBD.Areas.admin.Models.Slogan;
using TLBD.Models;
using System.Web.Security;
using WebMatrix.WebData;
using TLBD.Areas.admin.Controllers;
using TLBD.Areas.admin.Models.CartItem;
using TLBD.Areas.admin.Models.Cart;
using Newtonsoft.Json;
using TLBD.Areas.admin.Models.Advantage;
using TLBD.Areas.admin.Models.AlbumDetail;
using System.Data.SqlClient;
using System.Data;
using TLBD.Areas.admin.Models.Account;

namespace TLBD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Header()
        {
            var username = User.Identity.Name;
            ViewBag.Username = username;

            if (username == "")
            {
                var cookie = cHelper.GetCookie(Request, cHelper.CART_COOKIE);
                List<CartItem_Model> cookie_lst = null;
                if (cookie != "")
                {
                    cookie_lst = JsonConvert.DeserializeObject<List<CartItem_Model>>(cookie);
                }
                ViewBag.CartCount = cookie_lst == null ? 0 : cookie_lst.Sum(x => x.Quantity);
            }
            else
            {
                ViewBag.CartCount = Cart_Class.GetCartCount(username);
            }



            return PartialView("PartialHeader");
        }

        public ActionResult RightRegion()
        {
            return PartialView("PartialRightRegion");
        }

        public ActionResult SlideImage2()
        {
            AlbumDetail_Class alb_clss = new AlbumDetail_Class();
            return PartialView("PartialSlideImage2", alb_clss.GetListAlbumChitiet(8));
        }


        public ActionResult Menu()
        {
            ViewData["SendZalo"] = 0;
            var username = User.Identity.Name;
            if (username != "")
            {
                User_Model user = SELECT_USERDETAIL(username);
                if (user.sEMAIL == "SendZalo@gmail.com")
                {
                    ViewData["SendZalo"] = 1;
                }

            }
            ChuyenMuc_Class clss = new ChuyenMuc_Class();
            return PartialView("PartialMenu", clss.List_Menu());
        }

        public ActionResult Slider()
        {
            Slide_Class clss = new Slide_Class();
            return PartialView("PartialSlider", clss.List_Slide());
        }

        public ActionResult Welcome()
        {
            ThongTin_Class clss = new ThongTin_Class();
            return PartialView("PartialWelcome", clss.Get());
        }

        public ActionResult Service()
        {
          
            ThongTin_Class clss = new ThongTin_Class();
            NhomBaiViet_Class nbv_clss = new NhomBaiViet_Class();
            return PartialView("PartialService", nbv_clss.List_Nhom_Baiviet_ChuyenMuc(1069));
        }

        public ActionResult Doctor()
        {
            BaiViet_Class bv_clss = new BaiViet_Class();
            return PartialView("PartialDoctor", bv_clss.GetList_noibat_tt(1242).Take(6).ToList());
        }

        public ActionResult Department()
        {
            BaiViet_Class bv_clss = new BaiViet_Class();
            return PartialView("PartialDepartment", bv_clss.GetList_tt(1243));
        }

        public ActionResult SlideImage()
        {
            AlbumDetail_Class alb_clss = new AlbumDetail_Class();
            ThongTin_Class clss = new ThongTin_Class();
            return PartialView("PartialSlideImage", alb_clss.GetListAlbumChitiet(6));
        }

        public ActionResult FAQ()
        {
            BaiViet_Class bv_clss = new BaiViet_Class();
            ThongTin_Class clss = new ThongTin_Class();
            return PartialView("PartialFAQ", bv_clss.GetList(1247));
        }

        public ActionResult Map()
        {
            
            ThongTin_Class clss = new ThongTin_Class();
            return PartialView("PartialMap", clss.Get());
        }

        public ActionResult News()
        {
            BaiViet_Class bvClss = new BaiViet_Class();
            return PartialView("PartialNews", bvClss.List_index_NOIBAT_cm(1066).Take(6).ToList());
        }


        public ActionResult Partner()
        {
            Lienket_Class clss = new Lienket_Class();
            return PartialView("PartialPartner", clss.GetList());
        }

        public ActionResult Footer()
        {
            return PartialView("PartialFooter");
        }

        public ActionResult Error()
        {
            return PartialView("Error");
        }

        public User_Model SELECT_USERDETAIL(string username)
        {
            User_Model user = new User_Model();
            SqlConnection connetion = null;
            SqlDataReader reader = null;
            //data source = localhost\SQLEXPRESS2014; initial catalog = pkdkdinhtrongson; user id = pkdkdinhtrongson_admin; password = kid@1412; MultipleActiveResultSets = True; App = EntityFramework
            //connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connetion = new SqlConnection(@"data source=localhost\SQLEXPRESS2014;initial catalog=pkdkdinhtrongson_v2;user id=pkdkdinhtrongson_admin;password=kid@1412;MultipleActiveResultSets=True;App=EntityFramework");

            connetion.Open();
            SqlCommand dCmd = new SqlCommand("SELECT_USERDETAIL", connetion);
            dCmd.CommandType = CommandType.StoredProcedure;
            dCmd.Parameters.Add(new SqlParameter("@USERNAME", username));
            SqlDataAdapter da = new SqlDataAdapter(dCmd);
            DataTable table = new DataTable();
            // execute the command
            reader = dCmd.ExecuteReader();
            while (reader.Read())
            {
                //user.sEMAIL = int.Parse(reader["ID"].ToString());
                user.sEMAIL = reader["EMAIL"].ToString();
               
            }
            connetion.Close();
            return user;
        }
    }
}
