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

namespace TLBD.Areas.admin.Models.LuotTruyCap
{
    public class LuotTruyCap_Class
    {
        EntitiesData ctx = new EntitiesData();

        public int GetLuotTruyCap()
        {


            return ctx.LUOT_TRUY_CAP.Sum(x => x.so_luong);
        }

        public int GetLuotTruyCapTrongNgay()
        {
            var Today = DateTime.Today;
            string date = Today.Day + "/" + Today.Month + "/" + Today.Year;

            var _r = ctx.LUOT_TRUY_CAP
                .Where(x => x.NGAY_THANG == date)
                .Sum(x => x.so_luong);

            return _r;
        }

        public void TangLuotTruyCap()
        {
            var Today = DateTime.Today;
            string date = Today.Day + "/" + Today.Month + "/" + Today.Year;

            bool isExist = (ctx.LUOT_TRUY_CAP
                .Count(x => x.NGAY_THANG == date) > 0);

            if (isExist)
            {
                ctx.LUOT_TRUY_CAP.Where(x => x.NGAY_THANG == date)
                    .ToList()
                    .ForEach(x => x.so_luong++);
            }
            else
            {
                ctx.LUOT_TRUY_CAP.Add(new LUOT_TRUY_CAP
                {
                    NGAY_THANG = date,
                    so_luong = 1
                });
            }

            ctx.SaveChanges();
        }
    }
}