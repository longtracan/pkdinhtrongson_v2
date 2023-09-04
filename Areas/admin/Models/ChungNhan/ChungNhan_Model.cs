using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.ChungNhan
{
    public class ChungNhan_Model
    {
        public int ID { get; set; }
        public string TIEU_DE { get; set; }
        public string IMG { get; set; }
        public Nullable<System.DateTime> NGAY { get; set; }
        public Nullable<int> TRANG_THAI { get; set; }
    }
}