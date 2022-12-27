using AutoMapper;
using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Models;

namespace Tasky.Mapping;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<Category, CategoryResponseDto>()
            .ReverseMap();

        CreateMap<Category, CategoryRequestDto>()
            .ReverseMap();

        CreateMap<TaskList, TaskListResponseDto>()
            .ReverseMap();

        CreateMap<TaskList, TaskListRequestDto>()
            .ReverseMap();

        CreateMap<User, UserRequestDto>()
            .ReverseMap();

        CreateMap<User, UserResponseDto>()
            .ReverseMap();
    }
}
