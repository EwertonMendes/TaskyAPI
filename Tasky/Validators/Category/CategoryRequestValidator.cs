using FluentValidation;
using Tasky.Dtos.Request.Category;
using Tasky.Exceptions;
using Tasky.Interfaces.Repositories;

namespace Tasky.Validators.Category;

public class CategoryRequestValidator : AbstractValidator<CategoryRequestDto>
{
    public CategoryRequestValidator(ICategoryRepository repository)
    {
        RuleFor(x => x.CategoryId).MustAsync(async (id, cancelationToken) =>
        {
            var category = await repository.GetById(id);

            if (category == null)
                throw new NotFoundException($"A Category with id {id} not found");

            return true;
        });
    }
}
