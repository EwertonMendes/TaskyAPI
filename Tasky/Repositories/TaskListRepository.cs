using AutoMapper;
using Tasky.Dtos.Request;
using Tasky.Interfaces;
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

        public IEnumerable<TaskList?> GetAllTaskLists()
        {
            return _genericRepository.GetAll(includes: x => x.Category).ToList();
        }

        public TaskList? GetTaskListById(int id)
        {
            return _genericRepository.GetById(id, x => x.Category);
        }

        public TaskList? AddNewTaskList(TaskListRequestDto taskListDto)
        {
            CheckCategoryId(taskListDto.CategoryId);

            var newCategory = _mapper.Map<TaskList>(taskListDto);

            return _genericRepository.Add(newCategory);
        }

        public bool RemoveTaskList(int id)
        {
            var taskList = this.GetTaskListById(id);

            if (taskList == null) return false;

            _genericRepository.Remove(taskList);

            return true;
        }

        public TaskList? UpdateTaskList(TaskListRequestDto taskListDto, int id)
        {
            var taskList = this.GetTaskListById(id);

            if (taskList == null) return null;

            //TODO: Improve category validation
            CheckCategoryId(taskList.CategoryId);

            taskList.Name = taskListDto.Name;
            taskList.CategoryId = taskListDto.CategoryId;
            taskList.Checked = taskListDto.Checked;

            return _genericRepository.Update(taskList);
        }

        private void CheckCategoryId(int categoryId)
        {
            var category = _categoryService.GetCategoryById(categoryId);

            if(category == null)
            {
                throw new Exception($"Category with id {categoryId} not found");
            }
        }
    }
}
