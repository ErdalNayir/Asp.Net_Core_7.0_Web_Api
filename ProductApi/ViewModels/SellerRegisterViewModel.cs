using System.ComponentModel;

namespace ProductApi.ViewModels
{
    public class SellerRegisterViewModel
    {
        [DisplayName("Satıcı Id")]
        public int SellerId { get; set; }

        [DisplayName("Satıcı İsmi")]
        public string? SellerName { get; set; }

        [DisplayName("Satıcı Email")]
        public string? email { get; set; }

        [DisplayName("Satıcı Şifre")]
        public string? password { get; set; }

        [DisplayName("Satıcı doğrulama şifresi")]
        public string? confirmPassword { get; set; }
    }
}
