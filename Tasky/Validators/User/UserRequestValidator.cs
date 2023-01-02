using FluentValidation;
using Tasky.Dtos.Request.User;
using Tasky.Exceptions;
using Tasky.Interfaces.Repositories;

namespace Tasky.Validators.User;

public class UserRequestValidator : AbstractValidator<UserRequestDto>
{
    public UserRequestValidator(IUserRepository repository)
    {
        RuleFor(x => x.UserId).MustAsync(async (id, cancelationToken) =>
        {
            var category = await repository.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException($"A User with id {id} not found");

            return true;
        });
    }
}
