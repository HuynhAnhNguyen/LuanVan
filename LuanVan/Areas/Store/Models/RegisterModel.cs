using System.ComponentModel.DataAnnotations;

namespace LuanVan.Areas.Store.Models
{
    public class RegisterModel
    {
        public string? MaKhachHang { get; set; }
        [Required(ErrorMessage = "Họ lót không được bỏ trống!"), MaxLength(32)]
        public string HoKhachHang { get; set; }
        [Required(ErrorMessage = "Tên không được bỏ trống!")]
        public string TenKhachHang { get; set; }
        [Required(ErrorMessage = "Ngày sinh không được để trống!")]
        public DateTime NgaySinh { get; set; }
        [Required(ErrorMessage = "Giới tính không được bỏ trống!")]
        public string GioiTinh { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [MaxLength(10)]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string? SoDienThoai { get; set; }
        [Required(ErrorMessage = "Email không được bỏ trống!")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage="Địa chỉ email không hợp lệ!" )]
        public string Email { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống!")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Mật khẩu không đủ mạnh!")]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string MatKhau2 { get; set; }
        public RegisterModel() { }
    }
}
