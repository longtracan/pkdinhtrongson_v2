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

namespace TLBD.Areas.admin.Models.Lien_Ket
{
    public class Lienket_Class
    {
        EntitiesData ctx = new EntitiesData();
        public int GetCount()
        {
            return ctx.LIEN_KET
                .Count(x => x.TRANG_THAI != 99);
        }

        public IList<Lienket_Model> GetList(int page, int pageSize)
        {
            return ctx.LIEN_KET
                .Where(x => x.TRANG_THAI != 99)
                .Select(x => new Lienket_Model
                {
                    sID = x.ID,
                    sTIEU_DE = x.TIEU_DE,
                    sLIEN_KET_HEF = x.LIEN_KET_HEF,
                    sNGAY_TAO = x.NGAY_TAO,
                    sTRANG_THAI = x.TRANG_THAI,
                    sIMG_URL = x.IMG_URL,
                    sHIENTHI_PHAI = x.HIENTHI_PHAI,
                    sHIENTHI_TRAI = x.HIENTHI_TRAI,
                    sTHUTU_PHAI = x.THUTU_PHAI
                })
                .OrderBy(x => x.sID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public IList<Lienket_Model> GetList()
        {
            var result = ctx.sp_LAY_DS_LIEN_KET()
                .Select(x => new Lienket_Model
                {
                    sID = x.ID,
                    sTIEU_DE = x.TIEU_DE,
                    sLIEN_KET_HEF = x.LIEN_KET_HEF,
                    sIMG_URL = x.IMG_URL
                }).ToList();

            return result;
        }

        public static IList<Lienket_Model> List_HinhAnh()
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.LIEN_KET
                .Where(x => x.TRANG_THAI != 99 && x.IMG_URL != null)
                .OrderBy(x => x.ID)
                .Select(x => new Lienket_Model
                {
                    sID = x.ID,
                    sTIEU_DE = x.TIEU_DE,
                    sLIEN_KET_HEF = x.LIEN_KET_HEF
                })
                .ToList();
        }

        public Lienket_Model Get(int id)
        {
            return ctx.LIEN_KET
                .Where(x => x.ID == id)
                .Select(x => new Lienket_Model
                {
                    sID = x.ID,
                    sTIEU_DE = x.TIEU_DE,
                    sIMG_URL = x.IMG_URL,
                    sLIEN_KET_HEF = x.LIEN_KET_HEF,
                    sHIENTHI_PHAI = x.HIENTHI_PHAI,
                    sTHUTU_PHAI = x.THUTU_PHAI,
                    sHIENTHI_TRAI = x.HIENTHI_TRAI,
                    sTHUTU_TRAI = x.THUTU_TRAI
                })
                .FirstOrDefault();
        }

        //public void Approve(IList<int> List)
        //{
        //    //foreach (var id in List)
        //    //{
        //    //    var m = (from x in ctx.LIEN_KETs
        //    //             where x.ID == id
        //    //             select x).FirstOrDefault();
        //    //}

        //    //ctx.SubmitChanges();
        //}

        public int Capnhat(Lienket_Model model)
        {
            var data = ctx.LIEN_KET.Where(x => x.ID == model.sID).FirstOrDefault();

            if (data != null)
            {
                data.TIEU_DE = model.sTIEU_DE;
                data.LIEN_KET_HEF = model.sLIEN_KET_HEF;
                data.IMG_URL = !string.IsNullOrEmpty(model.sIMG_URL) ? model.sIMG_URL : data.IMG_URL;
                data.HIENTHI_PHAI = model.sHIENTHI_PHAI;
                data.THUTU_PHAI = model.sTHUTU_PHAI;
                data.HIENTHI_TRAI = model.sHIENTHI_TRAI;
                data.THUTU_TRAI = model.sTHUTU_TRAI;
                data.NGAY_SUA = model.sNGAY_SUA;
                data.IP_SUA = model.sIP_SUA;
            }
            else
            {
                data = new LIEN_KET()
                {
                    IP_TAO = model.sIP_TAO,
                    NGAY_TAO = model.sNGAY_TAO,
                    TRANG_THAI = model.sTRANG_THAI,
                    TIEU_DE = model.sTIEU_DE,
                    LIEN_KET_HEF = model.sLIEN_KET_HEF,
                    IMG_URL = model.sIMG_URL,
                    HIENTHI_PHAI = model.sHIENTHI_PHAI,
                    THUTU_PHAI = model.sTHUTU_PHAI,
                    HIENTHI_TRAI = model.sHIENTHI_TRAI,
                    THUTU_TRAI = model.sTHUTU_TRAI,
                    NGAY_SUA = model.sNGAY_SUA,
                    IP_SUA = model.sIP_SUA
                };

                ctx.LIEN_KET.Add(data);
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

        public void Delete(Lienket_Model model)
        {
            var exist = ctx.LIEN_KET.Count(x => x.ID == model.sID);

            if (exist > 0)
            {
                ctx.LIEN_KET
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

        public static IList<Lienket_Model> List_QuangCao()
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.LIEN_KET
                .Where(x => x.TRANG_THAI != 99 && x.IMG_URL != null && x.HIENTHI_PHAI == true)
                .OrderBy(x => x.THUTU_PHAI)
                .Select(x => new Lienket_Model
                {
                    sID = x.ID,
                    sTIEU_DE = x.TIEU_DE,
                    sIMG_URL = x.IMG_URL,
                    sLIEN_KET_HEF = x.LIEN_KET_HEF
                })
                .ToList();
        }

        public static IList<Lienket_Model> List_QuangCao_Trai()
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.LIEN_KET
                .Where(x => x.TRANG_THAI != 99 && x.IMG_URL != null && x.HIENTHI_TRAI == true)
                .OrderBy(x => x.THUTU_TRAI)
                .Select(x => new Lienket_Model
                {
                    sID = x.ID,
                    sTIEU_DE = x.TIEU_DE,
                    sIMG_URL = x.IMG_URL,
                    sLIEN_KET_HEF = x.LIEN_KET_HEF
                })
                .ToList();
        }

        public static IList<Lienket_Model> List_Lienket_Phai()
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.LIEN_KET
                .Where(x => x.TRANG_THAI != 99 && x.IMG_URL != null && x.HIENTHI_PHAI == true)
                .OrderBy(x => x.THUTU_TRAI)
                .Select(x => new Lienket_Model
                {
                    sID = x.ID,
                    sTIEU_DE = x.TIEU_DE,
                    sIMG_URL = x.IMG_URL,
                    sLIEN_KET_HEF = x.LIEN_KET_HEF
                })
                .ToList();
        }

        public static IList<Lienket_Model> List_Slider_Home()
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.LIEN_KET
                 .Where(x => x.TRANG_THAI != 99 && x.IMG_URL != null)
                 .Select(x => new Lienket_Model
                 {
                     sID = x.ID,
                     sTIEU_DE = x.TIEU_DE,
                     sIMG_URL = x.IMG_URL,
                     sLIEN_KET_HEF = x.LIEN_KET_HEF
                 })
                 .ToList();
        }
    }
}