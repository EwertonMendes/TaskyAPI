using Microsoft.AspNetCore.Mvc;
using Tasky.Data;
using Tasky.Dtos;
using Tasky.Interfaces;
using Tasky.Models;

namespace Tasky.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(Context context) : base(context){}

        public void AddNewCategory(CategoryDto categoryDto)
        {
            var newCategory = new Category
            {
                Name = categoryDto.Name,
                CreatedDate = categoryDto.CreatedDate,
            };

           _context.Category.Add(newCategory);
           _context.SaveChanges();
        }

        public void RemoveCategory(int id)
        {
            var category = this.GetById(id);

            if (category == null) return;

            _context.Remove(category);
            _context.SaveChanges();

        }

        public void UpdateCategory(CategoryDto categoryDto)
        {
            var category = this.GetById(categoryDto.Id);

            category.Name = categoryDto.Name;
            category.CreatedDate = categoryDto.CreatedDate;

            _context.Category.Update(category);
            _context.SaveChanges();
        }
    }
}
