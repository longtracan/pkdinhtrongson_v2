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

namespace TLBD.Areas.admin.Models.GioiTinh
{
    public class GioiTinh_Class
    {
        public static List<GioiTinh_Model> GetList()
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.DM_GIOITINH
                .OrderBy(x => x.ID)
                .Select(x => new GioiTinh_Model
                {
                    ID = x.ID,
                    GIOITINH = x.GIOITINH
                })
                .ToList();
        }

    }
}