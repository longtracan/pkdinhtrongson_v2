using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLBD.Areas.admin.Models.CartItem;

namespace TLBD.Areas.admin.Models.Order
{
    public class Order_Model
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public List<CartItem_Model> detail_lst { get; set; }
    }
}
