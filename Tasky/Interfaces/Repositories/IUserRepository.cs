using Tasky.Dtos.Request.User;
using Tasky.Models;

namespace Tasky.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task<User> AddNewUser(UserModificationRequestDto userDto);
}
