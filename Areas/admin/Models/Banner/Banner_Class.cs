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
using System.Web.Mvc;
using TLBD.Areas.admin.Controllers;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Models.Banner
{
    public class Banner_Class
    {

        static EntitiesData ctx = new EntitiesData();

        public int GetCount()
        {
            return ctx.BANNERs
                .Count(x => x.TRANG_THAI != 99);
        }

        public IList<Banner_Model> GetList(int page, int pageSize)
        {
            return ctx.BANNERs
                .Where(x => x.TRANG_THAI != 99)
                .Select(z => new Banner_Model
                {
                    ID = z.ID,
                    TIEUDE = z.TIEUDE,
                    IMG_URL = z.IMG_URL,
                    IMG_URL_MB = z.IMG_URL_MB,
                    HIENTHI = z.HIENTHI,
                    SET_UP = z.SET_UP

                })
                .OrderBy(y => y.ID)
                .Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public Banner_Model Get(int id)
        {
            return ctx.BANNERs
                .Where(x => x.ID == id)
                .Select(y => new Banner_Model
                {
                    ID = y.ID,
                    TIEUDE = y.TIEUDE,
                    IMG_URL = y.IMG_URL,
                    IMG_URL_MB = y.IMG_URL_MB,
                    HIENTHI = y.HIENTHI,
                    SET_UP = y.SET_UP
                })
                .FirstOrDefault();
        }

        public Banner_Model Get()
        {
            return ctx.BANNERs
               
                .Select(y => new Banner_Model
                {
                    ID = y.ID,
                    TIEUDE = y.TIEUDE,
                    IMG_URL = y.IMG_URL,
                    IMG_URL_MB = y.IMG_URL_MB,
                    HIENTHI = y.HIENTHI,
                    SET_UP = y.SET_UP
                })
                .FirstOrDefault();
        }

        //public static List<Banner_Model> Banner_11()
        //{
        //    return ctx.BANNERs
        //        .Where(x => x.ID != 99 && x.SET_UP == 11)
        //        .Select(y => new Banner_Model
        //        {
        //            ID = y.ID,
        //            TIEUDE = y.TIEUDE,
        //            IMG_URL = y.IMG_URL,
        //            IMG_URL_MB = y.IMG_URL_MB,
        //            HIENTHI = y.HIENTHI,
        //            SET_UP = y.SET_UP
        //        })
        //        .ToList();
        //}

        //public static List<Banner_Model> Banner_12()
        //{
        //    return ctx.BANNERs
        //        .Where(x => x.ID != 99 && x.SET_UP == 12)
        //        .Select(y => new Banner_Model
        //        {
        //            ID = y.ID,
        //            TIEUDE = y.TIEUDE,
        //            IMG_URL = y.IMG_URL,
        //            IMG_URL_MB = y.IMG_URL_MB,
        //            HIENTHI = y.HIENTHI,
        //            SET_UP = y.SET_UP
        //        })
        //        .ToList();
        //}

        //public static List<Banner_Model> Banner_13()
        //{
        //    return ctx.BANNERs
        //        .Where(x => x.ID != 99 && x.SET_UP == 13)
        //        .Select(y => new Banner_Model
        //        {
        //            ID = y.ID,
        //            TIEUDE = y.TIEUDE,
        //            IMG_URL = y.IMG_URL,
        //            IMG_URL_MB = y.IMG_URL_MB,
        //            HIENTHI = y.HIENTHI,
        //            SET_UP = y.SET_UP
        //        })
        //        .ToList();
        //}

        //public static List<Banner_Model> Banner_14()
        //{
        //    return ctx.BANNERs
        //        .Where(x => x.ID != 99 && x.SET_UP == 14)
        //        .Select(y => new Banner_Model
        //        {
        //            ID = y.ID,
        //            TIEUDE = y.TIEUDE,
        //            IMG_URL = y.IMG_URL,
        //            IMG_URL_MB = y.IMG_URL_MB,
        //            HIENTHI = y.HIENTHI,
        //            SET_UP = y.SET_UP
        //        })
        //        .ToList();
        //}

        public int Capnhat(Banner_Model m)
        {
            var data = ctx.BANNERs.Where(x => x.ID == m.ID).FirstOrDefault();

            if (data != null)
            {
                data.HIENTHI = m.HIENTHI;
                data.IMG_URL = !string.IsNullOrEmpty(m.IMG_URL) ? m.IMG_URL : data.IMG_URL;
                data.IMG_URL_MB = !string.IsNullOrEmpty(m.IMG_URL_MB) ? m.IMG_URL_MB : data.IMG_URL_MB;
                data.IP_SUA = m.IP_SUA;
                data.IP_TAO = m.IP_TAO;
                data.IP_XOA = m.IP_XOA;
                data.NGAY_SUA = m.NGAY_SUA;
                data.NGAY_TAO = m.NGAY_TAO;
                data.NGAY_XOA = m.NGAY_XOA;
                data.TIEUDE = m.TIEUDE;
                data.TRANG_THAI = m.TRANG_THAI;
                data.SET_UP = m.SET_UP;
            }
            else
            {
                data = new BANNER()
                {
                    ID = m.ID,
                    HIENTHI = m.HIENTHI,
                    IMG_URL = m.IMG_URL,
                    IMG_URL_MB = m.IMG_URL_MB,
                    IP_SUA = m.IP_SUA,
                    IP_TAO = m.IP_TAO,
                    IP_XOA = m.IP_XOA,
                    NGAY_SUA = m.NGAY_SUA,
                    NGAY_TAO = m.NGAY_TAO,
                    NGAY_XOA = m.NGAY_XOA,
                    TIEUDE = m.TIEUDE,
                    TRANG_THAI = m.TRANG_THAI,
                    SET_UP = m.SET_UP
                };

                ctx.BANNERs.Add(data);
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

        public void Delete(Banner_Model model)
        {
            var exist = ctx.BANNERs.Where(x => x.ID == model.ID).ToList().Count;

            if (exist > 0)
            {
                ctx.BANNERs
                    .Where(x => x.ID == model.ID)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.HIENTHI = false;
                        x.IP_XOA = model.IP_XOA;
                        x.NGAY_XOA = model.NGAY_XOA;
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

        //public static Banner_Model Banner()
        //{
        //    EntitiesData ctx = new EntitiesData();
        //    return ctx.BANNERs
        //        .Where(x => x.TRANG_THAI == 0 && x.HIENTHI == true)
        //        .OrderByDescending(y => y.NGAY_SUA)
        //        .Select(z => new Banner_Model
        //        {
        //            ID = z.ID,
        //            TIEUDE = z.TIEUDE,
        //            IMG_URL = z.IMG_URL,
        //            IMG_URL_MB = z.IMG_URL_MB,
        //            HIENTHI = z.HIENTHI,
        //            SET_UP = z.SET_UP,
        //        })
        //        .FirstOrDefault();
        //}

        //public static Banner_Model Banner1()
        //{

        //    return ctx.BANNERs
        //        .Where(x => x.TRANG_THAI == 0 && x.SET_UP == 1)
        //        .OrderByDescending(y => y.NGAY_SUA)
        //        .Select(z => new Banner_Model
        //        {
        //            ID = z.ID,
        //            TIEUDE = z.TIEUDE,
        //            IMG_URL = z.IMG_URL,
        //            IMG_URL_MB = z.IMG_URL_MB,
        //            HIENTHI = z.HIENTHI,
        //            SET_UP = z.SET_UP,

        //        })
        //        .FirstOrDefault();
        //}

        //public static Banner_Model Banner2()
        //{

        //    return ctx.BANNERs
        //        .Where(x => x.TRANG_THAI == 0 && x.SET_UP == 2)
        //        .OrderByDescending(y => y.NGAY_SUA)
        //        .Select(z => new Banner_Model
        //        {
        //            ID = z.ID,
        //            TIEUDE = z.TIEUDE,
        //            IMG_URL = z.IMG_URL,
        //            IMG_URL_MB = z.IMG_URL_MB,
        //            HIENTHI = z.HIENTHI,
        //            SET_UP = z.SET_UP,

        //        })
        //        .FirstOrDefault();
        //}

        //public static Banner_Model Banner3()
        //{

        //    return ctx.BANNERs
        //        .Where(x => x.TRANG_THAI == 0 && x.SET_UP == 3)
        //        .OrderByDescending(y => y.NGAY_SUA)
        //        .Select(z => new Banner_Model
        //        {
        //            ID = z.ID,
        //            TIEUDE = z.TIEUDE,
        //            IMG_URL = z.IMG_URL,
        //            IMG_URL_MB = z.IMG_URL_MB,
        //            HIENTHI = z.HIENTHI,
        //            SET_UP = z.SET_UP,

        //        })
        //        .FirstOrDefault();
        //}

        //public static Banner_Model Banner4()
        //{

        //    return ctx.BANNERs
        //        .Where(x => x.TRANG_THAI == 0 && x.SET_UP == 4)
        //        .OrderByDescending(y => y.NGAY_SUA)
        //        .Select(z => new Banner_Model
        //        {
        //            ID = z.ID,
        //            TIEUDE = z.TIEUDE,
        //            IMG_URL = z.IMG_URL,
        //            IMG_URL_MB = z.IMG_URL_MB,
        //            HIENTHI = z.HIENTHI,
        //            SET_UP = z.SET_UP,

        //        })
        //        .FirstOrDefault();
        //}

        //public static Banner_Model Banner5()
        //{

        //    return ctx.BANNERs
        //        .Where(x => x.TRANG_THAI == 0 && x.SET_UP == 5)
        //        .OrderByDescending(y => y.NGAY_SUA)
        //        .Select(z => new Banner_Model
        //        {
        //            ID = z.ID,
        //            TIEUDE = z.TIEUDE,
        //            IMG_URL = z.IMG_URL,
        //            IMG_URL_MB = z.IMG_URL_MB,
        //            HIENTHI = z.HIENTHI,
        //            SET_UP = z.SET_UP,

        //        })
        //        .FirstOrDefault();
        //}

        public List<Banner_Model> LayDSBanner()
        {
            var result = ctx.sp_LAY_DS_BANNER()
                .Select(y => new Banner_Model
                {
                    ID = y.ID,
                    TIEUDE = y.TIEUDE,
                    IMG_URL = y.IMG_URL,
                    IMG_URL_MB = y.IMG_URL_MB,
                    HIENTHI = y.HIENTHI,
                    SET_UP = y.SET_UP
                })
                .ToList();

            return result;
        }

        public Banner_Model GetBanner()
        {
            var result = ctx.BANNERs.Where(x => x.TRANG_THAI == 0)
                .Select(y => new Banner_Model
                {
                    ID = y.ID,
                    TIEUDE = y.TIEUDE,
                    IMG_URL = y.IMG_URL,
                    IMG_URL_MB = y.IMG_URL_MB,
                    HIENTHI = y.HIENTHI,
                    SET_UP = y.SET_UP
                })
                .FirstOrDefault();

            return result;
        }
    }
}