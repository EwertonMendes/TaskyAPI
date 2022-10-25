using Microsoft.AspNetCore.Mvc;
using Tasky.Dtos;
using Tasky.Models;

namespace Tasky.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    void AddNewCategory(CategoryDto categoryDto);
    void UpdateCategory(CategoryDto categoryDto);
    void RemoveCategory(int id);
}
