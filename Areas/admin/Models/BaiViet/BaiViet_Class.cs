using Entities.Model;
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
using TLBD.Models;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Models.BaiViet
{
    public class BaiViet_Class
    {
        static EntitiesData ctx = new EntitiesData();
        public IList<Comment_Model> GetListComment(int id)
        {
            var result = ctx.sp_LAY_DS_COMMENT(id)
                .Select(x => new Comment_Model
                  {
                      Comment = x.Comment,
                      Date = x.Date,
                      Name = x.Name
                  }).ToList();

            return result;
        }

        public int GetCount(int id_nhom_baiviet)
        {
            return ctx.BAI_VIET
                .Where(x => x.TRANG_THAI != 99 && x.ID_NHOM_BAIVIET == id_nhom_baiviet)
                .ToList().Count;
        }


        public int GetCount1(int id_chuyenmuc)
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

        public IList<BaiViet_Model> GetAllTinTuc()
        {
            var result = ctx.BAI_VIET.Where(x => x.TRANG_THAI == 0)
                .Join(ctx.NHOM_BAIVIET.Where(x => x.TRANG_THAI == 0)
                , x => x.ID_NHOM_BAIVIET
                , y => y.ID
                , (x, y) => new
                {
                    ID = x.ID,
                    TIEU_DE = x.TIEU_DE,
                    IMAGE_URL = x.IMAGE_URL,
                    ID_CHUYENMUC = y.ID_CHUYENMUC
                })
                .Join(ctx.CHUYEN_MUC.Where(x => x.TRANG_THAI == 0 && x.HIENTHI_TRANGCHU == false)
                , x => x.ID_CHUYENMUC
                , y => y.ID
                , (x, y) => new BaiViet_Model
                {
                    ID = x.ID,
                    TIEU_DE = x.TIEU_DE,
                    IMAGE_URL = x.IMAGE_URL
                }).ToList();

            return result;
        }

        public IList<BaiViet_Model> GetList(int page, int pageSize, int id_nhom_baiviet)
        {
            var _r = ctx.BAI_VIET
                .Where(x => x.TRANG_THAI != 99 && x.ID_NHOM_BAIVIET == id_nhom_baiviet)

                .Join(ctx.NHOM_BAIVIET,
                y => y.ID_NHOM_BAIVIET,
                z => z.ID,
                (y, z) => new
                {
                    y,
                    z
                })
                .Join(ctx.NGUOI_VIETBAI,
                m => m.y.ID_NGUOIVIETBAI,
                n => n.ID,
                (m, n) => new BaiViet_Model
                {
                    ID = m.y.ID,
                    ID_NHOM_BAIVIET = m.y.ID_NHOM_BAIVIET,
                    TIEU_DE = m.y.TIEU_DE,
                    NOI_BAT = m.y.NOI_BAT,
                    TOM_TAT = m.y.TOM_TAT,
                    NOI_DUNG = m.y.NOI_DUNG,
                    IMAGE_URL = m.y.IMAGE_URL,
                    TEN_BAIVIET = m.z.TEN_BAIVIET,
                    NGAY_DANG = m.y.NGAY_DANG,

                    //Noi dung ve san pham
                    SP_GIA = m.y.SP_GIA,
                    SP_GIAKM = m.y.SP_GIAKM,
                    SP_BAOHANH = m.y.SP_BAOHANH,
                    SP_IMGCHITIET = m.y.SP_IMGCHITIET,
                    SP_THONGSO = m.y.SP_THONGSO,
                    SP_NHASANXUAT = m.y.SP_NHASANXUAT,
                    SP_SOLUONG = 9999,
                    //SP_SOLUONG = m.y.SP_SOLUONG,

                    HOT = m.y.HOT,
                    GIAMGIA = m.y.GIAMGIA,
                    DANHGIACAO = m.y.DANHGIACAO,
                    MATHANG_PHOBIEN = m.y.MATHANG_PHOBIEN,
                    BANCHAYNHAT = m.y.BANCHAYNHAT,

                })
                .OrderByDescending(y => y.NGAY_DANG)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _r;
        }


        public List<BaiViet_Model> GetList(int id_nhom_baiviet) // get list theo ngày đăng
        {
            var _r = ctx.BAI_VIET
                .Where(x => x.TRANG_THAI != 99 && x.ID_NHOM_BAIVIET == id_nhom_baiviet)

                .Join(ctx.NHOM_BAIVIET,
                y => y.ID_NHOM_BAIVIET,
                z => z.ID,
                (y, z) => new
                {
                    y,
                    z
                })
                .Join(ctx.NGUOI_VIETBAI,
                m => m.y.ID_NGUOIVIETBAI,
                n => n.ID,
                (m, n) => new BaiViet_Model
                {
                    ID = m.y.ID,
                    ID_NHOM_BAIVIET = m.y.ID_NHOM_BAIVIET,
                    TIEU_DE = m.y.TIEU_DE,
                    NOI_BAT = m.y.NOI_BAT,
                    TOM_TAT = m.y.TOM_TAT,
                    NOI_DUNG = m.y.NOI_DUNG,
                    IMAGE_URL = m.y.IMAGE_URL,
                    TEN_BAIVIET = m.z.TEN_BAIVIET,
                    NGAY_DANG = m.y.NGAY_DANG,

                    //Noi dung ve san pham
                    SP_GIA = m.y.SP_GIA,
                    SP_GIAKM = m.y.SP_GIAKM,
                    SP_BAOHANH = m.y.SP_BAOHANH,
                    SP_IMGCHITIET = m.y.SP_IMGCHITIET,
                    SP_THONGSO = m.y.SP_THONGSO,
                    SP_NHASANXUAT = m.y.SP_NHASANXUAT,
                    SP_SOLUONG = 9999,
                    //SP_SOLUONG = m.y.SP_SOLUONG,

                    HOT = m.y.HOT,
                    GIAMGIA = m.y.GIAMGIA,
                    DANHGIACAO = m.y.DANHGIACAO,
                    MATHANG_PHOBIEN = m.y.MATHANG_PHOBIEN,
                    BANCHAYNHAT = m.y.BANCHAYNHAT,

                })
                .OrderByDescending(y => y.NGAY_DANG)
                .ToList();

            return _r;
        }

        public List<BaiViet_Model> GetList_tt(int id_nhom_baiviet) // get list theo thứ tự ( thứ tự lưu trong cột SP_BAOHANH)
        {
            var _r = ctx.BAI_VIET
                .Where(x => x.TRANG_THAI != 99 && x.ID_NHOM_BAIVIET == id_nhom_baiviet)

                .Join(ctx.NHOM_BAIVIET,
                y => y.ID_NHOM_BAIVIET,
                z => z.ID,
                (y, z) => new
                {
                    y,
                    z
                })
                .Join(ctx.NGUOI_VIETBAI,
                m => m.y.ID_NGUOIVIETBAI,
                n => n.ID,
                (m, n) => new BaiViet_Model
                {
                    ID = m.y.ID,
                    ID_NHOM_BAIVIET = m.y.ID_NHOM_BAIVIET,
                    TIEU_DE = m.y.TIEU_DE,
                    NOI_BAT = m.y.NOI_BAT,
                    TOM_TAT = m.y.TOM_TAT,
                    NOI_DUNG = m.y.NOI_DUNG,
                    IMAGE_URL = m.y.IMAGE_URL,
                    TEN_BAIVIET = m.z.TEN_BAIVIET,
                    NGAY_DANG = m.y.NGAY_DANG,

                    //Noi dung ve san pham
                    SP_GIA = m.y.SP_GIA,
                    SP_GIAKM = m.y.SP_GIAKM,
                    SP_BAOHANH = m.y.SP_BAOHANH,
                    SP_IMGCHITIET = m.y.SP_IMGCHITIET,
                    SP_THONGSO = m.y.SP_THONGSO,
                    SP_NHASANXUAT = m.y.SP_NHASANXUAT,
                    SP_SOLUONG = 9999,
                    //SP_SOLUONG = m.y.SP_SOLUONG,

                    HOT = m.y.HOT,
                    GIAMGIA = m.y.GIAMGIA,
                    DANHGIACAO = m.y.DANHGIACAO,
                    MATHANG_PHOBIEN = m.y.MATHANG_PHOBIEN,
                    BANCHAYNHAT = m.y.BANCHAYNHAT,

                })
                .OrderBy(y => y.SP_BAOHANH)
                .ToList();

            return _r;
        }

        public List<BaiViet_Model> GetList_noibat_tt(int id_nhom_baiviet) // get list noi bat theo thứ tự ( thứ tự lưu trong cột SP_BAOHANH)
        {
            var _r = ctx.BAI_VIET
                .Where(x => x.TRANG_THAI != 99 && x.ID_NHOM_BAIVIET == id_nhom_baiviet && x.NOI_BAT == true)

                .Join(ctx.NHOM_BAIVIET,
                y => y.ID_NHOM_BAIVIET,
                z => z.ID,
                (y, z) => new
                {
                    y,
                    z
                })
                .Join(ctx.NGUOI_VIETBAI,
                m => m.y.ID_NGUOIVIETBAI,
                n => n.ID,
                (m, n) => new BaiViet_Model
                {
                    ID = m.y.ID,
                    ID_NHOM_BAIVIET = m.y.ID_NHOM_BAIVIET,
                    TIEU_DE = m.y.TIEU_DE,
                    NOI_BAT = m.y.NOI_BAT,
                    TOM_TAT = m.y.TOM_TAT,
                    NOI_DUNG = m.y.NOI_DUNG,
                    IMAGE_URL = m.y.IMAGE_URL,
                    TEN_BAIVIET = m.z.TEN_BAIVIET,
                    NGAY_DANG = m.y.NGAY_DANG,

                    //Noi dung ve san pham
                    SP_GIA = m.y.SP_GIA,
                    SP_GIAKM = m.y.SP_GIAKM,
                    SP_BAOHANH = m.y.SP_BAOHANH,
                    SP_IMGCHITIET = m.y.SP_IMGCHITIET,
                    SP_THONGSO = m.y.SP_THONGSO,
                    SP_NHASANXUAT = m.y.SP_NHASANXUAT,
                    SP_SOLUONG = 9999,
                    //SP_SOLUONG = m.y.SP_SOLUONG,

                    HOT = m.y.HOT,
                    GIAMGIA = m.y.GIAMGIA,
                    DANHGIACAO = m.y.DANHGIACAO,
                    MATHANG_PHOBIEN = m.y.MATHANG_PHOBIEN,
                    BANCHAYNHAT = m.y.BANCHAYNHAT,

                })
                .OrderBy(y => y.SP_BAOHANH)
                .ToList();

            return _r;
        }


        public static List<BaiViet_Model> List_BV_Banner11()
        {
            var _r = ctx.BAI_VIET
                .Where(x => x.TRANG_THAI != 99 && x.SP_SETUP == 11)

                .Join(ctx.NHOM_BAIVIET,
                y => y.ID_NHOM_BAIVIET,
                z => z.ID,
                (y, z) => new
                {
                    y,
                    z
                })
                .Join(ctx.NGUOI_VIETBAI,
                m => m.y.ID_NGUOIVIETBAI,
                n => n.ID,
                (m, n) => new BaiViet_Model
                {
                    ID = m.y.ID,
                    ID_NHOM_BAIVIET = m.y.ID_NHOM_BAIVIET,
                    TIEU_DE = m.y.TIEU_DE,
                    NOI_BAT = m.y.NOI_BAT,
                    TOM_TAT = m.y.TOM_TAT,
                    NOI_DUNG = m.y.NOI_DUNG,
                    IMAGE_URL = m.y.IMAGE_URL,
                    TEN_BAIVIET = m.z.TEN_BAIVIET,
                    NGAY_DANG = m.y.NGAY_DANG,

                    //Noi dung ve san pham
                    SP_GIA = m.y.SP_GIA,
                    SP_GIAKM = m.y.SP_GIAKM,
                    SP_BAOHANH = m.y.SP_BAOHANH,
                    SP_IMGCHITIET = m.y.SP_IMGCHITIET,
                    SP_THONGSO = m.y.SP_THONGSO,
                    SP_NHASANXUAT = m.y.SP_NHASANXUAT,
                    //SP_SOLUONG = m.y.SP_SOLUONG,
                    SP_SOLUONG = 9999,
                    HOT = m.y.HOT,
                    GIAMGIA = m.y.GIAMGIA,
                    DANHGIACAO = m.y.DANHGIACAO,
                    MATHANG_PHOBIEN = m.y.MATHANG_PHOBIEN,
                    BANCHAYNHAT = m.y.BANCHAYNHAT,
                })
                .OrderByDescending(y => y.NGAY_DANG)

                .ToList();

            return _r;
        }

        public BaiViet_Model Get(int id)
        {
            var _r = ctx.BAI_VIET
                .Where(bv => bv.TRANG_THAI != 99 && bv.ID == id)
                .OrderByDescending(bv => bv.NGAY_DANG)
                .Join(ctx.NHOM_BAIVIET.Where(nbv => nbv.TRANG_THAI != 99),
                bv => bv.ID_NHOM_BAIVIET,
                nbv => nbv.ID,
                (bv, nbv) => new { bv, nbv })
                .Join(ctx.NGUOI_VIETBAI,
                bv_nbv => bv_nbv.bv.ID_NGUOIVIETBAI,
                nvb => nvb.ID,
                (bv_nbv, nvb) => new BaiViet_Model
                {
                    ID = bv_nbv.bv.ID,
                    TIEU_DE = bv_nbv.bv.TIEU_DE,
                    NOI_BAT = bv_nbv.bv.NOI_BAT,
                    NGAY_DANG = bv_nbv.bv.NGAY_DANG,
                    ID_NHOM_BAIVIET = bv_nbv.bv.ID_NHOM_BAIVIET,
                    TOM_TAT = bv_nbv.bv.TOM_TAT,
                    NOI_DUNG = bv_nbv.bv.NOI_DUNG,
                    IMAGE_URL = bv_nbv.bv.IMAGE_URL,
                    FILE_DOWNLOAD = bv_nbv.bv.FILE_DOWNLOAD,
                    NGAY_TAO = bv_nbv.bv.NGAY_TAO,
                    TEN_BAIVIET = bv_nbv.nbv.TEN_BAIVIET,
                    LOAI_HIENTHI = bv_nbv.nbv.LOAI_HIENTHI,
                    ID_NGUOIVIETBAI = bv_nbv.bv.ID_NGUOIVIETBAI,
                    ID_CHUYENMUC = bv_nbv.nbv.ID_CHUYENMUC,
                    

                    //Noi dung ve san pham
                    SP_GIA = bv_nbv.bv.SP_GIA,
                    SP_GIAKM = bv_nbv.bv.SP_GIAKM,
                    SP_BAOHANH = bv_nbv.bv.SP_BAOHANH,
                    SP_IMGCHITIET = bv_nbv.bv.SP_IMGCHITIET,
                    SP_THONGSO = bv_nbv.bv.SP_THONGSO,
                    SP_NHASANXUAT = bv_nbv.bv.SP_NHASANXUAT,
                    SANPHAM = ctx.CHUYEN_MUC.Count(x => x.ID == bv_nbv.nbv.ID_CHUYENMUC && x.HIENTHI_TRANGCHU == true) > 0,
                    //SP_SOLUONG = bv_nbv.bv.SP_SOLUONG,
                    SP_SOLUONG = 9999,

                    HOT = bv_nbv.bv.HOT,
                    GIAMGIA = bv_nbv.bv.GIAMGIA,
                    DANHGIACAO = bv_nbv.bv.DANHGIACAO,
                    MATHANG_PHOBIEN = bv_nbv.bv.MATHANG_PHOBIEN,
                    BANCHAYNHAT = bv_nbv.bv.BANCHAYNHAT,
                    SP_SETUP =bv_nbv.bv.SP_SETUP
                    //sTHOIGIAN_KM = bv_nbv.bv.THOIGIANKM


                }).FirstOrDefault();

            return _r;
        }

      

        public List<BaiViet_Model> LayDSSanPham()
        {
            var result = ctx.sp_LAY_DS_SAN_PHAM()
                .Select(x => new BaiViet_Model
                 {
                     ID = x.ID,
                     IMAGE_URL = x.IMAGE_URL,
                     TIEU_DE = x.TIEU_DE,
                     SP_GIA = x.SP_GIA,
                     SP_GIAKM = x.SP_GIAKM,
                     SP_SOLUONG = 9999
                     //SP_SOLUONG = x.SP_SOLUONG

                 }).Take(500).ToList();

            return result;
        }

        public int CapNhat(BaiViet_Model model)
        {
            var data = ctx.BAI_VIET
                .Where(x => x.ID == model.ID).FirstOrDefault();

            if (data != null)
            {
                data.NGAY_DANG = model.NGAY_DANG;
                data.TIEU_DE = model.TIEU_DE;
                data.NOI_BAT = model.NOI_BAT;
                data.TOM_TAT = model.TOM_TAT;
                data.NOI_DUNG = model.NOI_DUNG;
                data.ID_NHOM_BAIVIET = model.ID_NHOM_BAIVIET;
                if (!string.IsNullOrEmpty(model.IMAGE_URL))
                {
                    data.IMAGE_URL = model.IMAGE_URL;
                }
                if (!string.IsNullOrEmpty(model.FILE_DOWNLOAD))
                {
                    data.FILE_DOWNLOAD = model.FILE_DOWNLOAD;
                }
                data.ID_NGUOIVIETBAI = model.ID_NGUOIVIETBAI;
                data.IP_SUA = model.IP_SUA;
                data.NGAY_SUA = model.NGAY_SUA;

                data.HOT = model.HOT;
                data.GIAMGIA = model.GIAMGIA;
                data.DANHGIACAO = model.DANHGIACAO;
                data.MATHANG_PHOBIEN = model.MATHANG_PHOBIEN;
                data.BANCHAYNHAT = model.BANCHAYNHAT;
                //data.THOIGIANKM = model.sTHOIGIAN_KM;

                //====
                if (!string.IsNullOrEmpty(model.SP_IMGCHITIET))
                {
                    data.SP_IMGCHITIET = model.SP_IMGCHITIET;
                }
                data.SP_GIA = model.SP_GIA;
                data.SP_GIAKM = model.SP_GIAKM;
                data.SP_NHASANXUAT = model.SP_NHASANXUAT;
                data.SP_BAOHANH = model.SP_BAOHANH;
                //data.SP_SOLUONG = model.SP_SOLUONG;
                data.SP_SOLUONG = 9999;
                data.SP_THONGSO = model.SP_THONGSO;
                data.SP_SETUP = model.SP_SETUP;
            }
            else
            {
                data = new BAI_VIET()
                {
                    IP_TAO = model.IP_TAO,
                    NGAY_TAO = model.NGAY_TAO,
                    NGAY_DANG = model.NGAY_DANG,
                    TIEU_DE = model.TIEU_DE,
                    NOI_BAT = model.NOI_BAT,
                    TOM_TAT = model.TOM_TAT,
                    NOI_DUNG = model.NOI_DUNG,
                    ID_NHOM_BAIVIET = model.ID_NHOM_BAIVIET,
                    IMAGE_URL = !string.IsNullOrEmpty(model.IMAGE_URL) ? model.IMAGE_URL : "",
                    FILE_DOWNLOAD = !string.IsNullOrEmpty(model.FILE_DOWNLOAD) ? model.FILE_DOWNLOAD : "",
                    ID_NGUOIVIETBAI = model.ID_NGUOIVIETBAI,
                    IP_SUA = model.IP_SUA,
                    NGAY_SUA = model.NGAY_SUA,

                    //===
                    SP_IMGCHITIET = !string.IsNullOrEmpty(model.SP_IMGCHITIET) ? model.SP_IMGCHITIET : "",
                    SP_GIA = model.SP_GIA,
                    SP_GIAKM = model.SP_GIAKM,
                    SP_NHASANXUAT = model.SP_NHASANXUAT,
                    SP_BAOHANH = model.SP_BAOHANH,
                    SP_SOLUONG = 9999,
                    //SP_SOLUONG = model.SP_SOLUONG,

                    HOT = model.HOT,
                    GIAMGIA = model.GIAMGIA,
                    DANHGIACAO = model.DANHGIACAO,
                    MATHANG_PHOBIEN = model.MATHANG_PHOBIEN,
                    BANCHAYNHAT = model.BANCHAYNHAT,
                    SP_SETUP = model.SP_SETUP
                    //THOIGIANKM = model.sTHOIGIAN_KM,

                };

                ctx.BAI_VIET.Add(data);
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

        public void Delete(BaiViet_Model model)
        {
            var exist = ctx.BAI_VIET.Count(x => x.ID == model.ID);

            if (exist > 0)
            {
                ctx.BAI_VIET
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

        public BaiViet_Model Get_BaiViet(int id)
        {
            return ctx.BAI_VIET
                .Where(x => x.ID_NHOM_BAIVIET == id && x.TRANG_THAI != 99)
                .OrderByDescending(x => x.NGAY_DANG)
                .Join(ctx.NHOM_BAIVIET.Where(x => x.TRANG_THAI != 99),
                x => x.ID_NHOM_BAIVIET,
                y => y.ID,
                (x, y) => new BaiViet_Model
                {
                    ID = x.ID,
                    TIEU_DE = x.TIEU_DE,
                    ID_NHOM_BAIVIET = x.ID_NHOM_BAIVIET,
                    TOM_TAT = x.TOM_TAT,
                    NOI_DUNG = x.NOI_DUNG,
                    IMAGE_URL = x.IMAGE_URL,
                    FILE_DOWNLOAD = x.FILE_DOWNLOAD,
                    NGAY_TAO = x.NGAY_TAO,
                    TEN_BAIVIET = y.TEN_BAIVIET,

                    //Noi dung ve san pham
                    SP_GIA = x.SP_GIA,
                    SP_GIAKM = x.SP_GIAKM,
                    SP_BAOHANH = x.SP_BAOHANH,
                    SP_IMGCHITIET = x.SP_IMGCHITIET,
                    SP_THONGSO = x.SP_THONGSO,
                    SP_NHASANXUAT = x.SP_NHASANXUAT,
                    //SP_SOLUONG = x.SP_SOLUONG,
                    SP_SOLUONG = 9999,

                    HOT = x.HOT,
                    GIAMGIA = x.GIAMGIA,
                    DANHGIACAO = x.DANHGIACAO,
                    MATHANG_PHOBIEN = x.MATHANG_PHOBIEN,
                    BANCHAYNHAT = x.BANCHAYNHAT,
                }).FirstOrDefault();
        }

        public BaiViet_Model Get_BaiViet_idlbum(int id)
        {
            return ctx.BAI_VIET
                .Where(x => x.SP_SETUP == id && x.TRANG_THAI != 99)
                .OrderByDescending(x => x.NGAY_DANG)
                .Join(ctx.NHOM_BAIVIET.Where(x => x.TRANG_THAI != 99),
                x => x.ID_NHOM_BAIVIET,
                y => y.ID,
                (x, y) => new BaiViet_Model
                {
                    ID = x.ID,
                    TIEU_DE = x.TIEU_DE,
                    ID_NHOM_BAIVIET = x.ID_NHOM_BAIVIET,
                    TOM_TAT = x.TOM_TAT,
                    NOI_DUNG = x.NOI_DUNG,
                    IMAGE_URL = x.IMAGE_URL,
                    FILE_DOWNLOAD = x.FILE_DOWNLOAD,
                    NGAY_TAO = x.NGAY_TAO,
                    TEN_BAIVIET = y.TEN_BAIVIET,

                    //Noi dung ve san pham
                    SP_GIA = x.SP_GIA,
                    SP_GIAKM = x.SP_GIAKM,
                    SP_BAOHANH = x.SP_BAOHANH,
                    SP_IMGCHITIET = x.SP_IMGCHITIET,
                    SP_THONGSO = x.SP_THONGSO,
                    SP_NHASANXUAT = x.SP_NHASANXUAT,
                    //SP_SOLUONG = x.SP_SOLUONG,
                    SP_SOLUONG = 9999,

                    HOT = x.HOT,
                    GIAMGIA = x.GIAMGIA,
                    DANHGIACAO = x.DANHGIACAO,
                    MATHANG_PHOBIEN = x.MATHANG_PHOBIEN,
                    BANCHAYNHAT = x.BANCHAYNHAT,
                }).FirstOrDefault();
        }

        public  List<BaiViet_Model> List_Baiviet(int id)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.BAI_VIET
                .Where(x => x.TRANG_THAI != 99)
                .OrderByDescending(x => x.NGAY_DANG)
                .Join(ctx.NHOM_BAIVIET.Where(x => x.TRANG_THAI != 99 && x.ID == id),
                x => x.ID_NHOM_BAIVIET,
                y => y.ID,
                (x, y) => new BaiViet_Model
                {
                    ID = x.ID,
                    TIEU_DE = x.TIEU_DE,
                    NOI_BAT = x.NOI_BAT,
                    NGAY_DANG = x.NGAY_DANG,
                    ID_NHOM_BAIVIET = x.ID_NHOM_BAIVIET,
                    TOM_TAT = x.TOM_TAT,
                    NOI_DUNG = x.NOI_DUNG,
                    IMAGE_URL = x.IMAGE_URL,
                    TEN_BAIVIET = y.TEN_BAIVIET
                })
                .ToList();
        }


        public List<BaiViet_Model> List_TinDaDua(int id_nhom, int id)
        {
            var result = ctx.sp_LAY_DS_LIEN_QUAN(id_nhom, id)
                .Select(x => new BaiViet_Model
                {
                    ID = x.ID,
                    TIEU_DE = x.TIEU_DE,
                    ID_NHOM_BAIVIET = x.ID_NHOM_BAIVIET,
                    TOM_TAT = x.TOM_TAT,
                    NOI_DUNG = x.NOI_DUNG,
                    IMAGE_URL = x.IMAGE_URL
                }).ToList();

            return result;
        }

        public List<BaiViet_Model> List_index(int trang, int kichthuoc, int id)
        {
            return ctx.BAI_VIET
                .Where(x => x.TRANG_THAI != 99 && x.ID_NHOM_BAIVIET == id)

                .Join(ctx.NHOM_BAIVIET.Where(x => x.TRANG_THAI != 99),
                x => x.ID_NHOM_BAIVIET,
                y => y.ID,
                (x, y) => new BaiViet_Model
                {
                    ID = x.ID,
                    TIEU_DE = x.TIEU_DE,
                    ID_NHOM_BAIVIET = x.ID_NHOM_BAIVIET,
                    TOM_TAT = x.TOM_TAT,
                    NOI_DUNG = x.NOI_DUNG,
                    IMAGE_URL = x.IMAGE_URL,
                    NGAY_DANG = x.NGAY_DANG,
                    TEN_BAIVIET = y.TEN_BAIVIET,

                    //Noi dung ve san pham
                    SP_GIA = x.SP_GIA,
                    SP_GIAKM = x.SP_GIAKM,
                    SP_BAOHANH = x.SP_BAOHANH,
                    SP_IMGCHITIET = x.SP_IMGCHITIET,
                    SP_THONGSO = x.SP_THONGSO,
                    SP_NHASANXUAT = x.SP_NHASANXUAT,

                    //SP_SOLUONG = x.SP_SOLUONG,
                    SP_SOLUONG = 9999,
                    HOT = x.HOT,
                    GIAMGIA = x.GIAMGIA,
                    DANHGIACAO = x.DANHGIACAO,
                    MATHANG_PHOBIEN = x.MATHANG_PHOBIEN,
                    BANCHAYNHAT = x.BANCHAYNHAT,
                    SP_SETUP = x.SP_SETUP
                })
                .OrderByDescending(x => x.NGAY_DANG)
                .Skip((trang - 1) * kichthuoc)
                .Take(kichthuoc)
                .ToList();
        }


        public List<BaiViet_Model> List_index_tt(int trang, int kichthuoc, int id)
        {
            return ctx.BAI_VIET
                .Where(x => x.TRANG_THAI != 99 && x.ID_NHOM_BAIVIET == id)

                .Join(ctx.NHOM_BAIVIET.Where(x => x.TRANG_THAI != 99),
                x => x.ID_NHOM_BAIVIET,
                y => y.ID,
                (x, y) => new BaiViet_Model
                {
                    ID = x.ID,
                    TIEU_DE = x.TIEU_DE,
                    ID_NHOM_BAIVIET = x.ID_NHOM_BAIVIET,
                    TOM_TAT = x.TOM_TAT,
                    NOI_DUNG = x.NOI_DUNG,
                    IMAGE_URL = x.IMAGE_URL,
                    NGAY_DANG = x.NGAY_DANG,
                    TEN_BAIVIET = y.TEN_BAIVIET,

                    //Noi dung ve san pham
                    SP_GIA = x.SP_GIA,
                    SP_GIAKM = x.SP_GIAKM,
                    SP_BAOHANH = x.SP_BAOHANH,
                    SP_IMGCHITIET = x.SP_IMGCHITIET,
                    SP_THONGSO = x.SP_THONGSO,
                    SP_NHASANXUAT = x.SP_NHASANXUAT,

                    //SP_SOLUONG = x.SP_SOLUONG,
                    SP_SOLUONG = 9999,
                    HOT = x.HOT,
                    GIAMGIA = x.GIAMGIA,
                    DANHGIACAO = x.DANHGIACAO,
                    MATHANG_PHOBIEN = x.MATHANG_PHOBIEN,
                    BANCHAYNHAT = x.BANCHAYNHAT,
                    SP_SETUP = x.SP_SETUP
                })
                .OrderBy(y => y.SP_BAOHANH)
                .Skip((trang - 1) * kichthuoc)
                .Take(kichthuoc)
                .ToList();
        }
        public List<BaiViet_Model> List_index_cm(int trang, int kichthuoc, int id)
        {
            return ctx.BAI_VIET
                .Where(x => x.TRANG_THAI != 99)

                .Join(ctx.NHOM_BAIVIET.Where(x => x.TRANG_THAI != 99),
                x => x.ID_NHOM_BAIVIET,
                y => y.ID,
                (x, y) => new { x, y })
                .Join(ctx.CHUYEN_MUC.Where(x => x.ID == id),
                m => m.y.ID_CHUYENMUC,
                n => n.ID,
                (m, n) =>



                new BaiViet_Model
                {
                    ID = m.x.ID,
                    TIEU_DE = m.x.TIEU_DE,
                    ID_NHOM_BAIVIET = m.x.ID_NHOM_BAIVIET,
                    ID_CHUYENMUC = m.y.ID_CHUYENMUC,
                    TOM_TAT = m.x.TOM_TAT,
                    NOI_DUNG = m.x.NOI_DUNG,
                    IMAGE_URL = m.x.IMAGE_URL,
                    NGAY_DANG = m.x.NGAY_DANG,
                    TEN_BAIVIET = m.y.TEN_BAIVIET,

                    //Noi dung ve san pham
                    SP_GIA = m.x.SP_GIA,
                    SP_GIAKM = m.x.SP_GIAKM,
                    SP_BAOHANH = m.x.SP_BAOHANH,
                    SP_IMGCHITIET = m.x.SP_IMGCHITIET,
                    SP_THONGSO = m.x.SP_THONGSO,
                    SP_NHASANXUAT = m.x.SP_NHASANXUAT,

                    SP_SOLUONG = 9999,
                    //SP_SOLUONG = m.x.SP_SOLUONG,
                    HOT = m.x.HOT,
                    GIAMGIA = m.x.GIAMGIA,
                    DANHGIACAO = m.x.DANHGIACAO,
                    MATHANG_PHOBIEN = m.x.MATHANG_PHOBIEN,
                    BANCHAYNHAT = m.x.BANCHAYNHAT,
                })
                .OrderByDescending(x => x.NGAY_DANG)
                .Skip((trang - 1) * kichthuoc)
                .Take(kichthuoc)
                .ToList();

        }

        public List<BaiViet_Model> List_index_NOIBAT_cm(int id)
        {
            return ctx.BAI_VIET
                .Where(x => x.TRANG_THAI != 99 && x.NOI_BAT == true)

                .Join(ctx.NHOM_BAIVIET.Where(x => x.TRANG_THAI != 99),
                x => x.ID_NHOM_BAIVIET,
                y => y.ID,
                (x, y) => new { x, y })
                .Join(ctx.CHUYEN_MUC.Where(x => x.ID == id),
                m => m.y.ID_CHUYENMUC,
                n => n.ID,
                (m, n) =>



                new BaiViet_Model
                {
                    ID = m.x.ID,
                    TIEU_DE = m.x.TIEU_DE,
                    ID_NHOM_BAIVIET = m.x.ID_NHOM_BAIVIET,
                    ID_CHUYENMUC = m.y.ID_CHUYENMUC,
                    TOM_TAT = m.x.TOM_TAT,
                    NOI_DUNG = m.x.NOI_DUNG,
                    IMAGE_URL = m.x.IMAGE_URL,
                    NGAY_DANG = m.x.NGAY_DANG,
                    TEN_BAIVIET = m.y.TEN_BAIVIET,
                    TEN_CHUYENMUC = n.TEN_CHUYENMUC,

                    //Noi dung ve san pham
                    SP_GIA = m.x.SP_GIA,
                    SP_GIAKM = m.x.SP_GIAKM,
                    SP_BAOHANH = m.x.SP_BAOHANH,
                    SP_IMGCHITIET = m.x.SP_IMGCHITIET,
                    SP_THONGSO = m.x.SP_THONGSO,
                    SP_NHASANXUAT = m.x.SP_NHASANXUAT,

                    SP_SOLUONG = 9999,
                    //SP_SOLUONG = m.x.SP_SOLUONG,
                    HOT = m.x.HOT,
                    GIAMGIA = m.x.GIAMGIA,
                    DANHGIACAO = m.x.DANHGIACAO,
                    MATHANG_PHOBIEN = m.x.MATHANG_PHOBIEN,
                    BANCHAYNHAT = m.x.BANCHAYNHAT,
                })
                .OrderByDescending(x => x.NGAY_DANG)
                .ToList();

        }


        public int CapNhat_BV_LienQuan(int id, int id_bv_lq)
        {
            ctx.BAIVIET_LIENQUAN.Add(new BAIVIET_LIENQUAN()
            {
                ID = id,
                ID_LIENQUAN = id_bv_lq
            });

            try
            {
                ctx.SaveChanges();
                return id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return -1;
            }
        }

        public List<BaiViet_Model> List_Baiviet_lienquan(int id)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.BAIVIET_LIENQUAN
                .Where(x => x.ID == id)
                .Join(ctx.BAI_VIET,
                x => x.ID_LIENQUAN,
                y => y.ID,
                (x, y) => new BaiViet_Model
                {
                    ID = y.ID,
                    TIEU_DE = y.TIEU_DE,
                    ID_NHOM_BAIVIET = y.ID_NHOM_BAIVIET,
                    TOM_TAT = y.TOM_TAT,
                    NOI_DUNG = y.NOI_DUNG,
                    IMAGE_URL = y.IMAGE_URL,
                    NGAY_DANG = y.NGAY_DANG,
                }).ToList();
        }
    }
}