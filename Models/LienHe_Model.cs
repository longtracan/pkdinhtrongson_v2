using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TLBD.Models.LienHe
{
    public class LienHe_Model
    {
        [Required(ErrorMessage = "Cần nhập họ tên")]
        public string HoTen { get; set; }
        //[Required(ErrorMessage = "Cần nhập địa chỉ")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Cần nhập số điện thoại")]
        [RegularExpression(@"0\d{9,10}", ErrorMessage = "Kiểm tra số điện thoại!")]
        public string SoDienThoai { get; set; }
        [Required(ErrorMessage = "Cần nhập email")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Cần nhập nội dung")]
        public string NoiDung { get; set; }

        public DateTime NgayDang { get; set; }
    }
}