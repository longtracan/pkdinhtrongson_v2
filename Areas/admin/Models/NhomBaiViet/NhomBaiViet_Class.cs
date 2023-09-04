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

namespace TLBD.Areas.admin.Models.NhomBaiViet
{
    public class NhomBaiViet_Class
    {
        //NhomBaiViet_DBDataContext ctx = new NhomBaiViet_DBDataContext();
        EntitiesData ctx = new EntitiesData();

        public List<NhomBaiViet_Model> LayDanhMucSanPham()
        {
            var result = ctx.sp_LAY_DS_DANHMUC_SANPHAM()
                .Select(x => new NhomBaiViet_Model
                {
                    ID = x.ID,
                    TEN_BAIVIET = x.TEN_BAIVIET
                }).ToList();

            return result;
        }

        public static List<NhomBaiViet_Model> GetList_Nhom_Baiviet()
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.NHOM_BAIVIET
                .Where(x => x.TRANG_THAI == 0)
                .OrderBy(y => y.TEN_BAIVIET)
                .Join(ctx.LOAI_HIENTHI,
                m => m.LOAI_HIENTHI,
                n => n.ID,
                (m, n) => new NhomBaiViet_Model
                {
                    ID = m.ID,
                    LINK = m.LINK,
                    ID_CHUYENMUC = m.ID_CHUYENMUC,
                    TEN_BAIVIET = m.TEN_BAIVIET,
                    LOAI_HIENTHI = m.LOAI_HIENTHI,
                    THUTU = m.THUTU,
                    HIENTHI_BAIVIET_MENU = m.HIENTHI_BAIVIET_MENU
                })
                .ToList();
        }

        public List<NhomBaiViet_Model> List_Nhom_Baiviet_ChuyenMuc(int id_chuyen_muc)
        {
            var result = ctx.sp_LAY_DS_MENU_CON_THEO_MENU_CHA(id_chuyen_muc)
                .Select(m => new NhomBaiViet_Model
                {
                    ID = m.ID,
                    LINK = m.LINK,
                    ICON = m.ICON,
                    IP_XOA = m.IP_XOA,
                    ID_CHUYENMUC = m.ID_CHUYENMUC,
                    TEN_BAIVIET = m.TEN_BAIVIET,
                    LOAI_HIENTHI = m.LOAI_HIENTHI,
                    THUTU = m.THUTU,
                    HIENTHI_BAIVIET_MENU = m.HIENTHI_BAIVIET_MENU
                }).ToList();

            return result;
        }

        public int GetCount(int id_chuyenmuc)
        {
            return ctx.NHOM_BAIVIET
                .Count(x => x.TRANG_THAI != 99 && x.ID_CHUYENMUC == id_chuyenmuc);
        }

        public IList<NhomBaiViet_Model> GetList(int page, int pageSize, int id_chuyenmuc)
        {
            return ctx.NHOM_BAIVIET
                 .Where(x => x.TRANG_THAI != 99 && x.ID_CHUYENMUC == id_chuyenmuc)
                 
                 .Join(ctx.LOAI_HIENTHI,
                 m => m.LOAI_HIENTHI,
                 n => n.ID,
                 (m, n) => new
                 {
                     m,
                     n
                 })
                 .Join(ctx.CHUYEN_MUC,
                 o => o.m.ID_CHUYENMUC,
                 p => p.ID,
                 (o, p) => new NhomBaiViet_Model
                 {
                     ID = o.m.ID,
                     ID_CHUYENMUC = o.m.ID_CHUYENMUC,
                     TEN_CHUYENMUC = p.TEN_CHUYENMUC,
                     TEN_BAIVIET = o.m.TEN_BAIVIET,
                     LOAI_HIENTHI = o.m.LOAI_HIENTHI,
                     THUTU = o.m.THUTU,
                     HIENTHI_BAIVIET_MENU = o.m.HIENTHI_BAIVIET_MENU
                 })
                 .OrderBy(y => y.THUTU)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

        }

        public List<NhomBaiViet_Model> GetList(int id_chuyenmuc)
        {
            return ctx.NHOM_BAIVIET
                 .Where(x => x.TRANG_THAI != 99 && x.ID_CHUYENMUC == id_chuyenmuc)

                 .Join(ctx.LOAI_HIENTHI,
                 m => m.LOAI_HIENTHI,
                 n => n.ID,
                 (m, n) => new
                 {
                     m,
                     n
                 })
                 .Join(ctx.CHUYEN_MUC,
                 o => o.m.ID_CHUYENMUC,
                 p => p.ID,
                 (o, p) => new NhomBaiViet_Model
                 {
                     ID = o.m.ID,
                     ID_CHUYENMUC = o.m.ID_CHUYENMUC,
                     TEN_CHUYENMUC = p.TEN_CHUYENMUC,
                     TEN_BAIVIET = o.m.TEN_BAIVIET,
                     LOAI_HIENTHI = o.m.LOAI_HIENTHI,
                     THUTU = o.m.THUTU,
                     HIENTHI_BAIVIET_MENU = o.m.HIENTHI_BAIVIET_MENU
                 })
                 .OrderBy(y => y.THUTU)
                 .ToList();
        }

        public NhomBaiViet_Model Get(int id)
        {
            var result = ctx.NHOM_BAIVIET
                 .Where(x => x.TRANG_THAI != 99 && x.ID == id)
                 .Join(ctx.LOAI_HIENTHI,
                 m => m.LOAI_HIENTHI,
                 n => n.ID,
                 (m, n) => new NhomBaiViet_Model
                 {
                     ID = m.ID,
                     LINK = m.LINK,
                     ICON = m.ICON,
                     IP_XOA = m.IP_XOA, // Tóm tắt NBV lưu trong cột này
                     ID_CHUYENMUC = m.ID_CHUYENMUC,
                     TEN_BAIVIET = m.TEN_BAIVIET,
                     LOAI_HIENTHI = m.LOAI_HIENTHI,
                     THUTU = m.THUTU,
                     HIENTHI_BAIVIET_MENU = m.HIENTHI_BAIVIET_MENU
                     
                 })
                 .FirstOrDefault();

            return result;
        }

        public NhomBaiViet_Model Get1(int id)
        {
            return ctx.NHOM_BAIVIET
                 .Where(x => x.TRANG_THAI != 99 && x.ID_CHUYENMUC == id)
                .Join(ctx.CHUYEN_MUC,
                m=>m.ID_CHUYENMUC,
                n=>n.ID,
                
                 (m, n) => new NhomBaiViet_Model
                 {
                     ID = m.ID,
                     LINK = m.LINK,
                     ID_CHUYENMUC = m.ID_CHUYENMUC,
                     TEN_BAIVIET = m.TEN_BAIVIET,
                     LOAI_HIENTHI = m.LOAI_HIENTHI,
                     THUTU = m.THUTU,
                     HIENTHI_BAIVIET_MENU = m.HIENTHI_BAIVIET_MENU

                 })
                 .FirstOrDefault();
        }

        public NhomBaiViet_Model Get_BaiViet(int id_baiviet)
        {
            return ctx.BAI_VIET
                .Where(x => x.ID == id_baiviet)
                .Join(ctx.NHOM_BAIVIET,
                n => n.ID_NHOM_BAIVIET,
                m => m.ID,
                (n, m) => new { n, m })
                .Join(ctx.LOAI_HIENTHI,
                o => o.m.LOAI_HIENTHI,
                p => p.ID,
                (o, p) => new NhomBaiViet_Model
                {
                    ID = o.m.ID,
                    ID_CHUYENMUC = o.m.ID_CHUYENMUC,
                    TEN_BAIVIET = o.m.TEN_BAIVIET,
                    LOAI_HIENTHI = o.m.LOAI_HIENTHI,
                    THUTU = o.m.THUTU,
                    HIENTHI_BAIVIET_MENU = o.m.HIENTHI_BAIVIET_MENU,
                    SANPHAM = ctx.CHUYEN_MUC.Count(x => x.ID == o.m.ID_CHUYENMUC && x.HIENTHI_TRANGCHU == true) > 0,
                })
                 .FirstOrDefault();
        }

        //public void Approve(IList<int> List_nbv)
        //{
        //    //foreach (var id in List_nbv)
        //    //{
        //    //    var m = (from x in ctx.NHOM_BAIVIETs
        //    //             where x.ID == id
        //    //             select x).FirstOrDefault();
        //    //}

        //    //ctx.SubmitChanges();
        //}

        public int Capnhat(NhomBaiViet_Model model)
        {
            var data = ctx.NHOM_BAIVIET
                .Where(x => x.ID == model.ID)
                .FirstOrDefault();

            if (data != null)
            {
                   data.ID_CHUYENMUC = model.ID_CHUYENMUC;
                   data.TEN_BAIVIET = model.TEN_BAIVIET;
                   data.LOAI_HIENTHI = model.LOAI_HIENTHI;
                   data.NGAY_SUA = model.NGAY_SUA;
                   data.IP_SUA = model.IP_SUA;
                   data.THUTU = model.THUTU;
                   data.LINK = model.LINK;
                   data.ICON = model.ICON;
                   data.IP_XOA = model.IP_XOA; // lấy icon
            }
            else
            {
                data = new NHOM_BAIVIET()
                {
                    IP_TAO = model.IP_TAO,
                    NGAY_TAO = model.NGAY_TAO,
                    TRANG_THAI = model.TRANG_THAI,
                    ID_CHUYENMUC = model.ID_CHUYENMUC,
                    TEN_BAIVIET = model.TEN_BAIVIET,
                    LOAI_HIENTHI = model.LOAI_HIENTHI,
                    NGAY_SUA = model.NGAY_SUA,
                    IP_SUA = model.IP_SUA,
                    THUTU = model.THUTU,
                    LINK = model.LINK,
                    ICON = model.ICON,
                    IP_XOA = model.IP_XOA //// lấy icon
                };
                ctx.NHOM_BAIVIET.Add(data);
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

        public void Delete(NhomBaiViet_Model model)
        {
            var exist = ctx.NHOM_BAIVIET.Where(x => x.ID == model.ID).ToList().Count;

            if (exist > 0)
            {
                ctx.NHOM_BAIVIET
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

        public static int GetLoaiHienThi(int id)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.NHOM_BAIVIET
                .Where(x => x.ID == id)
                .Select(y => new
                {
                    LOAI_HIENTHI = y.LOAI_HIENTHI
                }).FirstOrDefault().LOAI_HIENTHI;
        }

        public static List<NhomBaiViet_Model> List_Nhom_BaiViet(int id)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.NHOM_BAIVIET
                .Where(x => x.TRANG_THAI != 99)
                .Join(ctx.CHUYEN_MUC.Where(y => y.TRANG_THAI != 99 && y.ID == id),
                m => m.ID_CHUYENMUC,
                n => n.ID,
                (m, n) => new NhomBaiViet_Model
                {
                    ID = m.ID,
                    TEN_BAIVIET = m.TEN_BAIVIET,
                    THUTU = m.THUTU,
                    LINK = m.LINK,
                    HIENTHI_BAIVIET_MENU = m.HIENTHI_BAIVIET_MENU
                }).ToList();
        }

        public static List<NhomBaiViet_Model> List_NhomBaiViet_TrangChu()
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.NHOM_BAIVIET
                .Where(x => x.TRANG_THAI != 99)
                .Join(ctx.CHUYEN_MUC.Where(y => y.TRANG_THAI != 99 && y.HIENTHI_TRANGCHU == true),
                m => m.ID_CHUYENMUC,
                n => n.ID,
                (m, n) => new NhomBaiViet_Model
                {
                    ID = m.ID,
                    TEN_BAIVIET = m.TEN_BAIVIET,
                    THUTU = m.THUTU,
                    LINK = m.LINK,
                    HIENTHI_BAIVIET_MENU = m.HIENTHI_BAIVIET_MENU
                })
                .ToList();
        }
    }

}