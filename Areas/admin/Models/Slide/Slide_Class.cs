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

namespace TLBD.Areas.admin.Models.Slide
{
    public class Slide_Class
    {
        EntitiesData ctx = new EntitiesData();

        public int GetCount()
        {
            return ctx.SLIDEs
                .Count(x => x.TRANG_THAI != 99);
        }

        public IList<Slide_Model> GetList(int page, int pageSize)
        {
            return ctx.SLIDEs
                .Where(x => x.TRANG_THAI != 99)
                .Select(x => new Slide_Model
                {
                    sID = x.ID,
                    sTIEUDE = x.TIEUDE,
                    sIMG_URL = x.IMG_URL,
                    sHIENTHI = x.HIENTHI,
                    sLINK = x.LINK
                })
                .OrderBy(y => y.sID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Slide_Model Get(int id)
        {
            return ctx.SLIDEs
                .Where(x => x.ID == id)
                .Select(x => new Slide_Model
                {
                    sID = x.ID,
                    sTIEUDE = x.TIEUDE,
                    sIMG_URL = x.IMG_URL,
                    sLINK = x.LINK,
                    sHIENTHI = x.HIENTHI
                })
                .FirstOrDefault();
        }

        public int Capnhat(Slide_Model model)
        {
            var data = ctx.SLIDEs.Where(x => x.ID == model.sID).FirstOrDefault();

            if (data != null)
            {
                        data.TIEUDE = model.sTIEUDE;
                        data.IMG_URL = !string.IsNullOrEmpty(model.sIMG_URL) ? model.sIMG_URL : data.IMG_URL;
                        data.LINK = model.sLINK;
                        data.HIENTHI = model.sHIENTHI;
                        data.NGAY_SUA = model.sNGAY_SUA;
                        data.IP_SUA = model.sIP_SUA;
            }
            else
            {
                data = new SLIDE()
                {
                    IP_TAO = model.sIP_TAO,
                    NGAY_TAO = model.sNGAY_TAO,
                    TRANG_THAI = model.sTRANG_THAI,
                    TIEUDE = model.sTIEUDE,
                    IMG_URL = !string.IsNullOrEmpty(model.sIMG_URL) ? model.sIMG_URL : "",
                    LINK = model.sLINK,
                    HIENTHI = model.sHIENTHI,
                    NGAY_SUA = model.sNGAY_SUA,
                    IP_SUA = model.sIP_SUA
                };

                ctx.SLIDEs.Add(data);
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

        public void Delete(Slide_Model model)
        {
            var exist = ctx.SLIDEs.Where(x => x.ID == model.sID).ToList().Count;

            if (exist > 0)
            {
                ctx.SLIDEs
                    .Where(x => x.ID == model.sID)
                    .ToList()
                    .ForEach(x =>
                    {
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

        public List<Slide_Model> List_Slide()
        {
            return ctx.SLIDEs
                .Where(x => x.TRANG_THAI == 0 && x.HIENTHI == true)
                .Select(x => new Slide_Model
                {
                    sID = x.ID,
                    sTIEUDE = x.TIEUDE,
                    sIMG_URL = x.IMG_URL,
                    sHIENTHI = x.HIENTHI,
                    sLINK = x.LINK
                })
                .OrderByDescending(y => y.sID)
                .ToList();
        }
    }
}