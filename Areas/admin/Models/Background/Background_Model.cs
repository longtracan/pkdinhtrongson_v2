using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.Background
{
    public class Background_Model
    {
        public int ID { get; set; }
        public string IMG { get; set; }
        public Nullable<int> TRANG_THAI { get; set; }
    }
}