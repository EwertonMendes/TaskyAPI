using Tasky.Dtos.Request.User;
using Tasky.Dtos.Response;

namespace Tasky.Interfaces.Services;

public interface IUserService
{
    Task<UserResponseDto> CreateUser(UserModificationRequestDto userDto);
}
