using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.PhanQuyen_Menu
{
    public class PhanQuyen_Menu_Model
    {
        public int sID_USER { get; set; }
        public int sID_MENU { get; set; }
        public int sTRANG_THAI { get; set; }

        public string sCONTROLLER { get; set; }
        public string sVIEW { get; set; }
        
        public string sTEN_MENU1 { get; set; }
    }
}