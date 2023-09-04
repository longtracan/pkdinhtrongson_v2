using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.NhomBaiViet
{
    public class NhomBaiViet_Model
    {
        public int ID { get; set; }
        public int ID_CHUYENMUC { get; set; }
        public string TEN_BAIVIET { get; set; }
        public int LOAI_HIENTHI { get; set; }
        public string LINK { get; set; }
        public string IP_TAO { get; set; }
        public System.DateTime NGAY_TAO { get; set; }
        public string IP_SUA { get; set; }
        public Nullable<System.DateTime> NGAY_SUA { get; set; }
        public string IP_XOA { get; set; }
        public Nullable<System.DateTime> NGAY_XOA { get; set; }
        public int TRANG_THAI { get; set; }
        public int THUTU { get; set; }
        public Nullable<int> LICHSU_SOLUONG { get; set; }
        public bool HIENTHI_BAIVIET_MENU { get; set; }
        public string ICON { get; set; }

        public string TEN_CHUYENMUC { get; set; }
        /*
         * EXT
         */
        public bool SANPHAM { get; set; }
    }
}