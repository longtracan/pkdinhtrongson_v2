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
using TLBD.Areas.admin.Models.AlbumDetail;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Models.SanPham_Album
{
    public class SanPham_Album_Class
    {
        EntitiesData ctx = new EntitiesData();

        public int GetCount(int id_album)
        {
            return ctx.SAN_PHAM_ALBUM
                .Count(x => x.TRANG_THAI != 99 && x.ID_ALBUM == id_album);
        }

        public IList<AlbumDetail_Model> GetList(int page, int pageSize, int id_album)
        {
            return ctx.SAN_PHAM_ALBUM
                .Where(x => x.TRANG_THAI != 99 && x.ID_ALBUM == id_album)
                .Select(x => new AlbumDetail_Model
                {
                    sID = x.ID,
                    sID_ALBUM = x.ID_ALBUM,
                    sIMAGE_URL = x.IMAGE_URL
                })
                .OrderBy(x => x.sID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public AlbumDetail_Model Get(int id)
        {
            return ctx.SAN_PHAM_ALBUM
                .Where(x => x.TRANG_THAI != 99 && x.ID == id)
                .Join(ctx.BAI_VIET.Where(x => x.TRANG_THAI != 99),
                x => x.ID_ALBUM,
                y => y.ID,
                (x, y) => new AlbumDetail_Model
                {
                    sID = x.ID,
                    sID_ALBUM = x.ID_ALBUM,
                    sIMAGE_URL = x.IMAGE_URL,
                    sID_NHOM = y.ID_NHOM_BAIVIET
                })
                .FirstOrDefault();
        }

        public int Capnhat(AlbumDetail_Model model)
        {
            var data = ctx.SAN_PHAM_ALBUM.Where(x => x.ID == model.sID).FirstOrDefault();

            if (data != null)
            {
                data.ID_ALBUM = model.sID_ALBUM;
                data.IMAGE_URL = (!string.IsNullOrEmpty(model.sIMAGE_URL) ? model.sIMAGE_URL : data.IMAGE_URL);
            }
            else
            {
                data = new SAN_PHAM_ALBUM()
                {
                    ID_ALBUM = model.sID_ALBUM,
                    IMAGE_URL = (!string.IsNullOrEmpty(model.sIMAGE_URL) ? model.sIMAGE_URL : "")
                };
                ctx.SAN_PHAM_ALBUM.Add(data);
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

        public void Delete(AlbumDetail_Model model)
        {
            var exist = ctx.SAN_PHAM_ALBUM.Count(x => x.ID == model.sID) > 0;
            if (exist)
            {
                ctx.SAN_PHAM_ALBUM
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

        public static List<AlbumDetail_Model> GetListAlbumChitiet(int id_Album)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.SAN_PHAM_ALBUM
                .Where(x => x.TRANG_THAI != 99 && x.ID_ALBUM == id_Album)
                .Select(x => new AlbumDetail_Model
                {
                    sID = x.ID,
                    sID_ALBUM = x.ID_ALBUM,
                    sIMAGE_URL = x.IMAGE_URL
                })
                .ToList();
        }
    }

}