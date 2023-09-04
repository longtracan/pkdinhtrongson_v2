using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using TLBD.Areas.admin.Controllers;
using TLBD.Areas.admin.Models.AlbumDetail;
using TLBD.Models.EntityModel;
using MVC4WEB.Functions;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace TLBD.Areas.admin.Models.Album
{
    public class Album_Class
    {
        EntitiesData ctx = new EntitiesData();

        public int GetCount()
        {
            return ctx.ALBUMs.Count(x => x.TRANG_THAI != 99);
        }

        public IList<Album_Model> GetList(int page, int pageSize)
        {
            return ctx.ALBUMs
                .Where(x => x.TRANG_THAI != 99)
                .LeftOuterJoin(ctx.ALBUMDETAILs.Where(x => x.TRANG_THAI != 99 && x.DAI_DIEN == true),
                x => x.ID,
                y => y.ID_ALBUM,
                (x, y) => new Album_Model
                {
                    sID = x.ID,
                    sTIEU_DE = x.TIEU_DE,
                    sNGAY_DANG = x.NGAY_DANG,
                    sIMAGE_URL = y.IMAGE_URL
                })
                .OrderBy(y => y.sID)
                .Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public Album_Model Get(int id)
        {
            return ctx.ALBUMs
                .Where(x => x.ID == id)
                .Select(x => new Album_Model
                {
                    sID = x.ID,
                    sTIEU_DE = x.TIEU_DE,
                    sNGAY_DANG = x.NGAY_DANG
                })
                .FirstOrDefault();
        }

        public int Capnhat(Album_Model model)
        {
            var data = ctx.ALBUMs.Where(x => x.ID == model.sID).FirstOrDefault();

            if (data != null)
            {
                data.TIEU_DE = model.sTIEU_DE;
                data.NGAY_DANG = model.sNGAY_DANG;
                data.NGAY_SUA = model.sNGAY_SUA;
                data.IP_SUA = model.sIP_SUA;
            }
            else
            {
                data = new ALBUM()
                {
                    IP_TAO = model.sIP_TAO,
                    NGAY_TAO = model.sNGAY_TAO,
                    TRANG_THAI = 0,
                    TIEU_DE = model.sTIEU_DE,
                    NGAY_DANG = model.sNGAY_DANG,
                    NGAY_SUA = model.sNGAY_SUA,
                    IP_SUA = model.sIP_SUA,
                };

                ctx.ALBUMs.Add(data);
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

        public void Delete(Album_Model model)
        {
            var exist = ctx.ALBUMs.Count(x => x.ID == model.sID);

            if (exist > 0)
            {
                ctx.ALBUMs
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

        public static List<Album_Model> List_AllAlbum()
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.ALBUMs
                .Where(x => x.TRANG_THAI == 0)
                .Select(x => new Album_Model
                {
                    sID = x.ID,
                    sTIEU_DE = x.TIEU_DE,
                    sNGAY_DANG = x.NGAY_DANG,
                    sIMAGE_URL = ctx.ALBUMDETAILs.Where(y => y.ID_ALBUM == x.ID && y.DAI_DIEN == true).FirstOrDefault() != null ? ctx.ALBUMDETAILs.Where(y => y.ID_ALBUM == x.ID && y.DAI_DIEN == true).FirstOrDefault().IMAGE_URL : "NoImage.jpg"
                })
                .ToList();
        }
    }
}