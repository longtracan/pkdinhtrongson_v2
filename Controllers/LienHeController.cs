using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLBD.Areas.admin.Models.ThongTin;

namespace TLBD.Controllers
{
    public class LienHeController : Controller
    {
        //
        // GET: /LienHe/

        [GET("Lien-he")]
        public ActionResult Index()
        {
            ThongTin_Class clss = new ThongTin_Class(); 
            return View(clss.Get());
        }

    }
}
