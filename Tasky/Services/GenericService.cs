using FluentValidation;
using Tasky.Dtos.Request.User;
using Tasky.Interfaces.Repositories;

namespace Tasky.Services;

public abstract class GenericService : IGenericService
{
    private readonly IValidator<UserRequestDto> _userValidator;
    public GenericService(IValidator<UserRequestDto> userValidator)
    {
        _userValidator = userValidator;
    }

    public async Task ValidateUserId(int userId)
    {
        await _userValidator.ValidateAndThrowAsync(new UserRequestDto { UserId = userId });
    }
}
