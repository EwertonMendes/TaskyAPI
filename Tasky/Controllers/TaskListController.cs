using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasky.Dtos.Request;
using Tasky.Interfaces.Services;

namespace Tasky.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class TaskListController : ControllerBase
    {
        public readonly ITaskListService _taskListService;
        public TaskListController(ITaskListService taskListService)
        {
            _taskListService = taskListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _taskListService.GetAllLists();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _taskListService.GetTaskListById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskListRequestDto dto)
        {
            var response = await _taskListService.CreateTaskList(dto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskListRequestDto dto)
        {
            var response = await _taskListService.UpdateTaskList(id, dto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var response = await _taskListService.DeleteTaskList(id);
            return Ok(response);
        }
    }
}
