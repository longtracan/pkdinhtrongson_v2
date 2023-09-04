using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLBD.Areas.admin.Models.BaiViet;
using TLBD.Areas.admin.Models.CartItem;

namespace TLBD.Areas.admin.Models.OrderItem
{
    public class OrderItem_Model
    {
        public string OrderID { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }
        public int? StatusID { get; set; }

        //Tong gia tri don hang
        public int Total { get; set; }
        
        //Thong tin khach hang
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
