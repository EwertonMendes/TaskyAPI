using FluentValidation;
using Tasky.Dtos.Request.TaskList;

namespace Tasky.Validators.Category;

public class TaskListModificationRequestValidator : AbstractValidator<TaskListModificationRequestDto>
{
    public TaskListModificationRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
            .MinimumLength(4).WithMessage("Name must have more than 3 characters")
            .MaximumLength(50).WithMessage("Name must have less than 50 characters");
    }
}
