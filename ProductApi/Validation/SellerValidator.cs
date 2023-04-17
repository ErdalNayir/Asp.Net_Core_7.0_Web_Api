using FluentValidation;
using ProductApi.Models;

namespace ProductApi.Validation
{
    public class SellerValidator : AbstractValidator<Seller>
    {
        public SellerValidator() {
            RuleFor(k => k.SellerName).NotEmpty().MinimumLength(3).MaximumLength(30).WithMessage("Kategori ismi zorunludur ve uzunluğu 3 ile 30 karakter olmalıdır");
            RuleFor(k => k.email).NotEmpty().WithMessage("E-posta adresi zorunludur.").EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
        }
    }
}
