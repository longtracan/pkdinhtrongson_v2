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
    
    public partial class LOAI_HIENTHI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAI_HIENTHI()
        {
            this.NHOM_BAIVIET = new HashSet<NHOM_BAIVIET>();
        }
    
        public int ID { get; set; }
        public string TEN_LOAI_HIENTHI { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHOM_BAIVIET> NHOM_BAIVIET { get; set; }
    }
}
