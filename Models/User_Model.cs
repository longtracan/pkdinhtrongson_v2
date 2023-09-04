using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TLBD.Areas.admin.Models.Account
{
    public class User_Model
    {
        public int sID { get; set; }
        public string sUSERNAME { get; set; }
        public string sPASSWORD { get; set; }
        public string sTEN_USER { get; set; }
        public int sTRANG_THAI { get; set; }
        public string sIMG_URL { get; set; }
        public DateTime? sNGAY_SINH { get; set; }
        public int? sGIOI_TINH { get; set; }
        public string sSO_DIEN_THOAI { get; set; }
        public string sEMAIL { get; set; }
        public string sDIA_CHI { get; set; }
        public DateTime sNGAY_TAO { get; set; }
        public string sIP_TAO { get; set; }
        public DateTime sNGAY_SUA { get; set; }
        public string sIP_SUA { get; set; }
        public DateTime sNGAY_XOA { get; set; }
        public string sIP_XOA { get; set; }
        public bool sSYSTEM_ACCOUNT { get; set; }
    }
}