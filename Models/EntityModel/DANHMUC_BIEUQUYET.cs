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
    
    public partial class DANHMUC_BIEUQUYET
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DANHMUC_BIEUQUYET()
        {
            this.BIEU_QUYET = new HashSet<BIEU_QUYET>();
        }
    
        public int ID { get; set; }
        public string TEN_BIEUQUYET { get; set; }
        public bool HIEN_THI { get; set; }
        public string GHI_CHU { get; set; }
        public string IP_TAO { get; set; }
        public System.DateTime NGAY_TAO { get; set; }
        public string IP_SUA { get; set; }
        public Nullable<System.DateTime> NGAY_SUA { get; set; }
        public string IP_XOA { get; set; }
        public Nullable<System.DateTime> NGAY_XOA { get; set; }
        public int TRANG_THAI { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BIEU_QUYET> BIEU_QUYET { get; set; }
    }
}