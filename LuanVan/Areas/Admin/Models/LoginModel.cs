using System.ComponentModel.DataAnnotations;

namespace LuanVan.Areas.Admin.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Số điện thoại đăng nhập là bắt buộc")]
        public string NumberPhone { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; }
        public bool Rememberme { get; set; }

        public LoginModel() { }

    }
}
