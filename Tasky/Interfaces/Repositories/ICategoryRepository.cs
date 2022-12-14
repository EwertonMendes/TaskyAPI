using Tasky.Dtos.Request;
using Tasky.Models;

namespace Tasky.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetById(int id);
    Task<IAsyncEnumerable<Category>> GetAllCategories();
    Task<Category> AddNewCategory(CategoryRequestDto categoryDto);
    Task<Category> UpdateCategory(CategoryRequestDto categoryDto, int id);
    Task<bool> RemoveCategory(int id);
}
