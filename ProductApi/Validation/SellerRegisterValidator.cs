using FluentValidation;
using ProductApi.Models;
using ProductApi.ViewModels;

namespace ProductApi.Validation
{
    public class SellerRegisterValidator : AbstractValidator<SellerRegisterViewModel>
    {
        public SellerRegisterValidator()
        {
            RuleFor(k => k.SellerName).NotEmpty().MinimumLength(3).MaximumLength(30).WithMessage("Kategori ismi zorunludur ve uzunluğu 3 ile 30 karakter olmalıdır");

            RuleFor(k => k.email).NotEmpty().WithMessage("E-posta adresi zorunludur.").EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(k => k.password)
           .NotEmpty().WithMessage("Şifre zorunludur.")
           .Equal(k => k.confirmPassword).WithMessage("Şifre ve şifre doğrulama alanları aynı olmalıdır.");

            RuleFor(k => k.confirmPassword)
                .NotEmpty().WithMessage("Şifre doğrulama alanı zorunludur.");
        }
    }
}
