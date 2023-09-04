using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using TLBD.Areas.admin.Controllers;
using TLBD.Areas.admin.Models.PhanQuyen_NhomBaiViet;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Models.PhanQuyen_Menu
{
    public class PhanQuyen_Menu_Class
    {
        EntitiesData ctx = new EntitiesData();
        public static int Count_Menu_PhanQuyen(int id)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.PHANQUYEN_MENU
                .Count(x => x.ID_USER == id);
        }

        public static List<PhanQuyen_Menu_Model> List_Menu_PhanQuyen(int id)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.PHANQUYEN_MENU
                 .Where(x => x.ID_USER == id)
                 .Join(ctx.MENUs.Where(x => x.MENU_CHA == 0).OrderBy(x => x.THU_TU),
                 x => x.ID_MENU,
                 y => y.ID,
                 (x, y) => new PhanQuyen_Menu_Model
                 {
                     sID_USER = x.ID_USER,
                     sID_MENU = x.ID_MENU,
                     sTRANG_THAI = x.TRANG_THAI,
                     sTEN_MENU1 = y.TEN_MENU,
                     sCONTROLLER = y.CONTROLLER
                 }).ToList();
        }

        public static List<PhanQuyen_Menu_Model> List_Menu_PhanQuyen_menucon(int id, int id_menu_cha)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.PHANQUYEN_MENU
                .Where(x => x.ID_USER == id)
                .Join(ctx.MENUs.Where(x => x.MENU_CHA == id_menu_cha).OrderBy(x => x.THU_TU),
                x => x.ID_MENU,
                y => y.ID,
                (x, y) => new PhanQuyen_Menu_Model
                {
                    sID_USER = x.ID_USER,
                    sID_MENU = x.ID_MENU,
                    sTRANG_THAI = x.TRANG_THAI,
                    sTEN_MENU1 = y.TEN_MENU,
                    sCONTROLLER = y.CONTROLLER
                }).ToList();
        }

        public static List<PhanQuyen_Menu_Model> Load_Menu_PhanQuyen(int id)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.PHANQUYEN_MENU
                .Where(x => x.ID_USER == id)
                .Join(ctx.MENUs.Where(x => x.MENU_CHA == 0).OrderBy(x => x.THU_TU),
                x => x.ID_MENU,
                y => y.ID,
                (x, y) => new PhanQuyen_Menu_Model
                {
                    sID_USER = x.ID_USER,
                    sID_MENU = x.ID_MENU,
                    sTRANG_THAI = x.TRANG_THAI,
                    sTEN_MENU1 = y.TEN_MENU,
                    sCONTROLLER = y.CONTROLLER
                }).ToList();
        }

        public static List<PhanQuyen_Menu_Model> List_Menu_PhanQuyen_Con(int id_menu_cha, int id)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.MENUs
                .Where(x => x.MENU_CHA == id_menu_cha)
                .OrderBy(x => x.THU_TU)
                .Join(ctx.PHANQUYEN_MENU
                .Where(x => x.TRANG_THAI == 1 && x.ID_USER == id),
                x => x.ID,
                y => y.ID_MENU,
                (x, y) => new PhanQuyen_Menu_Model
                {
                    sTEN_MENU1 = x.TEN_MENU,
                    sCONTROLLER = x.CONTROLLER,
                    sVIEW = x.VIEW
                }).ToList();
        }

        public int Capnhat(PhanQuyen_Menu_Model model)
        {
            var data = ctx.PHANQUYEN_MENU.Where(x => x.ID_USER == model.sID_USER && x.ID_MENU == model.sID_MENU).FirstOrDefault();

            if (data != null)
            {
                data.TRANG_THAI = model.sTRANG_THAI;
            }

            try
            {
                ctx.SaveChanges();
                return data.ID_USER;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                return -1;
            }
        }

        public int Update(PhanQuyen_Menu_Model model)
        {

            ctx.PHANQUYEN_MENU
                .Where(x => x.ID_USER == model.sID_USER)
                .ToList()
                .ForEach(x =>
                {
                    x.TRANG_THAI = 0;
                });

            try
            {
                ctx.SaveChanges();
                return model.sID_USER;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                return -1;
            }
        }
    }
}