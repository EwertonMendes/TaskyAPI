using Tasky.Dtos.Request;
using Tasky.Models;

namespace Tasky.Interfaces.Repositories;

public interface ITaskListRepository
{
    TaskList? GetTaskListById(int id);
    IEnumerable<TaskList> GetAllTaskLists();
    TaskList? AddNewTaskList(TaskListRequestDto taskListDto);
    TaskList? UpdateTaskList(TaskListRequestDto taskListDto, int id);
    bool RemoveTaskList(int id);
}
