using Tasky.Dtos.Request;
using Tasky.Models;

namespace Tasky.Interfaces.Repositories;

public interface ITaskListRepository
{
    Task<TaskList?> GetTaskListById(int id);
    Task<IAsyncEnumerable<TaskList>> GetAllTaskLists();
    Task<TaskList> AddNewTaskList(TaskListRequestDto taskListDto);
    Task<TaskList> UpdateTaskList(TaskListRequestDto taskListDto, int id);
    Task<bool> RemoveTaskList(int id);
}
