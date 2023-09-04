using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLBD.Areas.admin.Models.Album;
using TLBD.Areas.admin.Models.AlbumDetail;

namespace TLBD.Controllers
{
    public class AlbumController : Controller
    {
        //
        // GET: /Album/

        [GET("Hinh-anh")]
        public ActionResult Index()
        {
            return View(Album_Class.List_AllAlbum());
        }

        [GET("Hinh-anh/Chi-tiet/{TenAlbum}/{id}")]
        public ActionResult Detail(int id)
        {

            AlbumDetail_Class clss = new AlbumDetail_Class();
            return View(clss.GetListAlbumChitiet(id));
        }

    }
}
