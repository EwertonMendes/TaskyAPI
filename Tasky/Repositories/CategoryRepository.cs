using Tasky.Data;
using Tasky.Dtos.Request;
using Tasky.Interfaces;
using Tasky.Models;

namespace Tasky.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(TaskyContext context) : base(context){}

        public Category AddNewCategory(CategoryRequestDto categoryDto)
        {
            var newCategory = new Category
            {
                Name = categoryDto.Name
            };

           _context.Category.Add(newCategory);
           _context.SaveChanges();

            return newCategory;
        }

        public bool RemoveCategory(int id)
        {
            var category = this.GetById(id);

            if (category == null) return false;

            _context.Remove(category);
            _context.SaveChanges();

            return true;
        }

        public Category? UpdateCategory(CategoryRequestDto categoryDto, string id)
        {
            var category = this.GetById(Int32.Parse(id));

            if (category == null) return null;

            category.Name = categoryDto.Name;

            _context.Category.Update(category);
            _context.SaveChanges();

            return category;
        }
    }
}
