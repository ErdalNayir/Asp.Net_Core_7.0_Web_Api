using FluentValidation;
using ProductApi.Models;

namespace ProductApi.Validation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator() {
            RuleFor(k => k.CategoryName).NotEmpty().MinimumLength(5).MaximumLength(30).WithMessage("Kategori ismi zorunludur ve uzunluğu 5 ile 30 karakter olmalıdır");
        }
    }
}
