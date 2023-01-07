using Tasky.Models;

namespace Tasky.Interfaces.Services;

public interface IAuthService
{
    Task<User> ValidateCredentials(string email, string password);
}
