using AutoMapper;
using Tasky.Dtos.Request;
using Tasky.Interfaces.Repositories;
using Tasky.Interfaces.Services;
using Tasky.Models;

namespace Tasky.Repositories
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly IGenericRepository<TaskList> _genericRepository;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public TaskListRepository(IGenericRepository<TaskList> genericRepository, ICategoryService categoryService, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _categoryService = categoryService;
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
            taskList.Checked = taskListDto.Checked;

            //TODO: improve this check
            if (taskListDto.CategoryId != taskList.CategoryId)
            {
                var category = await _categoryService.GetCategoryById(taskListDto.CategoryId);
                taskList.CategoryId = category.Id;
            }

            return await _genericRepository.Update(taskList);
        }
    }
}
