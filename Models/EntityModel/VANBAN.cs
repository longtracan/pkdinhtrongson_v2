//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TLBD.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class VANBAN
    {
        public int ID { get; set; }
        public int ID_NHOM_VANBAN { get; set; }
        public int ID_COQUAN_BANHANH { get; set; }
        public string SO_KYHIEU { get; set; }
        public string NOIDUNG_TRICHYEU { get; set; }
        public string FILE_DOWNLOAD { get; set; }
        public System.DateTime NGAY_BANHANH { get; set; }
        public string LINH_VUC { get; set; }
        public string GHI_CHU { get; set; }
        public string IP_TAO { get; set; }
        public System.DateTime NGAY_TAO { get; set; }
        public string IP_SUA { get; set; }
        public Nullable<System.DateTime> NGAY_SUA { get; set; }
        public string IP_XOA { get; set; }
        public Nullable<System.DateTime> NGAY_XOA { get; set; }
        public int TRANG_THAI { get; set; }
    
        public virtual COQUAN_BANHANH COQUAN_BANHANH { get; set; }
        public virtual NHOM_VANBAN NHOM_VANBAN { get; set; }
    }
}
