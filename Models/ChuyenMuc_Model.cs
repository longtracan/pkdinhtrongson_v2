using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.ChuyenMuc
{
    public class ChuyenMuc_Model
    {
        public int ID { get; set; }
        public string TEN_CHUYENMUC { get; set; }
        public bool MENU_TREN { get; set; }
        public Nullable<int> THUTU_TREN { get; set; }
        public bool HIENTHI_TRAI { get; set; }
        public Nullable<int> THUTU_TRAI { get; set; }
        public bool HIENTHI_TRANGCHU { get; set; }
        public Nullable<int> THUTU_TRANGCHU { get; set; }
        public string LINK { get; set; }
        public string IP_TAO { get; set; }
        public Nullable<System.DateTime> NGAY_TAO { get; set; }
        public string IP_SUA { get; set; }
        public Nullable<System.DateTime> NGAY_SUA { get; set; }
        public string IP_XOA { get; set; }
        public Nullable<System.DateTime> NGAY_XOA { get; set; }
        public Nullable<int> TRANG_THAI { get; set; }

    }
}