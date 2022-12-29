using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tasky.Dtos.Request;
using Tasky.Interfaces.Repositories;
using Tasky.Models;

namespace Tasky.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<User> _genericRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserRepository(IMapper mapper, IGenericRepository<User> genericRepository, IPasswordHasher<User> passwordHasher)
    {
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _genericRepository = genericRepository;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _genericRepository.GetSingleAsync(x => x.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _genericRepository.GetSingleAsync(x => x.Id == id);
    }

    public async Task<User> AddNewUser(UserRequestDto userDto)
    {
        //var newUser = _mapper.Map<User>(userDto);
        var newUser = new User
        {
            Name = userDto.UserName,
            Email = userDto.Email,
            PassworHash = _passwordHasher.HashPassword(null, userDto.Password)
        };

        return await _genericRepository.Add(newUser);
    }
}
