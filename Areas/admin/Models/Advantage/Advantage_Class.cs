using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Models.Advantage
{
    public class Advantage_Class
    {
        EntitiesData ctx = new EntitiesData();

        internal int GetCount()
        {
            return ctx.UU_DIEM
                .Count(x => x.TRANG_THAI == 0);
        }

        public IList<Advantage_Model> GetList(int page, int pageSize)
        {
            return ctx.UU_DIEM
                .Where(x => x.TRANG_THAI == 0)
                .Select(x => new Advantage_Model
                {
                    sID = x.ID,
                    sNOI_DUNG = x.NOI_DUNG,
                    sHIEN_THI = x.HIEN_THI
                })
                .OrderBy(y => y.sID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IList<Advantage_Model> GetTop(int top)
        {
            return ctx.UU_DIEM
                .Where(x => x.TRANG_THAI == 0 && x.HIEN_THI == true)
                .Select(x => new Advantage_Model
                {
                    sID = x.ID,
                    sNOI_DUNG = x.NOI_DUNG,
                    sHIEN_THI = x.HIEN_THI
                })
                .OrderBy(y => y.sID)
                .Take(top)
                .ToList();
        }

        public Advantage_Model Get(int id)
        {
            return ctx.UU_DIEM
               .Where(x => x.ID == id)
               .Select(x => new Advantage_Model
               {
                   sID = x.ID,
                   sNOI_DUNG = x.NOI_DUNG,
                   sHIEN_THI = x.HIEN_THI
               })
               .FirstOrDefault();
        }

        internal int Capnhat(Advantage_Model model)
        {
            var data = ctx.UU_DIEM.Where(x => x.ID == model.sID)
                .FirstOrDefault();

            if (data != null)
            {
                data.NOI_DUNG = model.sNOI_DUNG;
                data.HIEN_THI = model.sHIEN_THI;
            }
            else
            {
                data = new UU_DIEM()
                {
                    NOI_DUNG = model.sNOI_DUNG,
                    TRANG_THAI = model.sTRANG_THAI,
                    HIEN_THI = model.sHIEN_THI,
                    
                };

                ctx.UU_DIEM.Add(data);
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

        internal void Delete(Advantage_Model model)
        {
            var exist = ctx.UU_DIEM.Where(x => x.ID == model.sID).ToList().Count;

            if (exist > 0)
            {
                ctx.UU_DIEM
                    .Where(x => x.ID == model.sID)
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
    }
}