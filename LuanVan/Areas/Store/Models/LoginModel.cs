using System.ComponentModel.DataAnnotations;

namespace LuanVan.Areas.Store.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Số điện thoại/Email đăng nhập là bắt buộc")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; }
        public bool Rememberme { get; set; }

        public LoginModel() { }
    }
}
