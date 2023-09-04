using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using TLBD.Areas.admin.Controllers;
using TLBD.Areas.admin.Models.Account;
using TLBD.Models.EntityModel;
using WebMatrix.WebData;

namespace TLBD.Areas.admin.Models.SignUp
{
    public class SignUp_Class
    {
        public static int SignUp(RegisterModel m)
        {
            EntitiesData ctx = new EntitiesData();
            var exist =
                ctx.webpages_UserProfile
                .ToList()
                .Exists(x => x.UserName == m.UserName);

            if (exist)
            {
                return 1;
            }

            //Them vao bang [webpages_UserProfile]
            WebSecurity.CreateUserAndAccount(m.UserName, m.Password);

            //Lay UserID
            var UserID = WebSecurity.GetUserId(m.UserName);

            //Them vao bang [USERDETAILS]
            ctx.USERDETAILS.Add(
                new USERDETAIL()
                {
                    ADDRESS = m.Address,
                    BIRTHDAY = m.NgaySinh,
                    EMAIL = m.Email,
                    ID = UserID,
                    IP_SUA = m.IP_SUA,
                    IP_TAO = m.IP_TAO,
                    NAME = m.Name,
                    NGAY_SUA = m.NGAY_SUA,
                    NGAY_TAO = m.NGAY_TAO,
                    PHONE = m.Phone,
                    SEX = m.GioiTinh,
                    USERNAME = m.UserName,
                    SYS_ACC = m.SYS_ACC
                });

            //Phan quyen menu
            ctx.MENUs
                .ToList()
                .ForEach(x => ctx.PHANQUYEN_MENU.Add(
                new PHANQUYEN_MENU()
                {
                    ID_MENU = x.ID,
                    ID_USER = UserID
                }));

            //Phan quyen chuyen muc
            ctx.CHUYEN_MUC
                .ToList()
                .ForEach(x => ctx.PHANQUYEN_CHUYENMUC.Add(
                    new PHANQUYEN_CHUYENMUC()
                    {
                        ID_CHUYENMUC = x.ID,
                        ID_USER = UserID
                    }));

            ctx.SaveChanges();

            return 0;
        }       
    }
}