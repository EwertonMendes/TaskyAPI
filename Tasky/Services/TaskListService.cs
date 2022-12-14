using AutoMapper;
using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Interfaces.Repositories;
using Tasky.Interfaces.Services;

namespace Tasky.Services
{
    public class TaskListService : ITaskListService
    {
        private readonly ITaskListRepository _repository;
        private readonly IMapper _mapper;
        public TaskListService(ITaskListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TaskListResponseDto> CreateTaskList(TaskListRequestDto taskList)
        {
            var createdTaskList = await _repository.AddNewTaskList(taskList);

            return _mapper.Map<TaskListResponseDto>(createdTaskList);
        }

        public async Task<bool> DeleteTaskList(int id)
        {
            return await _repository.RemoveTaskList(id);
        }

        public async Task<TaskListResponseDto> GetTaskListById(int id)
        {
            var taskList = await _repository.GetTaskListById(id);

            if (taskList == null) throw new Exception($"Task List with id: {id} not found");

            return _mapper.Map<TaskListResponseDto>(taskList);
        }

        public async Task<IEnumerable<TaskListResponseDto>> GetAllLists()
        {
            var allTaskLists = await _repository.GetAllTaskLists();

            var responseList = new List<TaskListResponseDto>();

            foreach(var taskList in allTaskLists.ToEnumerable())
            {
                responseList.Add(_mapper.Map<TaskListResponseDto>(taskList));
            }

            return responseList;
        }

        public async Task<TaskListResponseDto> UpdateTaskList(int id, TaskListRequestDto taskList)
        {
            var updatedTaskList = await _repository.UpdateTaskList(taskList, id);

            if (updatedTaskList == null) throw new Exception($"Task List with id: {id} not found");

            return _mapper.Map<TaskListResponseDto>(updatedTaskList);
        }
    }
}
