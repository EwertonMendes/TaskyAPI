using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Models;

namespace Tasky.Interfaces.Services
{
    public interface ITaskListService
    {
        Task<IEnumerable<TaskListResponseDto>> GetAllLists();
        Task<TaskListResponseDto> GetTaskListById(int id);
        Task<TaskListResponseDto> CreateTaskList(TaskListRequestDto taskList);
        Task<TaskListResponseDto> UpdateTaskList(int id, TaskListRequestDto taskList);
        Task<bool> DeleteTaskList(int id);
    }
}
