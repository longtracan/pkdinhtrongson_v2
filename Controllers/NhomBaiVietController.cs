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
        
        public string SendAPIZalo(string phone, string number, string schedule_time, string phone_number, string customer_name, string product_name, string customer_code, string token, string staff_name)
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
                template_id = "255076",
                template_data = new
                {
                    number = number,
                    schedule_time = schedule_time,
                    phone_number = phone_number,
                    customer_name = customer_name,
                    product_name = product_name,
                    customer_code = customer_code
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
            var responseContent = "FAILURE";
            if (response.IsSuccessStatusCode)
            {
                responseContent = response.Content.ReadAsStringAsync().Result;
            }
            GhiLogSendZalo(phone, number, schedule_time, phone_number, customer_name, product_name, customer_code, responseContent, staff_name, username, 255076);
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
                // Parse the response body.

            }
            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            //GhiLogSendZalo(phone, number, schedule_time, phone_number, customer_name, product_name, customer_code, responseContent, "", username, 255076);
            client.Dispose();
            return responseContent;
        }
        public void GhiLogSendZalo(string phone, string number, string schedule_time, string phone_number, string customer_name, string product_name, string customer_code, string result, string ghichu, string taikhoan, int templat_id)
        {
            SqlConnection connetion = null;
            SqlDataReader rdr = null;
            //data source = localhost\SQLEXPRESS2014; initial catalog = pkdkdinhtrongson; user id = pkdkdinhtrongson_admin; password = kid@1412; MultipleActiveResultSets = True; App = EntityFramework
            //connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
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
            connetion = new SqlConnection("Data Source=HUYDT-BDH;Initial Catalog=pkdkdinhtrongson_v2;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
            //connetion = new SqlConnection(@"data source=localhost\SQLEXPRESS2014;initial catalog=pkdkdinhtrongson_v2;user id=pkdkdinhtrongson_admin;password=kid@1412;MultipleActiveResultSets=True;App=EntityFramework");

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
    }
}
