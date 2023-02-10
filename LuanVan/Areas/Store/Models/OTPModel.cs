using System.ComponentModel.DataAnnotations;

namespace LuanVan.Areas.Store.Models
{
    public class OTPModel
    {
        [Required(ErrorMessage = "Mã OTP là bắt buộc")]
        public string OTP { get; set; }

        public OTPModel() { }
    }
}
