using FluentValidation;
using ProductApi.Models;

namespace ProductApi.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(k => k.Name).NotEmpty().MinimumLength(5).MaximumLength(30).WithMessage("Ürün ismi zorunludur ve uzunluğu 5 ile 30 karakter olmalıdır");
            RuleFor(k => k.Price).NotEmpty().Must(Price => Price.GetType() == typeof(int)).InclusiveBetween(5, 5000).WithMessage("Fiyat alanı zorunludur ve 5 ile 5000 TL arası olmalıdır");
            RuleFor(k => k.Description).NotEmpty().MaximumLength(200).MinimumLength(10).WithMessage("Açıklama zorunludur ve uzunluğu 10 ile 200 karakter arası olmalıdır ");
        }
    }
}
