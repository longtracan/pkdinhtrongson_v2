using Newtonsoft.Json;
using System;
using System.Collections;
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
using TLBD.Areas.admin.Models.LSGDSanPham;
using TLBD.Models;
using TLBD.Models.EntityModel;

namespace TLBD.Areas.admin.Models.BaiViet
{
    public class SanPham_Class
    {
        //BaiViet_DBDataContext ctx = new BaiViet_DBDataContext();
        EntitiesData ctx = new EntitiesData();

        public int GetLSGDCount(string sd, string ed)
        {
            DateTime sDate = Convert.ToDateTime(DateTime.ParseExact(sd, "dd-MM-yyyy", CultureInfo.InvariantCulture));
            DateTime eDate = Convert.ToDateTime(DateTime.ParseExact(ed, "dd-MM-yyyy", CultureInfo.InvariantCulture)).AddDays(1);

            var _r = ctx.ORDER_DETAIL
                .Join(ctx.ORDERs.Where(x => DateTime.Compare(x.OrderDate.Value, sDate) >= 0 && DateTime.Compare(x.OrderDate.Value, eDate) < 0),
                x => x.OrderID,
                y => y.ID,
                (x, y) => new
                {
                    x
                })
                .GroupBy(x => x.x.ProductID
                , (key, g) => new
                {
                    ProductID = key,
                    SoLuong = g.Sum(y => y.x.Quantity)
                })
                .Join(ctx.BAI_VIET.Where(x => x.TRANG_THAI == 0),
                x => x.ProductID,
                y => y.ID,
                (x, y) => new
                {
                    x,
                    y
                }).Count();

            return _r;
        }

        public IList<LSGD_Model> GetLSGDList(int page, int pageSize, string sd, string ed)
        {
            DateTime sDate = Convert.ToDateTime(DateTime.ParseExact(sd, "dd-MM-yyyy", CultureInfo.InvariantCulture));
            DateTime eDate = Convert.ToDateTime(DateTime.ParseExact(ed, "dd-MM-yyyy", CultureInfo.InvariantCulture)).AddDays(1);

            var _r = ctx.ORDER_DETAIL
                .Join(ctx.ORDERs.Where(x => DateTime.Compare(x.OrderDate.Value, sDate) >= 0 && DateTime.Compare(x.OrderDate.Value, eDate) < 0),
                x => x.OrderID,
                y => y.ID,
                (x, y) => new
                {
                    x
                })
                .GroupBy(x => x.x.ProductID
                , (key, g) => new
                {
                    ProductID = key,
                    SoLuong = g.Sum(y => y.x.Quantity)
                })
                .Join(ctx.BAI_VIET.Where(x => x.TRANG_THAI == 0),
                x => x.ProductID,
                y => y.ID,
                (x, y) => new
                {
                    SanPham = y.TIEU_DE,
                    SoLuong = x.SoLuong,
                    SoLuongKho = y.SP_SOLUONG,
                    NhomSP_ID = y.ID_NHOM_BAIVIET
                })
                .Join(ctx.NHOM_BAIVIET.Where(x => x.TRANG_THAI == 0),
                x => x.NhomSP_ID,
                y => y.ID,
                (x, y) => new LSGD_Model
                {
                    SanPham = x.SanPham,
                    SoLuong = x.SoLuong,
                    SoLuongKho = x.SoLuongKho,
                    NhomSanPham = y.TEN_BAIVIET
                })
                .OrderByDescending(x => x.SoLuong)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _r;
        }

        public int GetLSCount(int id_sanpham, string sd, string ed)
        {
            DateTime sDate = Convert.ToDateTime(DateTime.ParseExact(sd, "dd-MM-yyyy", CultureInfo.InvariantCulture));
            DateTime eDate = Convert.ToDateTime(DateTime.ParseExact(ed, "dd-MM-yyyy", CultureInfo.InvariantCulture)).AddDays(1);

            var _r = ctx.ORDER_DETAIL
                .Where(x => x.ProductID == id_sanpham)
                .Join(ctx.ORDERs.Where(x => DateTime.Compare(x.OrderDate.Value, sDate) >= 0 && DateTime.Compare(x.OrderDate.Value, eDate) < 0),
                x => x.OrderID,
                y => y.ID,
                (x, y) => new { x, y })
                .Count();

            return _r;
        }

        public IList<LSGDSanPham_Model> GetLSList(int page, int pageSize, int id_sanpham, string sd, string ed)
        {
            DateTime sDate = Convert.ToDateTime(DateTime.ParseExact(sd, "dd-MM-yyyy", CultureInfo.InvariantCulture));
            DateTime eDate = Convert.ToDateTime(DateTime.ParseExact(ed, "dd-MM-yyyy", CultureInfo.InvariantCulture)).AddDays(1);

            var _r = ctx.ORDER_DETAIL
                .Where(x => x.ProductID == id_sanpham)
                .Join(ctx.ORDERs.Where(x => DateTime.Compare(x.OrderDate.Value, sDate) >= 0 && DateTime.Compare(x.OrderDate.Value, eDate) < 0),
                x => x.OrderID,
                y => y.ID,
                (x, y) => new
                {
                    ID_donhang = x.OrderID,
                    OrderStatusID = y.Status,
                    Ngay = y.OrderDate,
                    SoLuong = x.Quantity,
                    KhachHang = y.Name
                })
                .Join(ctx.ORDER_STATUS,
                x => x.OrderStatusID,
                y => y.ID,
                (x, y) => new LSGDSanPham_Model
                {
                    ID_donhang = x.ID_donhang,
                    TrangThaiDonHang = y.Description,
                    Ngay = x.Ngay,
                    SoLuong = x.SoLuong,
                    KhachHang = x.KhachHang
                }).OrderByDescending(x => x.Ngay)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _r;
        }

        public static IList<BaiViet_Model> GetList(int id_nhom_baiviet)
        {
            EntitiesData ctx = new EntitiesData();
            return ctx.BAI_VIET
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


                    HOT = m.y.HOT,
                    GIAMGIA = m.y.GIAMGIA,
                    DANHGIACAO = m.y.DANHGIACAO,
                    MATHANG_PHOBIEN = m.y.MATHANG_PHOBIEN,
                    BANCHAYNHAT = m.y.BANCHAYNHAT,


                })
                .OrderByDescending(y => y.NGAY_DANG)
                .ToList();
        }

        public int GetCount(int id_nhom_baiviet)
        {
            return ctx.BAI_VIET
                .Where(x => x.TRANG_THAI != 99 && x.ID_NHOM_BAIVIET == id_nhom_baiviet)
                .ToList().Count;
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
                    //SP_SOLUONG = m.y.SP_SOLUONG,
                    SP_SOLUONG = 9999,

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
                    //SP_SOLUONG = bv_nbv.bv.SP_SOLUONG
                    SP_SOLUONG = 9999
                }).FirstOrDefault();

            return _r;
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

                //====
                if (!string.IsNullOrEmpty(model.SP_IMGCHITIET))
                {
                    data.SP_IMGCHITIET = model.SP_IMGCHITIET;
                }
                data.SP_GIA = model.SP_GIA;
                data.SP_GIAKM = model.SP_GIAKM;
                data.SP_NHASANXUAT = model.SP_NHASANXUAT;
                data.SP_BAOHANH = model.SP_BAOHANH;
                data.SP_SOLUONG = 9999;
                //data.SP_SOLUONG = model.SP_SOLUONG;
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
                    //SP_SOLUONG = model.SP_SOLUONG
                    SP_SOLUONG = 9999
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

        public List<BaiViet_Model> List_Baiviet(int id)
        {
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
            return ctx.BAIVIET_LIENQUAN
                .Where(x => x.ID == id)
                .Join(ctx.BAI_VIET,
                x => x.ID_LIENQUAN,
                y => y.ID,
                (x, y) => new BaiViet_Model
                {
                    ID = y.ID,
                    TIEU_DE = y.TIEU_DE
                }).ToList();
        }
    }
}