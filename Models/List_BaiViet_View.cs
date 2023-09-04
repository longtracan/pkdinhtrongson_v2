using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TLBD.Areas.admin.Models.NhomBaiViet;
using TLBD.Areas.admin.Models.ChuyenMuc;

namespace TLBD.Areas.admin.Models.BaiViet
{
    public class List_BaiViet_View
    {
        public NhomBaiViet_Model Nhom_BaiViet { get; set; }
        public BaiViet_Model BaiViet { get; set; }
        public IList<BaiViet_Model> List_BaiViet { get; set; }
        public ChuyenMuc_Model ChuyenMuc { get; set; }
    }
}