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
using SautinSoft.Document;
using SautinSoft.Document.Tables;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Reflection;
using AttributeRouting.Helpers;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TLBD.Controllers
{
    public class NhomBaiVietController : Controller
    {
        NhomBaiViet_Class nbv = new NhomBaiViet_Class();
        BaiViet_Class bv = new BaiViet_Class();
        ChuyenMuc_Class cm = new ChuyenMuc_Class();

        [GET("Chuyen-Muc/{TenCM}/{TenNBV}/{id}")] /*Danh sách bài viết theo nhóm bài viết*/
        public ActionResult Index(int id, int? trang)
        {

            int kichthuoc = 12;
            int soluong = bv.GetCount(id);
            int tongtrang = (soluong - 1) / kichthuoc + 1;
            List_BaiViet_View model = new List_BaiViet_View();
            model.Nhom_BaiViet = nbv.Get(id);
            model.ChuyenMuc = cm.Get(model.Nhom_BaiViet.ID_CHUYENMUC);

            ViewData["kiemtra"] = bv.GetCount(id);

            model.BaiViet = bv.Get_BaiViet(id);
            model.List_BaiViet = bv.List_index(trang ?? 1, kichthuoc, id);

            ViewData["trang"] = trang ?? 1;
            ViewData["tongtrang"] = tongtrang;

            return View("Index", model);



        }

        [GET("Dich-Vu-Noi-Bat/{TenNBV}/{id}")]
        public ActionResult DichVuNoiBat(int id, int? trang)
        {

            int kichthuoc = 6;
            int soluong = bv.GetCount(id);
            int tongtrang = (soluong - 1) / kichthuoc + 1;
            List_BaiViet_View model = new List_BaiViet_View();
            model.Nhom_BaiViet = nbv.Get(id);
            model.ChuyenMuc = cm.Get(model.Nhom_BaiViet.ID_CHUYENMUC);

            ViewData["kiemtra"] = bv.GetCount(id);

            model.BaiViet = bv.Get_BaiViet(id);
            model.List_BaiViet = bv.List_index(trang ?? 1, kichthuoc, id);

            ViewData["trang"] = trang ?? 1;
            ViewData["tongtrang"] = tongtrang;

            return View("DichVuNoiBat", model);

        }

        [GET("Doi-Ngu-Bac-Si/{id}")]
        public ActionResult DoiNguBacSi(int id, int? trang)
        {
            int kichthuoc = 6;
            int soluong = bv.GetCount(id);
            int tongtrang = (soluong - 1) / kichthuoc + 1;
            List_BaiViet_View model = new List_BaiViet_View();
            model.Nhom_BaiViet = nbv.Get(id);
            model.ChuyenMuc = cm.Get(model.Nhom_BaiViet.ID_CHUYENMUC);

            ViewData["kiemtra"] = bv.GetCount(id);

            model.BaiViet = bv.Get_BaiViet(id);
            model.List_BaiViet = bv.List_index_tt(trang ?? 1, kichthuoc, id);

            ViewData["trang"] = trang ?? 1;
            ViewData["tongtrang"] = tongtrang;
            return View("DoiNguBacSi", model);
        }

        [GET("Danh-Sach-Bai-Viet/{id}")] /*Danh sách bài viết theo chuyên mục*/
        public ActionResult DanhSachBaiVietTheoChuyenMuc(int id, int? trang)
        {
            int kichthuoc = 12;
            int soluong = bv.GetCount1(id);
            int tongtrang = (soluong - 1) / kichthuoc + 1;
            List_BaiViet_View model = new List_BaiViet_View();
            model.Nhom_BaiViet = nbv.Get1(id); //get nhombaiviet theo chuyen muc
            model.ChuyenMuc = cm.Get(id);

            ViewData["kiemtra"] = bv.GetCount(id);

            //model.BaiViet = bv.Get_BaiViet(id);
            model.List_BaiViet = bv.List_index_cm(trang ?? 1, kichthuoc, id);

            ViewData["trang"] = trang ?? 1;
            ViewData["tongtrang"] = tongtrang;
            return View("DanhSachBaiVietTheoChuyenMuc", model);
        }

        [GET("Dang-Ky-Kham-Tong-Quat")] /* theo chuyên mục*/
        public ActionResult DangKyKhamTongQuat(int? trang)
        {
            int id = 2071;
            int kichthuoc = 1000;
            int soluong = bv.GetCount1(id);
            int tongtrang = (soluong - 1) / kichthuoc + 1;
            List_BaiViet_View model = new List_BaiViet_View();
            model.Nhom_BaiViet = nbv.Get1(id); //get nhombaiviet theo chuyen muc
            model.ChuyenMuc = cm.Get(id);

            ViewData["kiemtra"] = bv.GetCount(id);

            //model.BaiViet = bv.Get_BaiViet(id);
            //model.List_BaiViet = bv.List_index_cm(trang ?? 1, kichthuoc, id);

            ViewData["trang"] = trang ?? 1;
            ViewData["tongtrang"] = tongtrang;
            return View("DangKyKhamTongQuat", model);
        }

        [GET("Hinh-Anh/{id}")]
        public ActionResult HinhAnh(int id, int? trang) /*Danh sách bài viết theo nhóm bài viết*/
        {
            int kichthuoc = 6;
            int soluong = bv.GetCount(id);
            int tongtrang = (soluong - 1) / kichthuoc + 1;
            List_BaiViet_View model = new List_BaiViet_View();
            model.Nhom_BaiViet = nbv.Get(id);
            model.ChuyenMuc = cm.Get(model.Nhom_BaiViet.ID_CHUYENMUC);

            ViewData["kiemtra"] = bv.GetCount(id);

            model.BaiViet = bv.Get_BaiViet(id);
            model.List_BaiViet = bv.List_index_tt(trang ?? 1, kichthuoc, id);

            ViewData["trang"] = trang ?? 1;
            ViewData["tongtrang"] = tongtrang;

            return View("HinhAnh", model);
        }



        [GET("SendZalo")]
        public ActionResult SendZalo() /*Danh sách bài viết theo nhóm bài viết*/
        {

            return View("SendZalo");
        }


        //Hàm xử lý lấy token và gửi qua bot-telegram
       
        private static readonly string TELEGRAM_BOT_TOKEN = "8548564644:AAGbFxMcqgIF6JL_KM4yz7b1vKCtZQEMntk";
        private static readonly string ZALO_SECRET_KEY = "iXIljjdAfnXaP8oBuPMY";
        private static readonly string ZALO_APP_ID = "1915518086623078051";

        [HttpPost]
        [POST("TelegramGetToken")]
        public async Task<ActionResult> TelegramGetToken()
        {
            try
            {
                // Nhận JSON từ telegram bot gửi lên
                string body = new StreamReader(Request.InputStream).ReadToEnd();
                dynamic data = JObject.Parse(body);

                string refreshToken = data.refresh_token;
                string chatId = "781284765";

                // GỌI ZALO API ĐỂ LẤY ACCESS TOKEN
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("secret_key", ZALO_SECRET_KEY);

                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "refresh_token", refreshToken },
                    { "app_id", ZALO_APP_ID },
                    { "grant_type", "refresh_token" }
                });

                var zaloResponse = await httpClient.PostAsync("https://oauth.zaloapp.com/v4/oa/access_token", content);
                string zaloRaw = await zaloResponse.Content.ReadAsStringAsync();

                // Parse JSON Zalo trả về
                dynamic zaloJson = JObject.Parse(zaloRaw);
                string newAccessToken = zaloJson.access_token;
                string newRefreshToken = zaloJson.refresh_token;
                // GỬI TRẢ NGƯỢC VỀ TELEGRAM
                var botClient = new TelegramBotClient(TELEGRAM_BOT_TOKEN);

                string message = "Lấy token thành công:\n\n" +
                                 $"🔑 Access Token:\n{newAccessToken}\n\n" +
                                 $"♻ Refresh Token:\n{newRefreshToken}";

                await botClient.SendTextMessageAsync(
                    chatId: long.Parse(chatId),
                    text: message
                );

                return Json(new { status = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "ERROR", message = ex.Message });
            }
        }

        public string SendAPIZalo(string phone, string number, string schedule_time, string phone_number, string customer_name, string product_name, string customer_code, string token, string note, string staff_name)
        {
            var username = User.Identity.Name;
            //var obj = new
            //{
            //    SoBienNhan = "000.00.BD.H08-230411-0004",
            //    GhiChu = "World",
            //    DanhSachGiayToDinhKem = ""
            //};
            
            var obj = new
            {
                phone = phone,
                template_id = "421018",
                template_data = new
                {
                    number = number,
                    schedule_time = schedule_time,
                    phone_number = phone_number,
                    customer_name = customer_name,
                    product_name = product_name,
                    customer_code = customer_code,
                    note = note
                },
                tracking_id = customer_code
            };
            const string URL = "https://business.openapi.zalo.me/message/template";
            string urlParameters = "";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("access_token", token);
            HttpResponseMessage response = client.PostAsJsonAsync(URL, obj).Result;
            var responseContent = "FAIL";
            string responseContent_message = "ZALO KHÔNG PHẢN HỒI";
            if (response.IsSuccessStatusCode)
            {
                responseContent = response.Content.ReadAsStringAsync().Result;
                var json_serializer = new JavaScriptSerializer();
                dynamic item = json_serializer.Deserialize<object>(responseContent);
                responseContent_message = item["message"];
            }
            GhiLogSendZalo(phone, number, schedule_time, phone_number, customer_name, product_name, customer_code, responseContent_message, staff_name, username, 421018);
            client.Dispose();
            return responseContent;
        }

        public string SendAPIZalo_danhgiadichvu(string phone, string schedule_date, string customer_name, string customer_code, string token)
        {
            var username = User.Identity.Name;
            //var obj = new
            //{
            //    SoBienNhan = "000.00.BD.H08-230411-0004",
            //    GhiChu = "World",
            //    DanhSachGiayToDinhKem = ""
            //};
            var obj = new
            {
                phone = phone,
                template_id = "281848",
                template_data = new
                {
                    schedule_date = schedule_date,
                    customer_name = customer_name,
                    customer_code = customer_code
                },
                tracking_id = customer_code
            };
            //var token = "6Tz9GdLuHGCGsaH5UtfE1H7GHq1zR1rPJyzcNdqRAnzrmsW-GIitD5RoDmjQDsSXKUa1ErytGYmmx2arCYff3Wl-DomyMsCs1OeMBI1JHcWYiXnSEs52Ro-UAoS5LKer6Qe603vCLY0ojZa26trp00FMA1KE7dCVFzKUF2D-VouyeYir4rLW8osJ33yRO5WLS88sEc4ZVWz-t7v6Io0g05VeQXn6DmazRDTzE4KjDImryKOD7ZONIZ3s5sWr35fLMj8dNMvpSHX7k0mySrWO85-U61zgPc0SSO8KN4TyHXXPsHH5VIn_KnBn314W1arH2_SNIJWoUsWpb0TLFsX94Lgu0IHfK7G10uTx0pzsAHqlhbCi6LCR5c5TaZbmS6D12m";
            const string URL = "https://business.openapi.zalo.me/message/template";
            string urlParameters = "";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("access_token", token);

            // List data response.
            //HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            HttpResponseMessage response = client.PostAsJsonAsync(URL, obj).Result;
            var responseContent = "LỖI - ZALO KHÔNG PHẢN HỒI";
            if (response.IsSuccessStatusCode)
            {
                responseContent = response.Content.ReadAsStringAsync().Result;
                
                //var data = data_json.data;
                // Parse the response body.

            }
            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            //GhiLogSendZalo(phone, number, schedule_time, phone_number, customer_name, product_name, customer_code, responseContent, "", username, 255076);
            client.Dispose();
            return responseContent;
        }
        public string lay_ds_phanhoi(string from_time, string to_time, string token)
        {
            //convert time to timestamp 
            string year_from_time = from_time.Substring(6, 4);
            string month_from_time = from_time.Substring(3, 2);
            string day_from_time = from_time.Substring(0, 2);

            string year_to_time = to_time.Substring(6, 4);
            string month_to_time = to_time.Substring(3, 2);
            string day_to_time = to_time.Substring(0, 2);


            var date_from_time = new DateTime(Int32.Parse(year_from_time), Int32.Parse(month_from_time), Int32.Parse(day_from_time), 00, 0, 0, DateTimeKind.Utc);
            var dateWithOffset = new DateTimeOffset(date_from_time).ToUniversalTime();
            long from_time_timestamp = dateWithOffset.ToUnixTimeMilliseconds();

            var date_to_time = new DateTime(Int32.Parse(year_to_time), Int32.Parse(month_to_time), Int32.Parse(day_to_time), 00, 0, 0, DateTimeKind.Utc);
            var dateWithOffset2 = new DateTimeOffset(date_to_time).ToUniversalTime();
            long to_time_timestamp = dateWithOffset2.ToUnixTimeMilliseconds();
            //token = "nzkIVANpyqhEtfzxg_7uJC_Kd7wjv_aBYuwTIwd5dptEdPjKxCh-ClUZ-LlDqPnj_BsQ3kUJdtcCxun8gRQJ5vJ_WdsbXeSRXF7WUB7UXKYOak8xcCVrNepewm2gc8T0oTA02zA1u5lmtlOLpQdp9klVaKw8zFPyaw7pJOk8dtcCre4ogRc6UvVSvG-Cyk91igZc9ABM-4wz-Ea3ZwxjKu_AZ1-CbPfvjjoDJwQ3Y3k0x8voXyENHupeYJ2Bjfe0hfdtDytFubFpa-CUy__vAFAtwa3BtDWZsvkqKx2rqZMgnyXTcvJwDQgnd6sEnRmbyAYUSFdseH_TlvvepDMmESYvXs6ezfWHWwQWU9h9cGUicgPwlQgc4OQQa2IAq-bKgFZ94QwSf4Ma-wWRfj79QQNqhpVFXgbTKnrK1w3ezq8";
            const string URL = "https://business.openapi.zalo.me/rating/get?template_id=281848";
            string urlParameters = "https://business.openapi.zalo.me/rating/get?template_id=281848&from_time=" + from_time_timestamp + "&to_time=" + to_time_timestamp + "&offset=0&limit=1000";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("access_token", token);

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            //HttpResponseMessage response = client.PostAsJsonAsync(URL, obj).Result;
            var responseContent_data = "LỖI - ZALO KHÔNG PHẢN HỒI";
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                dynamic jsonObj = JObject.Parse(responseContent)["data"]["data"];
                responseContent_data = JsonConvert.SerializeObject(jsonObj);

            }
            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            //GhiLogSendZalo(phone, number, schedule_time, phone_number, customer_name, product_name, customer_code, responseContent, "", username, 421018);
            //client.Dispose();
            return responseContent_data;
        }


        public void GhiLogSendZalo(string phone, string number, string schedule_time, string phone_number, string customer_name, string product_name, string customer_code, string result, string ghichu, string taikhoan, int templat_id)
        {
            SqlConnection connetion = null;
            SqlDataReader rdr = null;
            //data source = localhost\SQLEXPRESS2014; initial catalog = pkdkdinhtrongson; user id = pkdkdinhtrongson_admin; password = kid@1412; MultipleActiveResultSets = True; App = EntityFramework
            //connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson_v2;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connetion = new SqlConnection(@"data source=localhost\SQLEXPRESS2014;initial catalog=pkdkdinhtrongson_v2;user id=pkdkdinhtrongson_admin;password=kid@1412;MultipleActiveResultSets=True;App=EntityFramework");

            connetion.Open();
            SqlCommand dCmd = new SqlCommand("INSERT_LOG_ZALO", connetion);
            dCmd.CommandType = CommandType.StoredProcedure;
            dCmd.Parameters.Add(new SqlParameter("@CUSTOMER_NAME", customer_name));
            dCmd.Parameters.Add(new SqlParameter("@NUMBER", number));
            dCmd.Parameters.Add(new SqlParameter("@SCHEDULE_TIME", schedule_time));
            dCmd.Parameters.Add(new SqlParameter("@PHONE", phone_number));
            dCmd.Parameters.Add(new SqlParameter("@CUSTOMER_CODE", customer_code));
            dCmd.Parameters.Add(new SqlParameter("@PRODUCT_NAME", product_name));
            dCmd.Parameters.Add(new SqlParameter("@PHONE_SEND_ZALO", phone));
            dCmd.Parameters.Add(new SqlParameter("@RESULT", result));
            dCmd.Parameters.Add(new SqlParameter("@TAI_KHOAN", taikhoan));
            dCmd.Parameters.Add(new SqlParameter("@ID_TEMPLATE", templat_id));
            dCmd.Parameters.Add(new SqlParameter("@GHI_CHU", ghichu));

            SqlDataAdapter da = new SqlDataAdapter(dCmd);
            DataTable table = new DataTable();
            // execute the command
            rdr = dCmd.ExecuteReader();
            connetion.Close();
        }

        public JsonResult SELECT_LOG_ZALO(string TUNGAY)
        {
            //TUNGAY = "08/10/2023";
            DataTable dt = new DataTable();
            SqlConnection connetion = null;
            SqlDataReader rdr = null;
            //data source = localhost\SQLEXPRESS2014; initial catalog = pkdkdinhtrongson; user id = pkdkdinhtrongson_admin; password = kid@1412; MultipleActiveResultSets = True; App = EntityFramework
            //connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson_v2;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connetion = new SqlConnection(@"data source=localhost\SQLEXPRESS2014;initial catalog=pkdkdinhtrongson_v2;user id=pkdkdinhtrongson_admin;password=kid@1412;MultipleActiveResultSets=True;App=EntityFramework");

            connetion.Open();
            SqlCommand dCmd = new SqlCommand("SELECT_LOG_ZALO", connetion);
            dCmd.CommandType = CommandType.StoredProcedure;
            dCmd.Parameters.Add(new SqlParameter("@TUNGAY", TUNGAY));
            SqlDataAdapter da = new SqlDataAdapter(dCmd);
            da.Fill(dt);
            // execute the command
            rdr = dCmd.ExecuteReader();
            DataTable dtData = new DataTable("Data");
            DataTable dtSchema = new DataTable("Schema");
          
            
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            var String_rows = serializer.Serialize(rows);
            
            connetion.Close();
            return Json(String_rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SELECT_LOG_ZALO_TUNGAY_DENNGAY(string TUNGAY, string DENNGAY)
        {
            //TUNGAY = "08/10/2023";
            DataTable dt = new DataTable();
            SqlConnection connetion = null;
            SqlDataReader rdr = null;
            //data source = localhost\SQLEXPRESS2014; initial catalog = pkdkdinhtrongson; user id = pkdkdinhtrongson_admin; password = kid@1412; MultipleActiveResultSets = True; App = EntityFramework
            //connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson_v2;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connetion = new SqlConnection(@"data source=localhost\SQLEXPRESS2014;initial catalog=pkdkdinhtrongson_v2;user id=pkdkdinhtrongson_admin;password=kid@1412;MultipleActiveResultSets=True;App=EntityFramework");

            connetion.Open();
            SqlCommand dCmd = new SqlCommand("SELECT_LOG_ZALO_TUNGAY_DENNGAY", connetion);
            dCmd.CommandType = CommandType.StoredProcedure;
            dCmd.Parameters.Add(new SqlParameter("@TUNGAY", TUNGAY));
            dCmd.Parameters.Add(new SqlParameter("@DENNGAY", DENNGAY));
            SqlDataAdapter da = new SqlDataAdapter(dCmd);
            da.Fill(dt);
            // execute the command
            rdr = dCmd.ExecuteReader();
            DataTable dtData = new DataTable("Data");
            DataTable dtSchema = new DataTable("Schema");


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            var String_rows = serializer.Serialize(rows);

            connetion.Close();
            return Json(String_rows, JsonRequestBehavior.AllowGet);
        }

        [GET("SendVoucher")]
        public ActionResult SendVoucher() /*Danh sách bài viết theo nhóm bài viết*/
        {

            return View("SendVoucher");
        }

        [GET("tra-cuu-Voucher")]
        public ActionResult GetVoucher() /*Danh sách bài viết theo nhóm bài viết*/
        {

            return View("GetVoucher");
        }

        public void GhiLogSendAPIVoucher(string voucher_code, string start_date, string end_date, string value, string customer_name, string customer_code, string customer_year, string phone_number, string event_name, string event_condition, string event_date, string phone_sendzalo, string staff_name, string responseContent_message, string ID_template)
        {
            SqlConnection connetion = null;
            SqlDataReader rdr = null;
            //data source = localhost\SQLEXPRESS2014; initial catalog = pkdkdinhtrongson; user id = pkdkdinhtrongson_admin; password = kid@1412; MultipleActiveResultSets = True; App = EntityFramework
            //connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson_v2;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connetion = new SqlConnection(@"data source=localhost\SQLEXPRESS2014;initial catalog=pkdkdinhtrongson_v2;user id=pkdkdinhtrongson_admin;password=kid@1412;MultipleActiveResultSets=True;App=EntityFramework");

            connetion.Open();
            SqlCommand dCmd = new SqlCommand("INSERT_LOG_VOUCHER", connetion);
            dCmd.CommandType = CommandType.StoredProcedure;
            dCmd.Parameters.Add(new SqlParameter("@VOUCHER_CODE", voucher_code));
            dCmd.Parameters.Add(new SqlParameter("@FROM_DATE", start_date));
            dCmd.Parameters.Add(new SqlParameter("@TO_DATE", end_date));
            dCmd.Parameters.Add(new SqlParameter("@VOUCHER_VALUE", value));
            dCmd.Parameters.Add(new SqlParameter("@CUSTOMER_NAME", customer_name));
            dCmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", customer_code));
            dCmd.Parameters.Add(new SqlParameter("@CUSTOMER_YEAR", customer_year));
            dCmd.Parameters.Add(new SqlParameter("@CUSTOMER_PHONE", phone_number));
            dCmd.Parameters.Add(new SqlParameter("@EVENT_NAME", event_name));
            dCmd.Parameters.Add(new SqlParameter("@EVENT_CONDITION", event_condition));
            dCmd.Parameters.Add(new SqlParameter("@EVENT_DATE", event_date));
            dCmd.Parameters.Add(new SqlParameter("@PHONE_SEND_ZALO", phone_sendzalo));
            dCmd.Parameters.Add(new SqlParameter("@NGAY_GUI", "")); // trong db lay ngay hien tai
            dCmd.Parameters.Add(new SqlParameter("@NGUOI_THUC_HIEN", staff_name));
            dCmd.Parameters.Add(new SqlParameter("@RESULT", responseContent_message));
            dCmd.Parameters.Add(new SqlParameter("@ID_TEMPLATE", ID_template));



            SqlDataAdapter da = new SqlDataAdapter(dCmd);
            DataTable table = new DataTable();
            // execute the command
            rdr = dCmd.ExecuteReader();
            connetion.Close();
        }

        public void GhiLogVoucher(string voucher_code, string start_date, string end_date, string value, string customer_name, string customer_code, string customer_year, string phone_number, string event_name, string event_condition, string event_date, string phone_sendzalo, string staff_name, string ID_template)
        {
            SqlConnection connetion = null;
            SqlDataReader rdr = null;
            //data source = localhost\SQLEXPRESS2014; initial catalog = pkdkdinhtrongson; user id = pkdkdinhtrongson_admin; password = kid@1412; MultipleActiveResultSets = True; App = EntityFramework
            //connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson_v2;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connetion = new SqlConnection(@"data source=localhost\SQLEXPRESS2014;initial catalog=pkdkdinhtrongson_v2;user id=pkdkdinhtrongson_admin;password=kid@1412;MultipleActiveResultSets=True;App=EntityFramework");

            connetion.Open();
            SqlCommand dCmd = new SqlCommand("INSERT_VOUCHER", connetion);
            dCmd.CommandType = CommandType.StoredProcedure;
            dCmd.Parameters.Add(new SqlParameter("@VOUCHER_CODE", voucher_code));
            dCmd.Parameters.Add(new SqlParameter("@FROM_DATE", start_date));
            dCmd.Parameters.Add(new SqlParameter("@TO_DATE", end_date));
            dCmd.Parameters.Add(new SqlParameter("@VOUCHER_VALUE", value));
            dCmd.Parameters.Add(new SqlParameter("@CUSTOMER_NAME", customer_name));
            dCmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", customer_code));
            dCmd.Parameters.Add(new SqlParameter("@CUSTOMER_YEAR", customer_year));
            dCmd.Parameters.Add(new SqlParameter("@CUSTOMER_PHONE", phone_number));
            dCmd.Parameters.Add(new SqlParameter("@EVENT_NAME", event_name));
            dCmd.Parameters.Add(new SqlParameter("@EVENT_CONDITION", event_condition));
            dCmd.Parameters.Add(new SqlParameter("@EVENT_DATE", event_date));
            dCmd.Parameters.Add(new SqlParameter("@PHONE_SEND_ZALO", phone_sendzalo));
            dCmd.Parameters.Add(new SqlParameter("@NGAY_GUI", "")); // trong db lay ngay hien tai
            dCmd.Parameters.Add(new SqlParameter("@NGUOI_THUC_HIEN", staff_name));
            dCmd.Parameters.Add(new SqlParameter("@ID_TEMPLATE", ID_template));



            SqlDataAdapter da = new SqlDataAdapter(dCmd);
            DataTable table = new DataTable();
            // execute the command
            rdr = dCmd.ExecuteReader();
            connetion.Close();
        }

        public string SendAPIVoucher(string event_name, string event_date, string value, string customer_name, string customer_year, string phone_number, string event_condition, string start_date, string end_date, string voucher_code, string customer_code, string token, string phone_sendzalo, string staff_name)
        {
            var username = User.Identity.Name;
            var obj = new
            {
                phone = phone_sendzalo,
                template_id = "387768",
                template_data = new
                {
                    event_name = event_name,
                    event_date = event_date,
                    value = value,
                    customer_name = customer_name,
                    event_condition = event_condition,
                    start_date = start_date,
                    end_date = end_date,
                    voucher_code = voucher_code,
                    customer_id = customer_code
                },
                tracking_id = customer_code
            };
            const string URL = "https://business.openapi.zalo.me/message/template";
            string urlParameters = "";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("access_token", token);
            HttpResponseMessage response = client.PostAsJsonAsync(URL, obj).Result;
            var responseContent = "FAIL";
            string responseContent_message = "ZALO KHÔNG PHẢN HỒI";
            if (response.IsSuccessStatusCode)
            {
                responseContent = response.Content.ReadAsStringAsync().Result;
                var json_serializer = new JavaScriptSerializer();
                dynamic item = json_serializer.Deserialize<object>(responseContent);
               responseContent_message = item["message"];
            }
            GhiLogSendAPIVoucher(voucher_code, start_date, end_date, value, customer_name, customer_code, customer_year, phone_number, event_name, event_condition, event_date, phone_sendzalo, staff_name, responseContent_message, "387768");
            if(responseContent_message == "Success")
            {
                GhiLogVoucher(voucher_code, start_date, end_date, value, customer_name, customer_code, customer_year, phone_number, event_name, event_condition, event_date, phone_sendzalo, staff_name , "387768");
            }
            client.Dispose();
            return responseContent;
        }

        public JsonResult SELECT_LOG_VOUCHER_TUNGAY_DENNGAY(string TUNGAY, string DENNGAY)
        {
            //TUNGAY = "08/10/2023";
            DataTable dt = new DataTable();
            SqlConnection connetion = null;
            SqlDataReader rdr = null;
            //data source = localhost\SQLEXPRESS2014; initial catalog = pkdkdinhtrongson; user id = pkdkdinhtrongson_admin; password = kid@1412; MultipleActiveResultSets = True; App = EntityFramework
            //connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson_v2;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connetion = new SqlConnection(@"data source=localhost\SQLEXPRESS2014;initial catalog=pkdkdinhtrongson_v2;user id=pkdkdinhtrongson_admin;password=kid@1412;MultipleActiveResultSets=True;App=EntityFramework");

            connetion.Open();
            SqlCommand dCmd = new SqlCommand("SELECT_LOG_VOUCHER_TUNGAY_DENNGAY", connetion);
            dCmd.CommandType = CommandType.StoredProcedure;
            dCmd.Parameters.Add(new SqlParameter("@TUNGAY", TUNGAY));
            dCmd.Parameters.Add(new SqlParameter("@DENNGAY", DENNGAY));
            SqlDataAdapter da = new SqlDataAdapter(dCmd);
            da.Fill(dt);
            // execute the command
            rdr = dCmd.ExecuteReader();
            DataTable dtData = new DataTable("Data");
            DataTable dtSchema = new DataTable("Schema");


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            var String_rows = serializer.Serialize(rows);

            connetion.Close();
            return Json(String_rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SELECT_VOUCHER_TUNGAY_DENNGAY(string TUNGAY, string DENNGAY)
        {
            //TUNGAY = "08/10/2023";
            DataTable dt = new DataTable();
            SqlConnection connetion = null;
            SqlDataReader rdr = null;
            //data source = localhost\SQLEXPRESS2014; initial catalog = pkdkdinhtrongson; user id = pkdkdinhtrongson_admin; password = kid@1412; MultipleActiveResultSets = True; App = EntityFramework
            //connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson_v2;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connetion = new SqlConnection(@"data source=localhost\SQLEXPRESS2014;initial catalog=pkdkdinhtrongson_v2;user id=pkdkdinhtrongson_admin;password=kid@1412;MultipleActiveResultSets=True;App=EntityFramework");

            connetion.Open();
            SqlCommand dCmd = new SqlCommand("SELECT_VOUCHER_TUNGAY_DENNGAY", connetion);
            dCmd.CommandType = CommandType.StoredProcedure;
            dCmd.Parameters.Add(new SqlParameter("@TUNGAY", TUNGAY));
            dCmd.Parameters.Add(new SqlParameter("@DENNGAY", DENNGAY));
            SqlDataAdapter da = new SqlDataAdapter(dCmd);
            da.Fill(dt);
            // execute the command
            rdr = dCmd.ExecuteReader();
            DataTable dtData = new DataTable("Data");
            DataTable dtSchema = new DataTable("Schema");


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            var String_rows = serializer.Serialize(rows);

            connetion.Close();
            return Json(String_rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SELECT_VOUCHER_ALL()
        {
            //TUNGAY = "08/10/2023";
            DataTable dt = new DataTable();
            SqlConnection connetion = null;
            SqlDataReader rdr = null;
            //data source = localhost\SQLEXPRESS2014; initial catalog = pkdkdinhtrongson; user id = pkdkdinhtrongson_admin; password = kid@1412; MultipleActiveResultSets = True; App = EntityFramework
            //connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson_v2;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connetion = new SqlConnection(@"data source=localhost\SQLEXPRESS2014;initial catalog=pkdkdinhtrongson_v2;user id=pkdkdinhtrongson_admin;password=kid@1412;MultipleActiveResultSets=True;App=EntityFramework");

            connetion.Open();
            SqlCommand dCmd = new SqlCommand("SELECT_VOUCHER_ALL", connetion);
            dCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(dCmd);
            da.Fill(dt);
            // execute the command
            rdr = dCmd.ExecuteReader();
            DataTable dtData = new DataTable("Data");
            DataTable dtSchema = new DataTable("Schema");


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            var String_rows = serializer.Serialize(rows);

            connetion.Close();
            return Json(String_rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CHECKIN_VOUCHER(string ID)
        {
            //TUNGAY = "08/10/2023";
            DataTable dt = new DataTable();
            SqlConnection connetion = null;
            SqlDataReader rdr = null;
            //data source = localhost\SQLEXPRESS2014; initial catalog = pkdkdinhtrongson; user id = pkdkdinhtrongson_admin; password = kid@1412; MultipleActiveResultSets = True; App = EntityFramework
            //connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson_v2;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connetion = new SqlConnection(@"data source=localhost\SQLEXPRESS2014;initial catalog=pkdkdinhtrongson_v2;user id=pkdkdinhtrongson_admin;password=kid@1412;MultipleActiveResultSets=True;App=EntityFramework");

            connetion.Open();
            SqlCommand dCmd = new SqlCommand("CHECKIN_VOUCHER", connetion);
            dCmd.Parameters.Add(new SqlParameter("@ID", ID));
            dCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(dCmd);
            da.Fill(dt);
            // execute the command
            rdr = dCmd.ExecuteReader();
            DataTable dtData = new DataTable("Data");
            DataTable dtSchema = new DataTable("Schema");


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            var String_rows = serializer.Serialize(rows);

            connetion.Close();
            return Json(String_rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HUY_CHECKIN_VOUCHER(string ID)
        {
            //TUNGAY = "08/10/2023";
            DataTable dt = new DataTable();
            SqlConnection connetion = null;
            SqlDataReader rdr = null;
            //data source = localhost\SQLEXPRESS2014; initial catalog = pkdkdinhtrongson; user id = pkdkdinhtrongson_admin; password = kid@1412; MultipleActiveResultSets = True; App = EntityFramework
            //connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson_v2;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            connetion = new SqlConnection(@"data source=localhost\SQLEXPRESS2014;initial catalog=pkdkdinhtrongson_v2;user id=pkdkdinhtrongson_admin;password=kid@1412;MultipleActiveResultSets=True;App=EntityFramework");

            connetion.Open();
            SqlCommand dCmd = new SqlCommand("HUY_CHECKIN_VOUCHER", connetion);
            dCmd.Parameters.Add(new SqlParameter("@ID", ID));
            dCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(dCmd);
            da.Fill(dt);
            // execute the command
            rdr = dCmd.ExecuteReader();
            DataTable dtData = new DataTable("Data");
            DataTable dtSchema = new DataTable("Schema");


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            var String_rows = serializer.Serialize(rows);

            connetion.Close();
            return Json(String_rows, JsonRequestBehavior.AllowGet);
        }

    }
}
