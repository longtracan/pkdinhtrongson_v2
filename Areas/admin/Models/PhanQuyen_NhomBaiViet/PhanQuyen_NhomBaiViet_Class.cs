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

namespace TLBD.Areas.admin.Models.PhanQuyen_NhomBaiViet
{
    public class PhanQuyen_NhomBaiViet_Class
    {
        EntitiesData ctx = new EntitiesData();

        public static int Count_NhomBaiViet_PhanQuyen(int id)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.PHANQUYEN_CHUYENMUC
                    .Count(x => x.ID_USER == id);
        }

        public static int KiemTra_NhomBaiViet_PhanQuyen(int id_nhombaiviet)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.PHANQUYEN_CHUYENMUC
                .Count(x => x.ID_CHUYENMUC == id_nhombaiviet);
        }

        public static List<PhanQuyen_NhomBaiViet_Model> List_ChuyenMuc_PhanQuyen(int id)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.PHANQUYEN_CHUYENMUC
                .Where(x => x.ID_USER == id)
                .Join(ctx.CHUYEN_MUC.Where(x => x.TRANG_THAI == 0 && x.LINK == null).OrderBy(x => x.ID),
                x => x.ID_CHUYENMUC,
                y => y.ID,
                (x, y) => new PhanQuyen_NhomBaiViet_Model
                {
                    sID_USER = x.ID_USER,
                    sID_NHOMBAIVIET = x.ID_CHUYENMUC,
                    sTRANG_THAI = x.TRANG_THAI,
                    sTEN_NHOMBAIVIET1 = y.TEN_CHUYENMUC
                }).ToList();
        }

        public int Capnhat(PhanQuyen_NhomBaiViet_Model model)
        {
            var data = ctx.PHANQUYEN_CHUYENMUC.Where(x => x.ID_USER == model.sID_USER && x.ID_CHUYENMUC == model.sID_NHOMBAIVIET).FirstOrDefault();

            if (data != null)
            {
                data.TRANG_THAI = model.sTRANG_THAI;
            }

            try
            {
                ctx.SaveChanges();
                return model.sID_USER;
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

        public int Update(PhanQuyen_NhomBaiViet_Model model)
        {
            ctx.PHANQUYEN_CHUYENMUC
                .Where(x => x.ID_USER == model.sID_USER)
                .ToList()
                .ForEach(x =>
                {
                    x.TRANG_THAI = 0;
                });

            try
            {
                ctx.SaveChanges();
                return model.sID_USER;
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
    }
}