using Microsoft.EntityFrameworkCore;
using Tasky.Data;
using Tasky.Dtos.Request;
using Tasky.Interfaces;
using Tasky.Models;

namespace Tasky.Repositories
{
    public class TaskListRepository : GenericRepository<TaskList>, ITaskListRepository
    {
        private readonly ICategoryService _categoryService;

        public TaskListRepository(TaskyContext context, ICategoryService categoryService) : base(context)
        {
            _categoryService = categoryService;
        }

        public override IEnumerable<TaskList> GetAll()
        {
            return _context.TaskList.Include(x => x.Category).ToList();
        }

        public TaskList AddNewTaskList(TaskListRequestDto taskListDto)
        {
            CheckCategoryId(taskListDto.CategoryId);

            var newCategory = new TaskList
            {
                Name = taskListDto.Name,
                CategoryId = taskListDto.CategoryId,
                Checked = taskListDto.Checked
            };

            var addedEntity = _context.TaskList.Add(newCategory).Entity;
            _context.SaveChanges();

            return addedEntity;
        }

        public bool RemoveTaskList(int id)
        {
            var taskList = this.GetById(id);

            if (taskList == null) return false;

            _context.Remove(taskList);
            _context.SaveChanges();

            return true;
        }

        public TaskList? UpdateTaskList(TaskListRequestDto taskListDto, string id)
        {
            var taskList = this.GetById(Int32.Parse(id));

            if (taskList == null) return null;

            //TODO: Improve category validation
            CheckCategoryId(taskList.CategoryId);

            taskList.Name = taskListDto.Name;
            taskList.CategoryId = taskListDto.CategoryId;
            taskList.Checked = taskListDto.Checked;

            _context.TaskList.Update(taskList);
            _context.SaveChanges();

            return taskList;
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
