using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Interfaces;
using Tasky.Models;
using Tasky.Utilities;

namespace Tasky.Services
{
    public class TaskListService : ITaskListService
    {
        private readonly ITaskListRepository _repository;
        public TaskListService(ITaskListRepository repository)
        {
            _repository = repository;
        }

        public TaskListResponseDto CreateTaskList(TaskListRequestDto taskList)
        {
            var createdTaskList = _repository.AddNewTaskList(taskList);

            return new TaskListResponseDto
            {
                Id = createdTaskList.Id,
                Name = createdTaskList.Name,
                Category = createdTaskList.Category.ToDto(),
                Checked = createdTaskList.Checked,
            };
        }

        public bool DeleteTaskList(int id)
        {
            return _repository.RemoveTaskList(id);
        }

        public TaskListResponseDto GetTaskListById(int id)
        {
            var taskList = _repository.GetById(id);

            if (taskList == null) throw new Exception($"Task List with id: {id} not found");

            return new TaskListResponseDto
            {
                Id = taskList.Id,
                Name = taskList.Name,
                Category = taskList.Category.ToDto(),
                Checked = taskList.Checked,
            };
        }

        public IEnumerable<TaskListResponseDto> GetAllLists()
        {
            var allTaskLists = _repository.GetAll().ToList();

            var responseList = new List<TaskListResponseDto>();

            foreach(var taskList in allTaskLists)
            {
                responseList.Add(new()
                {
                    Id = taskList.Id,
                    Name = taskList.Name,
                    Category = taskList.Category.ToDto(),
                    Checked = taskList.Checked,
                });
            }

            return responseList;
        }

        public TaskListResponseDto UpdateTaskList(string id, TaskListRequestDto taskList)
        {
            var updatedTaskList = _repository.UpdateTaskList(taskList, id);

            if (updatedTaskList == null) throw new Exception($"Task List with id: {id} not found");

            return new TaskListResponseDto
            {
                Id = updatedTaskList.Id,
                Name = updatedTaskList.Name,
                Category = updatedTaskList.Category.ToDto(),
                Checked = updatedTaskList.Checked,
            };
        }
    }
}
