using FluentValidation;
using Tasky.Dtos.Request;
using Tasky.Exceptions;
using Tasky.Interfaces.Repositories;

namespace Tasky.Validators;

public class TaskListRequestValidator : AbstractValidator<TaskListRequestDto>
{
    public TaskListRequestValidator(ICategoryRepository categoryRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).MinimumLength(4).WithMessage("Name must have more than 3 characters");
        RuleFor(x => x.CategoryId).MustAsync(async (id, cancelationToken) =>
        {
            var category = await categoryRepository.GetById(id);

            if (category == null)
                throw new NotFoundException($"Category with id {id} not found");

            return true;
        });
    }
}
