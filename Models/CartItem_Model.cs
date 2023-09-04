using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLBD.Areas.admin.Models.BaiViet;

namespace TLBD.Areas.admin.Models.CartItem
{
    public class CartItem_Model
    {
        public BaiViet_Model Product { get; set;}
        public int Quantity { get; set; }
    }
}
