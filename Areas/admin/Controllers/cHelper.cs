using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Controllers
{
    public class cHelper
    {
        public static string baseURL = "http://localhost";

        public static int ADMINISTRATOR_ID = 24;
        //public static string COOKIE = "_OS_token_cookie";
        public static string CART_COOKIE = "_OS_cart_cookie";

        public enum ORDER_STATUS
        {
            WAITING = 1,
            PROCESSING,
            DELIVERY,
            DONE,
            CANCEL
        }

        public enum CART_STATUS
        {
            CART = 1,
            WISHLIST
        }

        public static string GetCookie(HttpRequestBase Request, string name)
        {
            string value = "";

            HttpCookie mwCookie = Request.Cookies[name];
            if (mwCookie != null)
            {
                value = mwCookie.Value;
            }
            return value;
        }

        public static string ToStringCurrency(int? value)
        {
            
            return value == null ? "" : value.Value.ToString("N0", new NumberFormatInfo()
                                            {
                                                NumberGroupSizes = new[] { 3 },
                                                NumberGroupSeparator = ","
                                            });
        }

        public static void AddCookie(HttpResponseBase Response, string name, string value, int expires)
        {
            HttpCookie mwCookie = new HttpCookie(name);
            mwCookie.Value = value;
            mwCookie.Expires = DateTime.Now.AddDays(expires);
            Response.Cookies.Add(mwCookie);
        }

        public static void DeleteCookie(HttpResponseBase Response, string name)
        {
            HttpCookie mwCookie = new HttpCookie(name);
            mwCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(mwCookie);
        }

        public static void SetCookieValue(HttpResponseBase Response, string name, string value)
        {
            HttpCookie mwCookie = new HttpCookie(name);
            mwCookie.Value = value;
            Response.Cookies.Add(mwCookie);
        }

        public static string ToFriendlyUrl(string text)
        {
            if (String.IsNullOrEmpty(text)) return (" ");

            for (int i = 33; i <= 44; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 46; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            text = text.Replace(" ", "-");
            text = text.Replace("--", "-");

            var regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static int getLangID()
        {
            HttpRequest req = HttpContext.Current.Request;
            string culture = req.Cookies["language"].Value;
            if (culture == "en")
            {
                return (1);
            }
            return (0);
        }

        public static string StrReplace(string s)
        {
            var s1 = s.Replace("@", "%40");
            return (s1.Replace(".", "%5B"));
        }

        ////

        public static string ChuyenTVKhongDau(string strVietNamese)
        {
            string FindText = "ÀàảẢãáÁạẠăằẳẵắặâÂầẩẫấẤậđĐèẻẽéẹêÊềểễếệìỉĩíịòỏõóọôÔồổỗốộơờởỡớợùủũúụưƯừửữứựỳỷỹýỵ";
            string ReplText = "AaaAaaAaAaaaaaaaAaaaaAadDeeeeeeEeeeeeiiiiiooooooOooooooooooouuuuuuUuuuuuyyyyy";
            int index = -1, index2;
            while ((index = strVietNamese.IndexOfAny(FindText.ToCharArray())) != -1)
            {
                index2 = FindText.IndexOf(strVietNamese[index]);
                strVietNamese = strVietNamese.Replace(strVietNamese[index], ReplText[index2]);
            }
            return strVietNamese;
        }

        public static int PASSWORD_MINIUM_LEN = 6;
    }
}