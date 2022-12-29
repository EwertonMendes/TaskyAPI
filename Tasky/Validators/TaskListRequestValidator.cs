using FluentValidation;
using Tasky.Dtos.Request;
using Tasky.Exceptions;
using Tasky.Interfaces.Repositories;

namespace Tasky.Validators;

public class TaskListRequestValidator : AbstractValidator<TaskListRequestDto>
{
    private int _userId;

    public TaskListRequestValidator(ICategoryRepository categoryRepository, IUserRepository userRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).MinimumLength(4).WithMessage("Name must have more than 3 characters");
        RuleFor(x => x.UserId).MustAsync(async (id, cancelationToken) =>
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null)
                throw new NotFoundException($"A User with id {id} not found");

            _userId = user.Id;
            return true;
        });
        RuleFor(x => x.CategoryId).MustAsync(async (id, cancelationToken) =>
        {
            var category = await categoryRepository.GetById(_userId, id);

            if (category == null)
                throw new NotFoundException($"A Category with id {id} not found");

            return true;
        });
    }
}
