using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Interfaces;
using Tasky.Models;

namespace Tasky.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public CategoryResponseDto CreateCategory(CategoryRequestDto category)
        {
            var createdCategory = _repository.AddNewCategory(category);

            return new CategoryResponseDto
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name,
                CreatedDate = createdCategory.CreatedDate,
            };
        }

        public bool DeleteCategory(int id)
        {
            return _repository.RemoveCategory(id);
        }

        public CategoryResponseDto GetCategoryById(int id)
        {
            var category = _repository.GetById(id);

            if (category == null) throw new Exception($"Category with id {id} not found");

            var categoryDto = new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate
            };

            return categoryDto;
        }

        public IEnumerable<Category> ListCategories()
        {
            var allCategories = _repository.GetAll().ToList();

            return allCategories;
        }

        public CategoryResponseDto UpdateCategory(string id, CategoryRequestDto category)
        {
            var updatedCategory = _repository.UpdateCategory(category, id);

            if (updatedCategory == null) throw new Exception($"Category with id {id} not found");

            return new CategoryResponseDto
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                CreatedDate = updatedCategory.CreatedDate,
            };
        }
    }
}
