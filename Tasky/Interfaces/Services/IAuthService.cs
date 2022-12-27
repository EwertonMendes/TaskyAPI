namespace Tasky.Interfaces.Services;

public interface IAuthService
{
    Task<bool> ValidateCredentials(string email, string password);
}
