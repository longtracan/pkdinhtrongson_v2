using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace TLBD.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Mật khẩu dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu không trùng khớp...")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }

    //Thanh vien

    public class DangkyThanhvienModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage="Cần nhập thông tin")]        
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} cần tối thiểu {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Họ tên")]        
        public string HoTen { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Vui lòng kiểm tra lại")]
        [Display(Name = "Số điện thoại")]
        public string Sodienthoai { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime Ngaysinh { get; set; }

        [Required]
        [Display(Name = "Giới tính")]
        public string Gioitinh { get; set; }

        [Required]
        [Display(Name = "Quốc tịch")]
        public string QuoctichId { get; set; }

        public string QuoctichTen { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Vui lòng kiểm tra lại")]
        [Display(Name = "Số CMND")]
        public string CMND { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày cấp")]
        public DateTime Ngaycap { get; set; }

        [Required]
        [Display(Name = "Nơi cấp")]
        public string Noicap { get; set; }

        [Required]
        [Display(Name = "HK Thường trú")]
        public string HKTT { get; set; }

        [Required]
        [Display(Name = "Nơi cư trú")]
        public string Noicutru { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Cần nhập thông tin")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
