using Tasky.Dtos.Request.TaskList;
using Tasky.Dtos.Response;
using Tasky.Models;

namespace Tasky.Interfaces.Services;

public interface ITaskListService
{
    Task<IEnumerable<TaskListResponseDto>> GetAllLists(int userId);
    Task<TaskListResponseDto> GetTaskListById(int userId, int id);
    Task<TaskListResponseDto> CreateTaskList(int userId, TaskListModificationRequestDto taskList);
    Task<TaskListResponseDto> UpdateTaskList(int userId, int id, TaskListModificationRequestDto taskList);
    Task<bool> DeleteTaskList(int userId, int id);
}
