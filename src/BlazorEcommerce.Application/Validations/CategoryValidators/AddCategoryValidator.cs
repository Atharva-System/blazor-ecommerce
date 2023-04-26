using BlazorEcommerce.Application.Features.Category.Commands.AddCategory;

namespace BlazorEcommerce.Application.Validations.CategoryValidators;

public class AddCategoryValidator : AbstractValidator<AddCategoryCommandRequest>
{
    public AddCategoryValidator()
    {
        RuleFor(d => d.category.Name).NotEmpty().NotNull().WithMessage("{ProperyName} must not be empty!")
                .Length(2, 50).WithMessage("{ProperyName} must be between 2 and 50 characters!");
    }
}
