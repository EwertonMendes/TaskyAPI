using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasky.Controllers.BaseControllers;
using Tasky.Dtos.Request.TaskList;
using Tasky.Interfaces.Services;

namespace Tasky.Controllers;

[Authorize]
[ApiController]
public class TaskListController : AuthorizedBaseControllerV1
{
    public readonly ITaskListService _taskListService;
    public TaskListController(ITaskListService taskListService)
    {
        _taskListService = taskListService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int userId)
    {
        var response = await _taskListService.GetAllLists(userId);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int userId, int id)
    {
        var response = await _taskListService.GetTaskListById(userId, id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int userId, [FromBody] TaskListModificationRequestDto dto)
    {
        var response = await _taskListService.CreateTaskList(userId, dto);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int userId, int id, [FromBody] TaskListModificationRequestDto dto)
    {
        var response = await _taskListService.UpdateTaskList(userId, id, dto);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int userId, int id)
    {
        var response = await _taskListService.DeleteTaskList(userId, id);
        return Ok(response);
    }
}
