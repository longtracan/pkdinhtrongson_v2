using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.BaiViet
{
    public class BaiViet_Model
    {
        public int ID { get; set; }
        public int ID_NHOM_BAIVIET { get; set; }
        public string TIEU_DE { get; set; }
        public bool NOI_BAT { get; set; }
        public string TOM_TAT { get; set; }
        public string NOI_DUNG { get; set; }
        public string IMAGE_URL { get; set; }
        public string FILE_DOWNLOAD { get; set; }
        public int ID_NGUOIVIETBAI { get; set; }
        public string IP_TAO { get; set; }
        public System.DateTime NGAY_TAO { get; set; }
        public string IP_SUA { get; set; }
        public Nullable<System.DateTime> NGAY_SUA { get; set; }
        public string IP_XOA { get; set; }
        public Nullable<System.DateTime> NGAY_XOA { get; set; }
        public int TRANG_THAI { get; set; }
        public Nullable<int> SP_GIA { get; set; }
        public Nullable<int> SP_GIAKM { get; set; }
        public Nullable<int> SP_BAOHANH { get; set; }
        public string SP_NHASANXUAT { get; set; }
        public string SP_IMGCHITIET { get; set; }
        public string SP_THONGSO { get; set; }
       
        public Nullable<int> SP_SOLUONG { get; set; }
        public bool HOT { get; set; }
        public bool GIAMGIA { get; set; }
        public bool DANHGIACAO { get; set; }
        public bool MATHANG_PHOBIEN { get; set; }
        public bool BANCHAYNHAT { get; set; }
        public Nullable<int> SP_SETUP { get; set; }
        public Nullable<System.DateTime> NGAY_DANG { get; set; }
        public string MAU_SAC { get; set; }
        public string DIADIEM_BAOHANH { get; set; }
        public string XUATXU { get; set; }
        public Nullable<System.DateTime> THOIGIANKM_BEGIN { get; set; }
        public Nullable<System.DateTime> THOIGIANKM_END { get; set; }

        /*
         * EXT
         */
        public string TEN_BAIVIET { get; set; }
        public int LOAI_HIENTHI { get; set; }
        public int ID_CHUYENMUC { get; set; }

        public string TEN_CHUYENMUC { get; set; }

        public bool SANPHAM { get; set; }
        public string SAVE_OFF { get; set; }
    }
}