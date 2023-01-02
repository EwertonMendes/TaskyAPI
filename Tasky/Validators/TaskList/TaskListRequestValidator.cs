using FluentValidation;
using Tasky.Dtos.Request.User;
using Tasky.Exceptions;
using Tasky.Interfaces.Repositories;

namespace Tasky.Validators.Category;

public class TaskListRequestValidator : AbstractValidator<TaskListRequestDto>
{
    public TaskListRequestValidator(ITaskListRepository repository)
    {
        RuleFor(x => x.TaskListId).MustAsync(async (id, cancelationToken) =>
        {
            var taskList = await repository.GetById(id);

            if (taskList == null)
                throw new NotFoundException($"A Task List with id {id} was not found");

            return true;
        });
    }
}
