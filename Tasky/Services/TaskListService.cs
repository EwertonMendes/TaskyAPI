using AutoMapper;
using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Interfaces;

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

        public TaskListResponseDto CreateTaskList(TaskListRequestDto taskList)
        {
            var createdTaskList = _repository.AddNewTaskList(taskList);

            return _mapper.Map<TaskListResponseDto>(createdTaskList);
        }

        public bool DeleteTaskList(int id)
        {
            return _repository.RemoveTaskList(id);
        }

        public TaskListResponseDto GetTaskListById(int id)
        {
            var taskList = _repository.GetById(id);

            if (taskList == null) throw new Exception($"Task List with id: {id} not found");

            return _mapper.Map<TaskListResponseDto>(taskList);
        }

        public IEnumerable<TaskListResponseDto> GetAllLists()
        {
            var allTaskLists = _repository.GetAll().ToList();

            var responseList = new List<TaskListResponseDto>();

            foreach(var taskList in allTaskLists)
            {
                responseList.Add(_mapper.Map<TaskListResponseDto>(taskList));
            }

            return responseList;
        }

        public TaskListResponseDto UpdateTaskList(string id, TaskListRequestDto taskList)
        {
            var updatedTaskList = _repository.UpdateTaskList(taskList, id);

            if (updatedTaskList == null) throw new Exception($"Task List with id: {id} not found");

            return _mapper.Map<TaskListResponseDto>(updatedTaskList);
        }
    }
}
