using AutoMapper;
using Tasky.Dtos.Request;
using Tasky.Interfaces.Repositories;
using Tasky.Models;

namespace Tasky.Repositories
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly IGenericRepository<TaskList> _genericRepository;
        private readonly IMapper _mapper;

        public TaskListRepository(IGenericRepository<TaskList> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<IAsyncEnumerable<TaskList>> GetAllTaskLists()
        {
            return await _genericRepository.GetAll(x => x.Category);
        }

        public async Task<TaskList?> GetTaskListById(int id)
        {
            return await _genericRepository.GetById(id, x => x.Category);
        }

        public async Task<TaskList> AddNewTaskList(TaskListRequestDto taskListDto)
        {
            var newCategory = _mapper.Map<TaskList>(taskListDto);

            return await _genericRepository.Add(newCategory);
        }

        public async Task<bool> RemoveTaskList(int id)
        {
            var taskList = await this.GetTaskListById(id);

            if (taskList == null) return false;

            await _genericRepository.Remove(taskList);

            return true;
        }

        public async Task<TaskList> UpdateTaskList(TaskListRequestDto taskListDto, int id)
        {
            var taskList = await this.GetTaskListById(id);

            if (taskList == null) return null;

            taskList.Name = taskListDto.Name;

            if (taskListDto.CategoryId != taskList.CategoryId)
            {
                taskList.CategoryId = taskListDto.CategoryId;
            }

            return await _genericRepository.Update(taskList);
        }
    }
}
