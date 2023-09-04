using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using TLBD.Areas.admin.Controllers;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Models.ThongTin
{
    public class ThongTin_Class
    {
        EntitiesData ctx = new EntitiesData();

        public IList<ThongTin_Model> Get_List_TT()
        {
            return ctx.THONG_TIN
                .Select(x => new ThongTin_Model
                {
                    sID = x.ID
                }).ToList();
        }

        public ThongTin_Model Get()
        {
            return ctx.THONG_TIN
                .Select(x => new ThongTin_Model
                {
                    sID = x.ID,
                    sTEN_DONVI = x.TEN_DONVI,
                    sDIACHI_DONVI = x.DIACHI_DONVI,
                    sGIAY_PHEP = x.GIAY_PHEP,
                    sSDT = x.SDT,
                    sFAX = x.FAX,
                    sDIDONG = x.DIDONG,
                    sEMAIL = x.EMAIL,
                    sTHIET_KE = x.THIET_KE,
                    sLAT = x.LATITUDE,
                    sLNG = x.LONGTITUDE,
                    sFACEBOOK = x.FACEBOOK,
                    CHAO_MUNG = x.CHAOMUNG,
                    MAP_IMG = x.MAP_IMG,
                    MAP_LINK = x.MAP_LINK,
                    VIDEO_LINK = x.VIDEO_LINK
                })
                .FirstOrDefault();
        }

        public int CapNhat(ThongTin_Model model)
        {
            var data = ctx.THONG_TIN.Where(x => x.ID == model.sID).FirstOrDefault();

            if (data != null)
            {
                data.TEN_DONVI = model.sTEN_DONVI;
                data.DIACHI_DONVI = model.sDIACHI_DONVI;
                data.GIAY_PHEP = model.sGIAY_PHEP;
                data.SDT = model.sSDT;
                data.DIDONG = model.sDIDONG;
                data.EMAIL = model.sEMAIL;
                data.FAX = model.sFAX;
                data.NGAY_SUA = model.sNGAY_SUA;
                data.IP_SUA = model.sIP_SUA;
                data.FACEBOOK = model.sFACEBOOK;
                data.LATITUDE = model.sLAT;
                data.LONGTITUDE = model.sLNG;
                data.CHAOMUNG = model.CHAO_MUNG;
                data.MAP_IMG = !string.IsNullOrEmpty(model.MAP_IMG) ? model.MAP_IMG : data.MAP_IMG;
                data.MAP_LINK = model.MAP_LINK;
                data.VIDEO_LINK = model.VIDEO_LINK;
            }
            else
            {
                data = new THONG_TIN()
                {
                    TEN_DONVI = model.sTEN_DONVI,
                    DIACHI_DONVI = model.sDIACHI_DONVI,
                    GIAY_PHEP = model.sGIAY_PHEP,
                    SDT = model.sSDT,
                    DIDONG = model.sDIDONG,
                    EMAIL = model.sEMAIL,
                    FAX = model.sFAX,
                    NGAY_SUA = model.sNGAY_SUA,
                    IP_SUA = model.sIP_SUA,
                    FACEBOOK = model.sFACEBOOK,
                    LATITUDE = model.sLAT,
                    LONGTITUDE = model.sLNG,
                    CHAOMUNG = model.CHAO_MUNG,
                    MAP_LINK = model.MAP_LINK,
                    MAP_IMG = model.MAP_IMG,
                    VIDEO_LINK = model.VIDEO_LINK
                };

                ctx.THONG_TIN.Add(data);
            }

            try
            {
                ctx.SaveChanges();
                return data.ID;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }

                return -1;
            }
        }
    }
}