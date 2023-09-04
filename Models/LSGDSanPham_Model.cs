using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLBD.Areas.admin.Models.LSGDSanPham
{
    public class LSGDSanPham_Model
    {
        public string ID_donhang { get; set; }
        public int SoLuong { get; set; }
        public DateTime? Ngay { get; set; }
        public string TrangThaiDonHang { get; set; }
        public string KhachHang { get; set; }
    }
}
