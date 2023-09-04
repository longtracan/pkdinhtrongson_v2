using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.SubBanner
{
    public class SubBanner_Model
    {
        public int sID { get; set; }
        public string sTIEUDE { get; set; }
        public string sIMG_URL { get; set; }
        public bool sHIENTHI { get; set; }
        public DateTime sNGAY_TAO { get; set; }
        public string sIP_TAO { get; set; }
        public DateTime sNGAY_SUA { get; set; }
        public string sIP_SUA { get; set; }
        public DateTime sNGAY_XOA { get; set; }
        public string sIP_XOA { get; set; }
        public int sTRANG_THAI { get; set; }     
    }
}