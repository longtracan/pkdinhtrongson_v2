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
    
    public partial class MENU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MENU()
        {
            this.PHANQUYEN_MENU = new HashSet<PHANQUYEN_MENU>();
        }
    
        public int ID { get; set; }
        public string TEN_MENU { get; set; }
        public int THU_TU { get; set; }
        public int MENU_CHA { get; set; }
        public string CONTROLLER { get; set; }
        public string VIEW { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHANQUYEN_MENU> PHANQUYEN_MENU { get; set; }
    }
}
