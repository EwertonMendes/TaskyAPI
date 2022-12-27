using Tasky.Dtos.Request;
using Tasky.Dtos.Response;

namespace Tasky.Interfaces.Services;

public interface IUserService
{
    Task<UserResponseDto> CreateUser(UserRequestDto userDto);
}
