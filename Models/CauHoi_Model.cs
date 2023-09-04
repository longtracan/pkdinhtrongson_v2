using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TLBD.Areas.admin.Models.CauHoi
{
    public class CauHoi_Model
    {             
        public int ID { get; set; }
        public bool HIEN_THI { get; set; }

        [Required]
        public string NGUOI_HOI { get; set; }

        [Required]
        [EmailAddress]
        public string EMAIL { get; set; }

        public string DIA_CHI { get; set; }

        public string SO_DIEN_THOAI { get; set; }

        [Required]
        public string CAU_HOI { get; set; }
        public string TRA_LOI { get; set; }    
        public DateTime NGAY_DANG { get; set; }        
        public int TRANG_THAI { get; set; }

        public bool DA_TRA_LOI { get; set; }
    }
}