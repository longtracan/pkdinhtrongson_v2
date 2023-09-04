using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using TLBD.Areas.admin.Controllers;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Models.Loai_HienThi
{
    public class Loai_HienThi_Class
    {
        EntitiesData ctx = new EntitiesData(); 
        public static List<Loai_HienThi_Model> GetList_Loai_HienThi()
        {
            EntitiesData ctx = new EntitiesData(); 
            return ctx.LOAI_HIENTHI
                .Where(x => x.ID != 4)//4 chỉ dành cho sản phẩm
                .Select(x => new Loai_HienThi_Model
                {
                    ID = x.ID,
                    LOAI_HIENTHI = x.TEN_LOAI_HIENTHI
                })
                .ToList();
        }
    }
}