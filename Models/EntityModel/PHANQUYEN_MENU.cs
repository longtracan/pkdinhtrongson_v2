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
    
    public partial class PHANQUYEN_MENU
    {
        public int ID_USER { get; set; }
        public int ID_MENU { get; set; }
        public int TRANG_THAI { get; set; }
    
        public virtual MENU MENU { get; set; }
    }
}