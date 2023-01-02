using AutoMapper;
using Tasky.Dtos.Request.TaskList;
using Tasky.Interfaces.Repositories;
using Tasky.Models;

namespace Tasky.Repositories;

public class TaskListRepository : ITaskListRepository
{
    private readonly IGenericRepository<TaskList> _genericRepository;
    private readonly IMapper _mapper;

    public TaskListRepository(IGenericRepository<TaskList> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }

    public async Task<IAsyncEnumerable<TaskList>> GetAllTaskListsForUser(int userId)
    {
        var taskLists = await _genericRepository.GetAll(c => c.UserId == userId, a => a.Category);
        return taskLists;
    }

    public async Task<TaskList?> GetByIdByUser(int userId, int id)
    {
        return await _genericRepository.FindBy(
            c => c.UserId == userId && c.Id == id,
            c => c.Category);
    }

    public async Task<TaskList> AddTaskListByUser(int userId, TaskListModificationRequestDto taskListDto)
    {
        var newTaskList = _mapper.Map<TaskList>(taskListDto, opt =>
            opt.AfterMap((src, dest) => dest.UserId = userId));

        return await _genericRepository.Add(newTaskList, prop => prop.Category);
    }

    public async Task<bool> RemoveTaskListByUser(int userId, int id)
    {
        var taskList = await _genericRepository.FindBy(c => c.UserId == userId && c.Id == id);

        if (taskList == null) return false;

        await _genericRepository.Remove(taskList);

        return true;
    }

    public async Task<TaskList> UpdateTaskListByUser(int userId, int id, TaskListModificationRequestDto taskListDto)
    {
        var taskList = await _genericRepository.FindBy(c => c.UserId == userId && c.Id == id);

        if (taskList == null) return null;

        taskList.Name = taskListDto.Name;

        if (taskListDto.CategoryId != taskList.CategoryId)
        {
            taskList.CategoryId = taskListDto.CategoryId;
        }

        return await _genericRepository.Update(taskList, t => t.Category);
    }

    public async Task<TaskList?> GetById(int id)
    {
        return await _genericRepository.GetById(id);
    }
}
