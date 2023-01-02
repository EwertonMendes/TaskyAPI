using AutoMapper;
using FluentValidation;
using System.Diagnostics;
using Tasky.Dtos.Request.TaskList;
using Tasky.Dtos.Request.User;
using Tasky.Dtos.Response;
using Tasky.Interfaces.Repositories;
using Tasky.Interfaces.Services;
using Tasky.Models;

namespace Tasky.Services;

public class TaskListService : GenericService, ITaskListService
{
    private readonly ITaskListRepository _repository;
    private readonly IValidator<TaskListRequestDto> _taskListValidator;
    private readonly IValidator<TaskListModificationRequestDto> _taskListModificationValidator;
    private readonly IMapper _mapper;

    public TaskListService(ITaskListRepository repository,
        IValidator<TaskListRequestDto> taskListValidator,
        IValidator<TaskListModificationRequestDto> taskListModificationValidator,
        IValidator<UserRequestDto> userValidator,
        IMapper mapper) : base(userValidator)
    {
        _repository = repository;
        _taskListValidator = taskListValidator;
        _taskListModificationValidator = taskListModificationValidator;
        _mapper = mapper;
    }

    public async Task<TaskListResponseDto> CreateTaskList(int userId, TaskListModificationRequestDto taskListDto)
    {
        await ValidateUserIdAndEntityFields(userId, taskListDto);

        var createdTaskList = await _repository.AddTaskListByUser(userId, taskListDto);

        return GetMappedResponse(createdTaskList);
    }

    public async Task<bool> DeleteTaskList(int userId, int id)
    {
        await ValidateUserAndEntityIds(userId, id);

        var wasRemoved = await _repository.RemoveTaskListByUser(userId, id);

        return wasRemoved;
    }

    public async Task<TaskListResponseDto> GetTaskListById(int userId, int id)
    {
        await ValidateUserAndEntityIds(userId, id);

        var taskList = await _repository.GetByIdByUser(userId, id);

        return GetMappedResponse(taskList);
    }

    public async Task<IEnumerable<TaskListResponseDto>> GetAllLists(int userId)
    {
        await ValidateUserId(userId);

        var allTaskLists = await _repository.GetAllTaskListsForUser(userId);

        var responseList = new List<TaskListResponseDto>();

        foreach (var taskList in allTaskLists.ToEnumerable())
        {
            responseList.Add(GetMappedResponse(taskList));
        }

        return responseList;
    }

    public async Task<TaskListResponseDto> UpdateTaskList(int userId, int id, TaskListModificationRequestDto taskListDto)
    {
        await ValidateEntireEntity(userId, id, taskListDto);

        var updatedTaskList = await _repository.UpdateTaskListByUser(userId, id, taskListDto);

        return GetMappedResponse(updatedTaskList);
    }

    private async Task ValidateUserAndEntityIds(int userId, int entityId)
    {
        await this.ValidateUserId(userId);
        await _taskListValidator.ValidateAndThrowAsync(new TaskListRequestDto { TaskListId = entityId });
    }

    private async Task ValidateEntireEntity(int userId, int entityId, TaskListModificationRequestDto dto)
    {
        await ValidateUserAndEntityIds(userId, entityId);
        await _taskListModificationValidator.ValidateAndThrowAsync(dto);
    }

    private async Task ValidateUserIdAndEntityFields(int userId, TaskListModificationRequestDto dto)
    {
        await this.ValidateUserId(userId);
        await _taskListModificationValidator.ValidateAndThrowAsync(dto);
    }

    private TaskListResponseDto GetMappedResponse(TaskList taskListModel)
    {
        return _mapper.Map<TaskListResponseDto>(taskListModel, opt =>
        opt.AfterMap((src, dest) => dest.Category = new CategoryResponseDto
        {
            Id = taskListModel.CategoryId,
            Name = taskListModel.Category.Name,
            CreatedDate = taskListModel.Category.CreatedDate,
        }));
    }
}
