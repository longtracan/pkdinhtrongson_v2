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
using TLBD.Areas.admin.Models.PhanQuyen_NhomBaiViet;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Models.ChuyenMuc
{
    public class ChuyenMuc_Class
    {
        static EntitiesData ctx = new EntitiesData();


        public static List<ChuyenMuc_Model> List_Menu_Tren()
        {


            List<ChuyenMuc_Model> query = ctx.CHUYEN_MUC
                .Where(x => x.TRANG_THAI == 0 && x.MENU_TREN == true)
                .OrderBy(x => x.THUTU_TREN).Select(x => new ChuyenMuc_Model

                {
                    ID = x.ID,
                    TEN_CHUYENMUC = x.TEN_CHUYENMUC,
                    LINK = x.LINK
                }).ToList();

            return query;





        }

        public static List<ChuyenMuc_Model> List_Menu_Trai()
        {


            return (from x in ctx.CHUYEN_MUC
                    where x.TRANG_THAI == 0 && x.HIENTHI_TRAI == true
                    orderby x.THUTU_TRAI
                    select new ChuyenMuc_Model
                    {
                        ID = x.ID,
                        TEN_CHUYENMUC = x.TEN_CHUYENMUC,
                        LINK = x.LINK
                    }).ToList();
        }


        public static List<ChuyenMuc_Model> List_ChuyenMuc(int id_user)
        {
            EntitiesData ctx = new EntitiesData();
            List<ChuyenMuc_Model> query = ctx.CHUYEN_MUC
                 .Where(x => x.TRANG_THAI != 99 && x.LINK == null)
                 .OrderBy(z => z.TEN_CHUYENMUC)
                 .Join(ctx.PHANQUYEN_CHUYENMUC.Where(y => y.ID_USER == id_user && y.TRANG_THAI == 1),
                 m => m.ID,
                 n => n.ID_CHUYENMUC,
                 (m, n) => new ChuyenMuc_Model
                 {
                     ID = m.ID,
                     TEN_CHUYENMUC = m.TEN_CHUYENMUC
                 }
                 ).ToList();

            return query;
        }

        public int GetCount()
        {
            return ctx.BANNERs
                .Count(x => x.TRANG_THAI != 99);
        }

        public static int Getcount(int id_chuyenmuc)
        {
            return ctx.BAI_VIET
               .Where(x => x.TRANG_THAI != 99)
               .Join(ctx.NHOM_BAIVIET.Where(o => o.ID != 99),
               y => y.ID_NHOM_BAIVIET,
               z => z.ID,
               (y, z) => new { y, z })
               .Join(ctx.CHUYEN_MUC.Where(i => i.ID != 99 && i.ID == id_chuyenmuc),
               m => m.z.ID_CHUYENMUC,
               n => n.ID,
               (m, n) => new { m, n })

               .ToList().Count;
        }

        public IList<ChuyenMuc_Model> GetList(int page, int pageSize)
        {
            return ctx.CHUYEN_MUC
                .Where(x => x.TRANG_THAI != 99)
                .Select(z => new ChuyenMuc_Model
                {
                    ID = z.ID,
                    TEN_CHUYENMUC = z.TEN_CHUYENMUC,
                    MENU_TREN = z.MENU_TREN,
                    HIENTHI_TRAI = z.HIENTHI_TRAI,
                    HIENTHI_TRANGCHU = z.HIENTHI_TRANGCHU
                })
                .OrderBy(y => y.ID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public ChuyenMuc_Model Get(int id)
        {
            ChuyenMuc_Model query = ctx.CHUYEN_MUC
                .Where(y => y.ID == id)
                .Select(x => new ChuyenMuc_Model
                {
                    ID = x.ID,
                    TEN_CHUYENMUC = x.TEN_CHUYENMUC,
                    MENU_TREN = x.MENU_TREN,
                    THUTU_TREN = x.THUTU_TREN,
                    HIENTHI_TRAI = x.HIENTHI_TRAI,
                    THUTU_TRAI = x.THUTU_TRAI,
                    HIENTHI_TRANGCHU = x.HIENTHI_TRANGCHU,
                    THUTU_TRANGCHU = x.THUTU_TRANGCHU,
                    LINK = x.LINK
                })
                .FirstOrDefault();
            return query;
        }

        public ChuyenMuc_Model Get_NhomBaiViet(int id_nhombaiviet)
        {
            return ctx.NHOM_BAIVIET
                .Where(x => x.ID == id_nhombaiviet)
                .Join(ctx.CHUYEN_MUC,
                y => y.ID_CHUYENMUC,
                z => z.ID,
                (y, z) => new ChuyenMuc_Model
                {
                    ID = z.ID,
                    TEN_CHUYENMUC = z.TEN_CHUYENMUC,
                    MENU_TREN = z.MENU_TREN,
                    THUTU_TREN = z.THUTU_TREN,
                    HIENTHI_TRAI = z.HIENTHI_TRAI,
                    THUTU_TRAI = z.THUTU_TRAI,
                    HIENTHI_TRANGCHU = z.HIENTHI_TRANGCHU,
                    THUTU_TRANGCHU = z.THUTU_TRANGCHU
                }).FirstOrDefault();
        }

        public ChuyenMuc_Model Get_BaiViet(int id_baiviet)
        {
            return ctx.BAI_VIET
                 .Where(x => x.ID == id_baiviet)
                 .Join(ctx.NHOM_BAIVIET,
                 y => y.ID_NHOM_BAIVIET,
                 z => z.ID,
                 (y, z) => new { y, z })
                 .Join(ctx.CHUYEN_MUC,
                 m => m.z.ID_CHUYENMUC,
                 n => n.ID,
                 (m, n) => new ChuyenMuc_Model
                 {
                     ID = n.ID,
                     TEN_CHUYENMUC = n.TEN_CHUYENMUC,
                     MENU_TREN = n.MENU_TREN,
                     THUTU_TREN = n.THUTU_TREN,
                     HIENTHI_TRAI = n.HIENTHI_TRAI,
                     THUTU_TRAI = n.THUTU_TRAI,
                     HIENTHI_TRANGCHU = n.HIENTHI_TRANGCHU,
                     THUTU_TRANGCHU = n.THUTU_TRANGCHU
                 }).FirstOrDefault();
        }



        public int Capnhat(ChuyenMuc_Model model)
        {
            var data = ctx.CHUYEN_MUC.Where(x => x.ID == model.ID).FirstOrDefault();

            if (data != null)
            {
                data.TEN_CHUYENMUC = model.TEN_CHUYENMUC;
                data.MENU_TREN = model.MENU_TREN;
                data.THUTU_TREN = model.THUTU_TREN;
                data.HIENTHI_TRAI = model.HIENTHI_TRAI;
                data.THUTU_TRAI = model.THUTU_TRAI;
                data.HIENTHI_TRANGCHU = model.HIENTHI_TRANGCHU;
                data.THUTU_TRANGCHU = model.THUTU_TRANGCHU;
                data.LINK = model.LINK;
                data.NGAY_SUA = model.NGAY_SUA;
                data.IP_SUA = model.IP_SUA;
            }
            else
            {
                data = new CHUYEN_MUC()
                {
                    IP_TAO = model.IP_TAO,
                    NGAY_TAO = model.NGAY_TAO,
                    TRANG_THAI = model.TRANG_THAI,
                    TEN_CHUYENMUC = model.TEN_CHUYENMUC,
                    MENU_TREN = model.MENU_TREN,
                    THUTU_TREN = model.THUTU_TREN,
                    HIENTHI_TRAI = model.HIENTHI_TRAI,
                    THUTU_TRAI = model.THUTU_TRAI,
                    HIENTHI_TRANGCHU = model.HIENTHI_TRANGCHU,
                    THUTU_TRANGCHU = model.THUTU_TRANGCHU,
                    LINK = model.LINK,
                    NGAY_SUA = model.NGAY_SUA,
                    IP_SUA = model.IP_SUA
                };

                ctx.CHUYEN_MUC.Add(data);
                ctx.SaveChanges();

                ctx.USERDETAILS
                    .ToList()
                    .ForEach(x => ctx.PHANQUYEN_CHUYENMUC.Add(new PHANQUYEN_CHUYENMUC
                    {
                        ID_USER = x.ID,
                        ID_CHUYENMUC = data.ID,
                        TRANG_THAI = x.ID == cHelper.ADMINISTRATOR_ID ? 1 : 0//24 is Administrator
                    }));
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

        public void Delete(ChuyenMuc_Model model)
        {
            var exist = ctx.CHUYEN_MUC.Where(x => x.ID == model.ID).ToList().Count;

            if (exist > 0)
            {
                ctx.CHUYEN_MUC
                    .Where(x => x.ID == model.ID)
                    .ToList()
                    .ForEach(x =>
                    {
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

        public List<ChuyenMuc_Model> List_Menu()
        {
            var result = ctx.sp_LAY_DS_MENU_CHA()
                .Select(z => new ChuyenMuc_Model
                {
                    ID = z.ID,
                    TEN_CHUYENMUC = z.TEN_CHUYENMUC,
                    LINK = z.LINK
                })
                .ToList();

            return result;
        }



        public static List<ChuyenMuc_Model> List_HienThi_TrangChu()
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.CHUYEN_MUC
                .Where(x => x.TRANG_THAI == 0 && x.HIENTHI_TRANGCHU == true)
                .OrderBy(y => y.THUTU_TRANGCHU)
                .Select(z => new ChuyenMuc_Model
                {
                    ID = z.ID,
                    TEN_CHUYENMUC = z.TEN_CHUYENMUC,
                    LINK = z.LINK
                })
                .ToList();
        }

        public static List<ChuyenMuc_Model> List_All_ChuyenMuc()
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.CHUYEN_MUC
                .Where(x => x.TRANG_THAI == 0)
                .Select(z => new ChuyenMuc_Model
                {
                    ID = z.ID,
                    TEN_CHUYENMUC = z.TEN_CHUYENMUC,
                    LINK = z.LINK
                })
                .ToList();
        }
    }
}