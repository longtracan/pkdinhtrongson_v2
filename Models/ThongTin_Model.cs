using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.ThongTin
{
    public class ThongTin_Model
    {
        public int sID { get; set; }
        public string sTEN_DONVI { get; set; }
        public string sDIACHI_DONVI { get; set; }
        public string sGIAY_PHEP { get; set; }
        public string sSDT { get; set; }
        public string sDIDONG { get; set; }
        public string sEMAIL { get; set; }
        public string sFACEBOOK { get; set; }
        public string sFAX { get; set; }
        public string sNGUOI_DAIDIEN { get; set; }
        public string sCHUC_VU { get; set; }
        public string sTHIET_KE { get; set; }
        public string sLAT { get; set; }
        public string sLNG { get; set; }
        public DateTime sNGAY_SUA { get; set; }
        public string sIP_SUA { get; set; }
        public string CHAO_MUNG { get; set; }

        public string MAP_IMG { get; set; }
        public string MAP_LINK { get; set; }
        public string VIDEO_LINK { get; set; }
    }
}