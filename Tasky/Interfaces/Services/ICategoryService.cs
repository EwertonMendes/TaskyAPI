using Tasky.Dtos.Request;
using Tasky.Dtos.Response;

namespace Tasky.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> ListCategories();
        Task<CategoryResponseDto> GetCategoryById(int id);
        Task<CategoryResponseDto> CreateCategory(CategoryRequestDto category);
        Task<CategoryResponseDto> UpdateCategory(int id, CategoryRequestDto category);
        Task<bool> DeleteCategory(int id);
    }
}
