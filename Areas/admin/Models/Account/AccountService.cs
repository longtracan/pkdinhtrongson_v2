using MVC4WEB.Functions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TLBD.Areas.admin.Controllers;
using TLBD.Areas.admin.Models.PhanQuyen_Menu;
using TLBD.Areas.admin.Models.PhanQuyen_NhomBaiViet;
using TLBD.Models.EntityModel;
using WebMatrix.WebData;

namespace TLBD.Areas.admin.Models.Account
{

    public class AccountService
    {
        public static EntitiesData ctx = new EntitiesData();
        public static User_Model Get(string sUSERNAME)
        {
            return ctx.USERDETAILS
                .Where(x => x.USERNAME == sUSERNAME)
                .Select(y => new User_Model
                {
                    sID = y.ID,
                    sUSERNAME = y.USERNAME,
                    sTEN_USER = y.NAME,
                    sDIA_CHI = y.ADDRESS,
                    sEMAIL = y.EMAIL,
                    sSO_DIEN_THOAI = y.PHONE,
                    sSYSTEM_ACCOUNT = y.SYS_ACC
                })
               .FirstOrDefault();
        }

        public static RegisterModel GetUserInfomation(string username)
        {
            return ctx.USERDETAILS
                .Where(x => x.USERNAME == username)
                .Select(x => new RegisterModel
                {
                    Address = x.ADDRESS,
                    Email = x.EMAIL,
                    GioiTinh = x.SEX,
                    Name = x.NAME,
                    NgaySinh = x.BIRTHDAY,
                    Phone = x.PHONE,
                    UserName = x.USERNAME
                })
               .FirstOrDefault();
        }

        public static List<User_Model> List_NguoiDung()
        {
            return ctx.USERDETAILS
                .Where(x => x.SYS_ACC == true && x.TRANG_THAI != 99 && x.ID != cHelper.ADMINISTRATOR_ID)//24 is ADMINISTRATOR ID
                .OrderByDescending(x => x.USERNAME)
                .Select(x => new User_Model
                {
                    sID = x.ID,
                    sUSERNAME = x.USERNAME,
                    sTEN_USER = x.NAME
                }).ToList();
        }

        public static string IsValid(string sUSERNAME, string sPASSWORD)
        {
            var login = WebSecurity.Login(sUSERNAME, sPASSWORD);

            if (login)
            {
                int _userid = WebSecurity.GetUserId(sUSERNAME);
                string _roles = String.Join(",", Roles.GetRolesForUser(sUSERNAME));

                var _r = (from ud in ctx.USERDETAILS
                          where ud.ID == _userid
                          select new
                          {
                              UserId = _userid,
                              FullName = ud.NAME,
                              Token = Guid.NewGuid().ToString()
                          }).FirstOrDefault();

                Toolkits.AddOrUpdateToken(_r.UserId, _r.Token);

                return _r.Token;
            }
            else
            {
                return "";
            }
        }

        public static int SavePassword(string sUSERNAME, string OLD_PASSWORD, string NEW_PASSWORD)
        {
            if (!WebSecurity.Login(sUSERNAME, OLD_PASSWORD, true))
            {
                return 1;//Mat khau khong dung
            }

            if (!WebSecurity.ChangePassword(sUSERNAME, OLD_PASSWORD, NEW_PASSWORD))
            {
                return 2;//Khong doi duoc
            }

            return 0;
        }

        public int GetCount()
        {
            return ctx.USERDETAILS
                .Count(x => x.TRANG_THAI != 99 && x.ID != cHelper.ADMINISTRATOR_ID);
        }

        public IList<User_Model> GetList(int page, int pageSize)
        {
            return ctx.USERDETAILS
                .Where(x => x.TRANG_THAI != 99 && x.ID != cHelper.ADMINISTRATOR_ID)//24 is ADMINISTRATOR ID
                .OrderBy(x => x.ID)
                .Select(x => new User_Model
                {
                    sID = x.ID,
                    sUSERNAME = x.USERNAME,
                    sTEN_USER = x.NAME,
                    sIMG_URL = x.IMG_URL,
                    sNGAY_TAO = x.NGAY_TAO,
                    sTRANG_THAI = x.TRANG_THAI
                })
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public User_Model Get(int id)
        {
            return ctx.USERDETAILS
                .Where(x => x.ID == id)
                .Join(ctx.webpages_Membership,
                x => x.ID,
                y => y.UserId,
                (x, y) => new User_Model
                {
                    sID = x.ID,
                    sUSERNAME = x.USERNAME,
                    sPASSWORD = y.Password,
                    sTEN_USER = x.NAME,
                    sIMG_URL = x.IMG_URL,
                    sNGAY_TAO = x.NGAY_TAO,
                    sTRANG_THAI = x.TRANG_THAI,

                    sNGAY_SINH = x.BIRTHDAY,
                    sSO_DIEN_THOAI = x.PHONE,
                    sEMAIL = x.EMAIL,
                    sGIOI_TINH = x.SEX,
                    sDIA_CHI = x.ADDRESS,
                    sSYSTEM_ACCOUNT = x.SYS_ACC

                })
                .FirstOrDefault();
        }

        //public void Approve(IList<int> List)
        //{
        //    //foreach (var id in List)
        //    //{
        //    //    var m = (from x in ctx.USERs
        //    //             where x.ID == id
        //    //             select x).FirstOrDefault();
        //    //}

        //    //ctx.SubmitChanges();
        //}

        public int Capnhat(User_Model model)
        {
            var data = ctx.USERDETAILS.Where(x => x.ID == model.sID).FirstOrDefault();

            if (data != null)
            {
                data.NAME = model.sTEN_USER;
                data.IMG_URL = model.sIMG_URL;
                data.SEX = model.sGIOI_TINH;
                data.BIRTHDAY = model.sNGAY_SINH;
                data.PHONE = model.sSO_DIEN_THOAI;
                data.EMAIL = model.sEMAIL;
                data.ADDRESS = model.sDIA_CHI;
                data.NGAY_SUA = model.sNGAY_SUA;
                data.IP_SUA = model.sIP_SUA;
                data.SYS_ACC = model.sSYSTEM_ACCOUNT;
            }
            else
            {
                WebSecurity.CreateUserAndAccount(model.sUSERNAME, model.sPASSWORD);
                int ID = WebSecurity.GetUserId(model.sUSERNAME);

                data = new USERDETAIL()
                {
                    ID = ID,
                    IP_TAO = model.sIP_TAO,
                    NGAY_TAO = model.sNGAY_TAO,
                    TRANG_THAI = model.sTRANG_THAI,
                    USERNAME = model.sUSERNAME,
                    NAME = model.sTEN_USER,
                    IMG_URL = model.sIMG_URL,
                    SEX = model.sGIOI_TINH,
                    BIRTHDAY = model.sNGAY_SINH,
                    PHONE = model.sSO_DIEN_THOAI,
                    EMAIL = model.sEMAIL,
                    ADDRESS = model.sDIA_CHI,
                    NGAY_SUA = model.sNGAY_SUA,
                    IP_SUA = model.sIP_SUA,
                    SYS_ACC = model.sSYSTEM_ACCOUNT
                };

                ctx.USERDETAILS.Add(data);

                ctx.MENUs
                    .ToList()
                    .ForEach(x => ctx.PHANQUYEN_MENU.Add(
                        new PHANQUYEN_MENU()
                        {
                            ID_MENU = x.ID,
                            ID_USER = ID,
                            TRANG_THAI = 0
                        }));

                ctx.CHUYEN_MUC
                    .ToList()
                    .ForEach(x => ctx.PHANQUYEN_CHUYENMUC.Add(
                        new PHANQUYEN_CHUYENMUC()
                        {
                            ID_CHUYENMUC = x.ID,
                            ID_USER = ID,
                            TRANG_THAI = 0
                        }));
            }

            try
            {
                ctx.SaveChanges();
                return data.ID;
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

        public void Delete(User_Model model)
        {
            var exist = ctx.USERDETAILS.Count(x => x.ID == model.sID);

            if (exist > 0)
            {
                ctx.USERDETAILS
                    .Where(x => x.ID == model.sID)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.IP_XOA = model.sIP_XOA;
                        x.NGAY_XOA = model.sNGAY_XOA;
                        x.TRANG_THAI = 99;
                    });

                try
                {
                    ctx.SaveChanges();
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
                }
            }
        }

        public static void CapNhat(RegisterModel model, string username)
        {
            ctx.USERDETAILS
                .Where(x => x.USERNAME == username)
                .ToList()
                .ForEach(x =>
                {
                    x.NAME = model.Name;
                    x.SEX = model.GioiTinh;
                    x.BIRTHDAY = model.NgaySinh;
                    x.PHONE = model.Phone;
                    x.EMAIL = model.Email;
                    x.ADDRESS = model.Address;
                    x.NGAY_SUA = model.NGAY_SUA;
                    x.IP_SUA = model.IP_SUA;
                });

            try
            {
                ctx.SaveChanges();
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
            }
        }
    }
}