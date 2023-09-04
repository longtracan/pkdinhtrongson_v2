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
using TLBD.Areas.admin.Models.NhomBaiViet;
using TLBD.Models;
using TLBD.Models.EntityModel;
using TLBD.Models.LienHe;

namespace TLBD.Areas.admin.Models.LienHe
{
    public class LienHe_Class
    {
        EntitiesData ctx = new EntitiesData();

        public void Add(LienHe_Model model)
        {
            HOI_DAP hoidap = new HOI_DAP()
            {
                NGAY_DANG = model.NgayDang,
                HIEN_THI = false,
                NGUOI_HOI = model.HoTen,
                CAU_HOI = model.NoiDung,
                TRA_LOI = "",
                EMAIL = model.Email,
                DIA_CHI = model.DiaChi,
                PHONE = model.SoDienThoai,
                TRANG_THAI = 0
            };

            ctx.HOI_DAP.Add(hoidap);
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