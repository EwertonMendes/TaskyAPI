using AutoMapper;
using Tasky.Dtos.Request;
using Tasky.Dtos.Request.Category;
using Tasky.Dtos.Request.User;
using Tasky.Dtos.Response;
using Tasky.Models;

namespace Tasky.Mapping;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<Category, CategoryResponseDto>()
            .ReverseMap();

        CreateMap<Category, CategoryModificationRequestDto>();

        CreateMap<CategoryModificationRequestDto, Category>().ForMember(dest => dest.UserId,
            opt => opt.Ignore());

        CreateMap<TaskList, TaskListResponseDto>()
            .ReverseMap();

        CreateMap<TaskList, TaskListRequestDto>()
            .ReverseMap();

        CreateMap<User, UserModificationRequestDto>()
            .ReverseMap();

        CreateMap<User, UserResponseDto>()
            .ReverseMap();
    }
}
