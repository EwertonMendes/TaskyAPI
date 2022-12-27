using Microsoft.AspNetCore.Identity;
using Tasky.Interfaces.Repositories;
using Tasky.Interfaces.Services;
using Tasky.Models;

namespace Tasky.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> ValidateCredentials(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return false;

        if (_passwordHasher.VerifyHashedPassword(user, user.PassworHash, password) == PasswordVerificationResult.Failed) 
            return false;

        return true;
    }
}
