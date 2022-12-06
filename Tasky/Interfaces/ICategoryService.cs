using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Models;

namespace Tasky.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> ListCategories();
        CategoryResponseDto GetCategoryById(int id);
        CategoryResponseDto CreateCategory(CategoryRequestDto category);
        CategoryResponseDto UpdateCategory(string id, CategoryRequestDto category);
        bool DeleteCategory(int id);
    }
}
