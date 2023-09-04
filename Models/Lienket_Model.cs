using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.Lien_Ket
{
    public class Lienket_Model
    {
        public int sID { get; set; }
        public string sTIEU_DE { get; set; }
        public string sLIEN_KET_HEF { get; set; }
        public string sIMG_URL { get; set; }
        public bool sHIENTHI_PHAI { get; set; }
        public int sTHUTU_PHAI { get; set; }
        public bool sHIENTHI_TRAI { get; set; }
        public int sTHUTU_TRAI { get; set; }
        public DateTime sNGAY_TAO { get; set; }
        public string sIP_TAO { get; set; }
        public DateTime sNGAY_SUA { get; set; }
        public string sIP_SUA { get; set; }
        public DateTime sNGAY_XOA { get; set; }
        public string sIP_XOA { get; set; }
        public int sTRANG_THAI { get; set; }
    }
}