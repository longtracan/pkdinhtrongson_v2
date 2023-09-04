using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.Nhom_VanBan
{
    public class Nhom_VanBan_Model
    {
        public int sID { get; set; }
        public string sTEN_NHOM_VANBAN { get; set; }      

        public DateTime sNGAY_TAO { get; set; }
        public string sIP_TAO { get; set; }
        public DateTime sNGAY_SUA { get; set; }
        public string sIP_SUA { get; set; }
        public DateTime sNGAY_XOA { get; set; }
        public string sIP_XOA { get; set; }
        public int sTRANG_THAI { get; set; }
    }
}