using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.Banner
{
    public class Banner_Model
    {
        public int ID { get; set; }
        public string TIEUDE { get; set; }
        public string IMG_URL { get; set; }
        public bool HIENTHI { get; set; }
        public string IP_TAO { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public string IP_SUA { get; set; }
        public DateTime NGAY_SUA { get; set; }
        public string IP_XOA { get; set; }
        public DateTime NGAY_XOA { get; set; }
        public Nullable<int> TRANG_THAI { get; set; }
        public string IMG_URL_MB { get; set; }
        public Nullable<int> SET_UP { get; set; } 
    }
}