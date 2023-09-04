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

namespace TLBD.Areas.admin.Models.Background
{
    public class Background_Class
    {
        static EntitiesData ctx = new EntitiesData();

        public int GetCount()
        {
            return ctx.LOGOes
                .Count(x => x.TRANG_THAI != 99);
        }

        public IList<Background_Model> GetList(int page, int pageSize)
        {
            return ctx.BACKGROUNDs
                 .Where(x => x.TRANG_THAI != 99)
                 .Select(z => new Background_Model
                 {
                     ID = z.ID,
                     IMG = z.IMG,
                 })
                 .OrderBy(y => y.ID)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();
        }

        public Background_Model Get(int id)
        {
            return ctx.BACKGROUNDs
                .Where(x => x.ID == id)
                .Select(y => new Background_Model
                {
                    ID = y.ID,
                    IMG = y.IMG
                })
                .FirstOrDefault();
        }

        public int Capnhat(Background_Model m)
        {
            var data = ctx.BACKGROUNDs.Where(x => x.ID == m.ID).FirstOrDefault();

            if (data != null)
            {
                if (m.IMG != null && m.IMG != "")
                {
                    data.IMG = m.IMG;
                }
            }
            else
            {
                data = new BACKGROUND()
                {
                    IMG = m.IMG
                };

                ctx.BACKGROUNDs.Add(data);
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

        public void Delete(Background_Model model)
        {
            var exist = ctx.BACKGROUNDs.Where(x => x.ID == model.ID).ToList().Count;

            if (exist > 0)
            {
                ctx.BACKGROUNDs
                    .Where(x => x.ID == model.ID)
                    .ToList()
                    .ForEach(x =>
                    {
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

        public Background_Model Background()
        {
            var result = ctx.sp_LAY_DS_BACKGROUND()
                .Select(x => new Background_Model 
                {
                    IMG = x.IMG
                }).FirstOrDefault();

            return result;
        }
    }
}