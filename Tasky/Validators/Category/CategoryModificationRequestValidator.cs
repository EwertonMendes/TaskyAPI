using FluentValidation;
using Tasky.Dtos.Request.Category;

namespace Tasky.Validators.Category;

public class CategoryModificationRequestValidator : AbstractValidator<CategoryModificationRequestDto>
{
    public CategoryModificationRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
            .MinimumLength(4).WithMessage("Name must have more than 3 characters")
            .MaximumLength(50).WithMessage("Name must have less than 50 characters");
    }
}
