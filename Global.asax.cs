using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TLBD.Models;
using WebMatrix.WebData;
using System.Globalization;
using TLBD.Areas.admin.Models.LuotTruyCap;
using TLBD.Controllers;

namespace TLBD
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            ViewEngines.Engines.Add(new RazorViewEngine());

            WebSecurity.InitializeDatabaseConnection("EntitiesDataConnection", "webpages_UserProfile", "UserId", "UserName", autoCreateTables: true);

            HttpRuntime.Cache["online"] = 0;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            LuotTruyCap_Class ltc = new LuotTruyCap_Class();

            //luottruycapDataContext ctx = new luottruycapDataContext();
            //var luot_truycap = ctx.sp_lay_luot_truycap().FirstOrDefault().Column1;
            //HttpRuntime.Cache["luot_truy_cap"] = luot_truycap;

            //var tang_truycap = ctx.sp_tang_luot_truycap();

            //var luottruycap_homnay = ctx.luottruycap_homnay().FirstOrDefault().so_luong;
            //HttpRuntime.Cache["luottruycap_homnay"] = luottruycap_homnay;

            //Tamj comment

            //var luot_truycap = ltc.GetLuotTruyCap();
            //HttpRuntime.Cache["luot_truy_cap"] = luot_truycap;

            //ltc.TangLuotTruyCap();

            //var luottruycap_homnay = ltc.GetLuotTruyCapTrongNgay();
            //HttpRuntime.Cache["luottruycap_homnay"] = luottruycap_homnay;
        }

        //public void Application_Error(Object sender, EventArgs e)
        //{
        //    Exception exception = Server.GetLastError();
        //    Server.ClearError();

        //    var routeData = new RouteData();
        //    routeData.Values.Add("controller", "ErrorPage");
        //    routeData.Values.Add("action", "Error");
        //    routeData.Values.Add("exception", exception);

        //    if (exception.GetType() == typeof(HttpException))
        //    {
        //        routeData.Values.Add("statusCode", ((HttpException)exception).GetHttpCode());
        //    }
        //    else
        //    {
        //        routeData.Values.Add("statusCode", 500);
        //    }

        //    Response.TrySkipIisCustomErrors = true;
        //    IController controller = new ErrorPageController();
        //    controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        //    Response.End();
        //}


        protected void Application_BeginRequest()
        {
            CultureInfo info = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            info.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
        }
    }
}