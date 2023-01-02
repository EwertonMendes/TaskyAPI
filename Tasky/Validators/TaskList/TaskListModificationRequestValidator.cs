using FluentValidation;
using Tasky.Dtos.Request.Category;
using Tasky.Dtos.Request.TaskList;

namespace Tasky.Validators.Category;

public class TaskListModificationRequestValidator : AbstractValidator<TaskListModificationRequestDto>
{
    public TaskListModificationRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).MinimumLength(4).WithMessage("Name must have more than 3 characters");
    }
}
