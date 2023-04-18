using FluentValidation;
using ProductApi.ViewModels;

namespace ProductApi.Validation
{
    public class SellerLoginValidator : AbstractValidator<SellerLoginViewModel>
    {
        public SellerLoginValidator()
        {
            RuleFor(k => k.email).NotEmpty().WithMessage("E-posta adresi zorunludur.").EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(k => k.password).NotEmpty().WithMessage("Şifre zorunludur.");
        }
    }
}
