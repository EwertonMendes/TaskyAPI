using Microsoft.AspNetCore.Mvc;
using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Models;

namespace Tasky.Interfaces;

public interface ICategoryRepository
{
    Category? GetById(int id);
    IEnumerable<Category> GetAllCategories();
    Category AddNewCategory(CategoryRequestDto categoryDto);
    Category? UpdateCategory(CategoryRequestDto categoryDto, int id);
    bool RemoveCategory(int id);
}
