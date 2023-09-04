using System.Web.Mvc;

namespace TLBD.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "admin_default_123",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", controller = "Account", id = UrlParameter.Optional }
            );
        }
    }
}
