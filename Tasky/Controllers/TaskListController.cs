using Microsoft.AspNetCore.Mvc;
using Tasky.Dtos.Request;
using Tasky.Interfaces;

namespace Tasky.Controllers
{
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
            var response = _taskListService.GetAllLists();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = _taskListService.GetTaskListById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskListRequestDto dto)
        {
            try
            {
                var response = _taskListService.CreateTaskList(dto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] TaskListRequestDto dto)
        {
            try
            {
                var response = _taskListService.UpdateTaskList(id, dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                var response = _taskListService.DeleteTaskList(id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
