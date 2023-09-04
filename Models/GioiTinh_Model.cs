using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
//using System.Web.Security;

namespace TLBD.Areas.admin.Models.GioiTinh
{
    public class GioiTinh_Model
    {
        public int ID { get; set; }
        public string GIOITINH { get; set; }
    }
}