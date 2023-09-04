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

namespace TLBD.Areas.admin.Models.Logo
{
    public class Logo_Class
    {
        //Logo_DBDataContext ctx = new Logo_DBDataContext();
        static EntitiesData ctx = new EntitiesData();

        public int GetCount()
        {
            return ctx.LOGOes
                .Count(x => x.TRANG_THAI != 99);
        }

        public IList<Logo_Model> GetList(int page, int pageSize)
        {
            return ctx.LOGOes
                 .Where(x => x.TRANG_THAI != 99)
                 .Select(z => new Logo_Model
                 {
                     sID = z.ID,
                     sTIEUDE = z.TIEUDE,
                     sIMG_URL = z.IMG_URL,
                     sHIENTHI = z.HIENTHI
                 })
                 .OrderBy(y => y.sID)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();
        }

        public Logo_Model Get(int id)
        {
            return ctx.LOGOes
                .Where(x => x.ID == id)
                .Select(y => new Logo_Model
                {
                    sID = y.ID,
                    sTIEUDE = y.TIEUDE,
                    sIMG_URL = y.IMG_URL,
                    sHIENTHI = y.HIENTHI
                })
                .FirstOrDefault();
        }

        public int Capnhat(Logo_Model m)
        {
            var data = ctx.LOGOes.Where(x => x.ID == m.sID).FirstOrDefault();

            if (data != null)
            {
                data.HIENTHI = m.sHIENTHI;
                data.IMG_URL = !string.IsNullOrEmpty(m.sIMG_URL) ? m.sIMG_URL : data.IMG_URL;
                data.IP_SUA = m.sIP_SUA;
                data.IP_TAO = m.sIP_TAO;
                data.IP_XOA = m.sIP_XOA;
                data.NGAY_SUA = m.sNGAY_SUA;
                data.NGAY_TAO = m.sNGAY_TAO;
                data.NGAY_XOA = m.sNGAY_XOA;
                data.TIEUDE = m.sTIEUDE;
                data.TRANG_THAI = m.sTRANG_THAI;
            }
            else
            {
                data = new LOGO()
                {
                    ID = m.sID,
                    HIENTHI = m.sHIENTHI,
                    IMG_URL = m.sIMG_URL,
                    IP_SUA = m.sIP_SUA,
                    IP_TAO = m.sIP_TAO,
                    IP_XOA = m.sIP_XOA,
                    NGAY_SUA = m.sNGAY_SUA,
                    NGAY_TAO = m.sNGAY_TAO,
                    NGAY_XOA = m.sNGAY_XOA,
                    TIEUDE = m.sTIEUDE,
                    TRANG_THAI = m.sTRANG_THAI
                };

                ctx.LOGOes.Add(data);
            }

            if (m.sHIENTHI == true)
            {
                ctx.LOGOes.Where(x => x.ID != m.sID)
                    .ToList()
                    .ForEach(y => y.HIENTHI = false);
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

        public void Delete(Logo_Model model)
        {
            var exist = ctx.LOGOes.Where(x => x.ID == model.sID).ToList().Count;

            if (exist > 0)
            {
                ctx.LOGOes
                    .Where(x => x.ID == model.sID)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.HIENTHI = false;
                        x.IP_XOA = model.sIP_XOA;
                        x.NGAY_XOA = model.sNGAY_XOA;
                        x.TRANG_THAI = 99;
                    });

                try
                {
                    ctx.SaveChanges();
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
                }
            }
        }

        public static Logo_Model Logo()
        {
            var result = ctx.sp_LAY_DS_LOGO()
                .Select(x => new Logo_Model 
                {
                    sIMG_URL = x.IMG_URL
                }).FirstOrDefault();

            return result;
        }
    }
}