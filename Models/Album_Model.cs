using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.Album
{
    public class Album_Model
    {
        public int sID { get; set; }
        public string sTIEU_DE { get; set; }
        public string sIMAGE_URL { get; set; } //hinh dai dien lay trong AlbumDetail
        public DateTime sNGAY_DANG { get; set; }
        public DateTime sNGAY_TAO { get; set; }
        public string sIP_TAO { get; set; }
        public DateTime sNGAY_SUA { get; set; }
        public string sIP_SUA { get; set; }
        public DateTime sNGAY_XOA { get; set; }
        public string sIP_XOA { get; set; }
        public int sTRANG_THAI { get; set; }

    }
}