using Tasky.Dtos.Request.TaskList;
using Tasky.Models;

namespace Tasky.Interfaces.Repositories;

public interface ITaskListRepository
{
    Task<TaskList?> GetByIdByUser(int userId, int id);
    Task<TaskList?> GetById(int id);
    Task<IAsyncEnumerable<TaskList>> GetAllTaskListsForUser(int userId);
    Task<TaskList> AddTaskListByUser(int userId, TaskListModificationRequestDto taskListDto);
    Task<TaskList> UpdateTaskListByUser(int userId, int id, TaskListModificationRequestDto taskListDto);
    Task<bool> RemoveTaskListByUser(int userId, int id);
}
