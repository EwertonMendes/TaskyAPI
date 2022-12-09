using Microsoft.AspNetCore.Mvc;
using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Models;

namespace Tasky.Interfaces;

public interface ITaskListRepository : IGenericRepository<TaskList>
{
    TaskList AddNewTaskList(TaskListRequestDto taskListDto);
    TaskList? UpdateTaskList(TaskListRequestDto taskListDto, string id);
    bool RemoveTaskList(int id);
}
