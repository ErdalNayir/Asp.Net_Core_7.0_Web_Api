using System.ComponentModel;

namespace ProductApi.ViewModels
{
    public class SellerLoginViewModel
    {
        [DisplayName("Satıcı Email")]
        public string? email { get; set; }

        [DisplayName("Satıcı Şifre")]
        public string? password { get; set; }

    }
}
