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

namespace TLBD.Areas.admin.Models.Nguoi_VietBai
{
    public class Nguoi_VietBai_Class
    {
        //Nguoi_VietBaiDataContext ctx = new Nguoi_VietBaiDataContext();
        EntitiesData ctx = new EntitiesData();

        public int GetCount()
        {
            return ctx.NGUOI_VIETBAI
                .Count(x => x.TRANG_THAI != 99);
        }

        public IList<Nguoi_VietBai_Model> GetList(int page, int pageSize)
        {
            return ctx.NGUOI_VIETBAI
                .Where(x => x.TRANG_THAI != 99)
                .Select(x => new Nguoi_VietBai_Model
                {
                    sID = x.ID,
                    sTEN = x.TEN
                })
                .OrderByDescending(x => x.sTEN)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public Nguoi_VietBai_Model Get(int id)
        {
            return ctx.NGUOI_VIETBAI
                .Where(x => x.ID == id)
                .Select(x => new Nguoi_VietBai_Model
                {
                    sID = x.ID,
                    sTEN = x.TEN
                })
                .FirstOrDefault();
        }

        public int CapNhat(Nguoi_VietBai_Model model)
        {
            var data = ctx.NGUOI_VIETBAI.Where(x => x.ID == model.sID).FirstOrDefault();

            if (data != null)
            {
                        data.TEN = model.sTEN;
                        data.IP_SUA = model.sIP_SUA;
                        data.NGAY_SUA = model.sNGAY_SUA;
            }
            else
            {
                 data = new NGUOI_VIETBAI()
                {
                    IP_TAO = model.sIP_TAO,
                    NGAY_TAO = model.sNGAY_TAO,
                    TEN = model.sTEN,
                    IP_SUA = model.sIP_SUA,
                    NGAY_SUA = model.sNGAY_SUA
                };

                 ctx.NGUOI_VIETBAI.Add(data);
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

        public void Delete(Nguoi_VietBai_Model model)
        {
            var exist = ctx.NGUOI_VIETBAI.Count(x => x.ID == model.sID);

            if (exist > 0)
            {
                ctx.NGUOI_VIETBAI
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

        public static List<Nguoi_VietBai_Model> List_NguoiVietBai()
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.NGUOI_VIETBAI
                 .Where(x => x.TRANG_THAI != 99)
                 .OrderBy(x => x.ID)
                 .Select(x => new Nguoi_VietBai_Model
                 {
                     sID = x.ID,
                     sTEN = x.TEN
                 })
                 .ToList();
        }
    }
}