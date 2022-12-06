using Microsoft.AspNetCore.Mvc;
using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Models;

namespace Tasky.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Category AddNewCategory(CategoryRequestDto categoryDto);
    Category UpdateCategory(CategoryRequestDto categoryDto, string id);
    bool RemoveCategory(int id);
}
