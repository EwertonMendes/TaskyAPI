using Tasky.Dtos.Request.Category;
using Tasky.Models;

namespace Tasky.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdByUser(int userId, int id);
    Task<Category?> GetById(int id);
    Task<IAsyncEnumerable<Category>> GetAllCategoriesForUser(int userId);
    Task<Category> AddCategoryByUser(int userId, CategoryModificationRequestDto categoryDto);
    Task<Category> UpdateCategoryByUser(int userId, CategoryModificationRequestDto categoryDto, int id);
    Task<bool> RemoveCategoryByUser(int userId, int id);
}
