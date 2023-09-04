using MVC4WEB.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using TLBD.Areas.admin.Controllers;
using TLBD.Models.EntityModel;

namespace MVC4WEB.Functions
{
    public class Toolkits
    {
        static EntitiesData ctx = new EntitiesData();

        ///<summary>
        ///Global data
        ///</summary>
        public static string SR_ADDROLE = "AddRole";
        public static string SR_ADDUIR = "AddUsersInRoles";
        public static string SR_DELUIR = "DeleteUsersInRoles";
        public static string SR_ADDRIG = "AddRolesInGroups";
        public static string SR_DELRIG = "DeleteRolesInGroups";

        public static string SR_ADDPRODUCT = "AddProduct";
        public static string SR_EDITPRODUCT = "EditProduct";
        public static string SR_DELPRODUCT = "DeleteProduct";

        public static string SR_ADDPRODUCTCATELOGY = "AddProductCatelogy";
        public static string SR_EDITPRODUCTCATELOGY = "EditProductCatelogy";
        public static string SR_DELPRODUCTCATELOGY = "DeleteProductCatelogy";

        public static string SR_DELUSER = "DeleteUser";

        public static string SR_EDITORDERSTATUS = "ChangeOrderStatus";
        public static string SR_VIEWORDER = "ViewOrder";
        public static string SR_PAYORDER = "PayOrder";


        /// <summary>
        /// Thêm quyền cho người dùng
        /// </summary>

        public static int AddOrUpdateToken(int userid, string token)
        {
            var y = (from x in ctx.TOKENs
                     where x.UserId == userid
                     select x
                    ).FirstOrDefault();

            if (y == null)
            {
                y = new TOKEN();
                y.UserId = userid;
                ctx.TOKENs.Add(y);
            }

            y.TokenValue = token;
            y.IssueOn = DateTime.Now;

            try
            {
                ctx.SaveChanges();
            }
            catch
            {
                return (1);
            }
            return (0);
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền cụ thể hay không?
        /// </summary>
        public static bool CheckSpecificRole(string username, string specificRole)
        {
            var _r =
                (from up in ctx.webpages_UserProfile
                 join ro in ctx.webpages_UsersInRoles on up.UserId equals ro.UserId
                 join rg in ctx.ROLEINGROUPs on ro.RoleId equals rg.RoleID
                 join sr in ctx.SPECIFICROLEs on rg.SRoleID equals sr.ID
                 where up.UserName == username && sr.SRoleName == specificRole
                 select up).FirstOrDefault();

            return _r != null;
        }

        public static void ExtendToken(string token)
        {
            var y = (from x in ctx.TOKENs
                     where x.TokenValue == token
                     select x
                    ).FirstOrDefault();
            if (y != null)
            {
                y.IssueOn = DateTime.Now;
                try
                {
                    ctx.SaveChanges();
                }
                catch
                {

                }
            }
        }

        public static bool isValidToken(string token)
        {
            var y = (from x in ctx.TOKENs
                     where x.TokenValue.Trim() == token
                     select x
                    ).FirstOrDefault();
            if (y != null)
            {
                int ExpiryTime = 0;
                Int32.TryParse(ConfigurationManager.AppSettings["TokenExpiry"], out ExpiryTime);
                if (y.IssueOn.AddMinutes(ExpiryTime) >= DateTime.Now)
                {
                    return (true);
                }
            }
            return (false);
        }

        //public static TokenResponeModel GetTokenModel(string token)
        //{
        //    return (from x in ctx.TOKENs
        //            join y in ctx.webpages_UserProfile on x.UserId equals y.UserId
        //            where x.TokenValue == token
        //            select new TokenResponeModel
        //            {
        //                TokenId = x.TokenId,
        //                UserId = x.UserId,
        //                Username = y.UserName,
        //                Token = x.TokenValue,
        //                IssueOn = x.IssueOn
        //            }).FirstOrDefault();
        //}

        public static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes
                = System.Convert.FromBase64String(encodedData);
            string returnValue =
               System.Text.Encoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public static void SndMAIL(string order)
        {
            var MAIL = WebConfigurationManager.AppSettings["mailAddress"];
            var PASSWORD = WebConfigurationManager.AppSettings["mailPassword"];
            var SUBJECT = WebConfigurationManager.AppSettings["mailSubject"];

            string body = @"<div>"
                            + "<p>Gửi <b>Quản trị</b>,</p>"
                            + "<p>Trang web của bạn vừa ghi nhận một đơn hàng. Vui lòng kiểm tra thông tin đơn hàng</p>"
                            + "<p>* Mã đơn hàng: <b>" + order + "</b></p>"
                            + "<p>Thân ái</p>"
                            + "</div>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //UseDefaultCredentials = false,
                Credentials = new NetworkCredential(MAIL, PASSWORD)
            };

            using (var message = new MailMessage(MAIL, MAIL)
            {
                Subject = SUBJECT,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}