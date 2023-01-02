using Tasky.Dtos.Request.Category;
using Tasky.Dtos.Response;

namespace Tasky.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> ListCategories(int userId);
        Task<CategoryResponseDto> GetCategoryById(int userId, int id);
        Task<CategoryResponseDto> CreateCategory(int userId, CategoryModificationRequestDto category);
        Task<CategoryResponseDto> UpdateCategory(int userId, int id, CategoryModificationRequestDto category);
        Task<bool> DeleteCategory(int userId, int id);
    }
}
