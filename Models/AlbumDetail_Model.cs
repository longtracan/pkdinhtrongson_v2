using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TLBD.Areas.admin.Models.AlbumDetail
{
    public class AlbumDetail_Model
    {
        public int sID { get; set; }
        public int sID_ALBUM { get; set; }
        public int sTRANG_THAI { get; set; }
        public string sIMAGE_URL { get; set; }        
        public bool sDAI_DIEN { get; set; }

        public int sID_NHOM { get; set; }
    }
}