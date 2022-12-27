using Tasky.Dtos.Request;
using Tasky.Models;

namespace Tasky.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddNewUser(UserRequestDto userDto);
}
