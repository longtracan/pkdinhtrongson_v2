using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using TLBD.Areas.admin.Controllers;
using TLBD.Areas.admin.Models.NhomBaiViet;
using TLBD.Models;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Models.CauHoi
{
    public class CauHoi_Class
    {
        EntitiesData ctx = new EntitiesData();
        public int GetCount(string _sDate, string _eDate)
        {
            DateTime sDate = Convert.ToDateTime(DateTime.ParseExact(_sDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
            DateTime eDate = Convert.ToDateTime(DateTime.ParseExact(_eDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)).AddDays(1);

            var _r = ctx.HOI_DAP
                .Count(x => x.TRANG_THAI != 99
                && DateTime.Compare(x.NGAY_DANG, sDate) >= 0
                && DateTime.Compare(x.NGAY_DANG, eDate) < 0);

            return _r;
        }

        public IList<CauHoi_Model> GetList(int page, int pageSize, string _sDate, string _eDate)
        {
            DateTime sDate = Convert.ToDateTime(DateTime.ParseExact(_sDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
            DateTime eDate = Convert.ToDateTime(DateTime.ParseExact(_eDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)).AddDays(1);

            var _r = ctx.HOI_DAP
                .Where(x => x.TRANG_THAI != 99
                && DateTime.Compare(x.NGAY_DANG, sDate) >= 0
                && DateTime.Compare(x.NGAY_DANG, eDate) < 0)
                .Select(x => new CauHoi_Model
                {
                    ID = x.ID,
                    HIEN_THI = x.HIEN_THI,
                    NGUOI_HOI = x.NGUOI_HOI,
                    EMAIL = x.EMAIL,
                    CAU_HOI = x.CAU_HOI,
                    TRA_LOI = x.TRA_LOI,
                    NGAY_DANG = x.NGAY_DANG,
                    DA_TRA_LOI = String.IsNullOrEmpty(x.TRA_LOI) ? false : true
                })
                .OrderByDescending(x => x.NGAY_DANG)
                .Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return _r;
        }

        public CauHoi_Model Get(int id)
        {
            return ctx.HOI_DAP
                .Where(x => x.TRANG_THAI != 99 && x.ID == id)
                .OrderByDescending(x => x.NGAY_DANG)
                .Select(x => new CauHoi_Model
                {
                    ID = x.ID,
                    HIEN_THI = x.HIEN_THI,
                    NGUOI_HOI = x.NGUOI_HOI,
                    EMAIL = x.EMAIL,
                    CAU_HOI = x.CAU_HOI,
                    TRA_LOI = x.TRA_LOI,
                    NGAY_DANG = x.NGAY_DANG,
                    DIA_CHI = x.DIA_CHI,
                    SO_DIEN_THOAI = x.PHONE
                })
                .FirstOrDefault();
        }

        public int CapNhat(CauHoi_Model model)
        {
            var data = ctx.HOI_DAP.Where(x => x.ID == model.ID).FirstOrDefault();

            if (data != null)
            {
                        data.HIEN_THI = model.HIEN_THI;
                        data.TRA_LOI = model.TRA_LOI;
            }
            else
            {
                data = new HOI_DAP()
                {
                    NGAY_DANG = model.NGAY_DANG,
                    HIEN_THI = model.HIEN_THI,
                    NGUOI_HOI = model.NGUOI_HOI,
                    CAU_HOI = model.CAU_HOI,
                    TRA_LOI = model.TRA_LOI,
                    EMAIL = model.EMAIL
                };

                ctx.HOI_DAP.Add(data);
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


        public void Delete(CauHoi_Model model)
        {
            var exist = ctx.HOI_DAP.Count(x => x.ID == model.ID);

            if (exist > 0)
            {
                ctx.HOI_DAP
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

        //public IList<CauHoi_Model> GetListPublic(int page, int pageSize)
        //{
        //    return ctx.HOI_DAP
        //        .Where(x => x.TRANG_THAI != 99 && x.HIEN_THI == true)
        //        .Select(x => new CauHoi_Model
        //        {
        //            ID = x.ID,
        //            HIEN_THI = x.HIEN_THI,
        //            NGUOI_HOI = x.NGUOI_HOI,
        //            CAU_HOI = x.CAU_HOI,
        //            TRA_LOI = x.TRA_LOI,
        //            NGAY_DANG = x.NGAY_DANG
        //        })
        //        .OrderByDescending(x => x.NGAY_DANG)
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();
        //}

    }              
}