using FluentValidation;
using Tasky.Dtos.Request;

namespace Tasky.Validators;

public class TaskListRequestValidator : AbstractValidator<TaskListRequestDto>
{
    public TaskListRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).MinimumLength(4).WithMessage("Name must have more than 3 characters");
    }
}
