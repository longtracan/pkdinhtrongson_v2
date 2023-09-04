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

namespace TLBD.Areas.admin.Models.AlbumDetail
{
    public class AlbumDetail_Class
    {
        EntitiesData ctx = new EntitiesData();

        public int GetCount(int id_album)
        {
            return ctx.ALBUMDETAILs
                .Count(x => x.TRANG_THAI != 99 && x.ID_ALBUM == id_album);
        }

        public IList<AlbumDetail_Model> GetList(int page, int pageSize, int id_album)
        {
            return ctx.ALBUMDETAILs
                .Where(x => x.TRANG_THAI != 99 && x.ID_ALBUM == id_album)
                .Select(x => new AlbumDetail_Model
                {
                    sID = x.ID,
                    sID_ALBUM = x.ID_ALBUM,
                    sIMAGE_URL = x.IMAGE_URL,
                    sDAI_DIEN = x.DAI_DIEN
                })
                .OrderBy(x => x.sID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public AlbumDetail_Model Get(int id)
        {
            return ctx.ALBUMDETAILs
                .Where(x => x.TRANG_THAI != 99 && x.ID == id)
                .Select(x => new AlbumDetail_Model
                {
                    sID = x.ID,
                    sID_ALBUM = x.ID_ALBUM,
                    sIMAGE_URL = x.IMAGE_URL,
                    sDAI_DIEN = x.DAI_DIEN
                })
                .FirstOrDefault();
        }

        public int Capnhat(AlbumDetail_Model model)
        {
            var data = ctx.ALBUMDETAILs.Where(x => x.ID == model.sID).FirstOrDefault();

            if (data != null)
            {
                data.ID_ALBUM = model.sID_ALBUM;
                data.IMAGE_URL = (!string.IsNullOrEmpty(model.sIMAGE_URL) ? model.sIMAGE_URL : data.IMAGE_URL);
                data.DAI_DIEN = model.sDAI_DIEN;
            }
            else
            {
                data = new ALBUMDETAIL()
                {
                    ID_ALBUM = model.sID_ALBUM,
                    IMAGE_URL = (!string.IsNullOrEmpty(model.sIMAGE_URL) ? model.sIMAGE_URL : ""),
                    DAI_DIEN = model.sDAI_DIEN
                };

                ctx.ALBUMDETAILs.Add(data);
            }

            if (model.sDAI_DIEN == true)
            {
                ctx
                    .ALBUMDETAILs
                    .Where(x => x.ID_ALBUM == model.sID_ALBUM && x.ID != model.sID)
                    .ToList()
                    .ForEach(x => x.DAI_DIEN = false);
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
            var exist = ctx.ALBUMDETAILs.Count(x => x.ID == model.sID);

            if (exist > 0)
            {
                ctx.ALBUMDETAILs
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

        public List<AlbumDetail_Model> GetListAlbumChitiet(int id_Album)
        {
            return ctx.ALBUMDETAILs
                .Where(x => x.TRANG_THAI != 99 && x.ID_ALBUM == id_Album)
                .Select(x => new AlbumDetail_Model
                {
                    sID = x.ID,
                    sID_ALBUM = x.ID_ALBUM,
                    sIMAGE_URL = x.IMAGE_URL,
                    sDAI_DIEN = x.DAI_DIEN
                })
                .ToList();
        }
    }

}