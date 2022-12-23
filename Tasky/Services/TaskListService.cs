using AutoMapper;
using FluentValidation;
using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Exceptions;
using Tasky.Interfaces.Repositories;
using Tasky.Interfaces.Services;

namespace Tasky.Services
{
    public class TaskListService : ITaskListService
    {
        private readonly ITaskListRepository _repository;
        private readonly IValidator<TaskListRequestDto> _taskListValidator;
        private readonly IMapper _mapper;

        public TaskListService(ITaskListRepository repository, IValidator<TaskListRequestDto> taskListValidator, IMapper mapper)
        {
            _repository = repository;
            _taskListValidator = taskListValidator;
            _mapper = mapper;
        }

        public async Task<TaskListResponseDto> CreateTaskList(TaskListRequestDto taskList)
        {
            await _taskListValidator.ValidateAndThrowAsync(taskList);

            var createdTaskList = await _repository.AddNewTaskList(taskList);

            return _mapper.Map<TaskListResponseDto>(createdTaskList);
        }

        public async Task<bool> DeleteTaskList(int id)
        {
            var wasRemoved = await _repository.RemoveTaskList(id);

            if (wasRemoved == false) throw new NotFoundException($"Task List with id {id} not found");

            return wasRemoved;
        }

        public async Task<TaskListResponseDto> GetTaskListById(int id)
        {
            var taskList = await _repository.GetTaskListById(id);

            if (taskList == null) throw new NotFoundException($"Task List with id {id} not found");

            return _mapper.Map<TaskListResponseDto>(taskList);
        }

        public async Task<IEnumerable<TaskListResponseDto>> GetAllLists()
        {
            var allTaskLists = await _repository.GetAllTaskLists();

            var responseList = new List<TaskListResponseDto>();

            foreach (var taskList in allTaskLists.ToEnumerable())
            {
                responseList.Add(_mapper.Map<TaskListResponseDto>(taskList));
            }

            return responseList;
        }

        public async Task<TaskListResponseDto> UpdateTaskList(int id, TaskListRequestDto taskList)
        {
            await _taskListValidator.ValidateAndThrowAsync(taskList);

            var updatedTaskList = await _repository.UpdateTaskList(taskList, id);

            if (updatedTaskList == null) throw new NotFoundException($"Task List with id {id} not found");

            return _mapper.Map<TaskListResponseDto>(updatedTaskList);
        }
    }
}
