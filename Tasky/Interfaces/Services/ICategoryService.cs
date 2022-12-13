using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Models;

namespace Tasky.Interfaces.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryResponseDto> ListCategories();
        CategoryResponseDto GetCategoryById(int id);
        CategoryResponseDto CreateCategory(CategoryRequestDto category);
        CategoryResponseDto UpdateCategory(int id, CategoryRequestDto category);
        bool DeleteCategory(int id);
    }
}
