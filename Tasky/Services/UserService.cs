using AutoMapper;
using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Exceptions;
using Tasky.Interfaces.Repositories;
using Tasky.Interfaces.Services;

namespace Tasky.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserResponseDto> CreateUser(UserRequestDto userDto)
    {
        var createdUser = await _repository.AddNewUser(userDto);
        return _mapper.Map<UserResponseDto>(createdUser);
    }

    public async Task<UserResponseDto> GetCategoryById(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null) throw new NotFoundException($"A User with id {id} was not found");

        return _mapper.Map<UserResponseDto>(user);
    }
}
