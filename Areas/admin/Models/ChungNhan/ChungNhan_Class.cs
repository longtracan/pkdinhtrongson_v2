using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Models.ChungNhan
{
    public class ChungNhan_Class
    {
        static EntitiesData ctx = new EntitiesData();

        public int GetCount()
        {
            return ctx.CHUNGNHANs
                .Count(x => x.TRANG_THAI != 99);
        }

        public IList<ChungNhan_Model> GetList(int page, int pageSize)
        {
            return ctx.CHUNGNHANs
                .Where(x => x.TRANG_THAI != 99)
                .Select(z => new ChungNhan_Model
                {
                    ID = z.ID,
                    IMG = z.IMG,
                    NGAY = z.NGAY,
                    TIEU_DE = z.TIEU_DE
                })
                .OrderBy(y => y.ID)
                .Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public ChungNhan_Model Get(int id)
        {
            return ctx.CHUNGNHANs
                .Where(x => x.ID == id)
                .Select(z => new ChungNhan_Model
                {
                    ID = z.ID,
                    IMG = z.IMG,
                    NGAY = z.NGAY,
                    TIEU_DE = z.TIEU_DE
                })
                .FirstOrDefault();
        }

        public int Capnhat(ChungNhan_Model m)
        {
            var data = ctx.CHUNGNHANs.Where(x => x.ID == m.ID).FirstOrDefault();

            if (data != null)
            {
                data.NGAY = m.NGAY;
                data.IMG = !string.IsNullOrEmpty(m.IMG) ? m.IMG : data.IMG;
                data.TRANG_THAI = m.TRANG_THAI;
                data.TIEU_DE = m.TIEU_DE;
            }
            else
            {
                data = new CHUNGNHAN()
                {
                    ID = m.ID,
                    IMG  = m.IMG,
                    TRANG_THAI = m.TRANG_THAI,
                    TIEU_DE = m.TIEU_DE,
                    NGAY = m.NGAY
                };

                ctx.CHUNGNHANs.Add(data);
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

        public void Delete(ChungNhan_Model model)
        {
            var exist = ctx.CHUNGNHANs.Where(x => x.ID == model.ID).ToList().Count;

            if (exist > 0)
            {
                ctx.CHUNGNHANs
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

        public List<ChungNhan_Model> LayDanhSachChungNhan()
        {
            var result = ctx.sp_LAY_DS_CHUNG_NHAN()
                .Select(x => new ChungNhan_Model
                {
                    ID = x.ID,
                    IMG = x.IMG
                }).ToList();

            return result;
        }
    }
}