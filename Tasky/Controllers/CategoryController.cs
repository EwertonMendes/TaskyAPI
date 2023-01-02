using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Tasky.Interfaces.Services;
using Tasky.Controllers.BaseControllers;
using Tasky.Dtos.Request.Category;

namespace Tasky.Controllers;

[Authorize]
[ApiController]
public class CategoryController : AuthorizedBaseControllerV1
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int userId)
    {
        var allCategories = await _categoryService.ListCategories(userId);
        return Ok(allCategories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int userId, CategoryRequestDto id)
    {
        var category = await _categoryService.GetCategoryById(userId, id.CategoryId);
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int userId, [FromBody] CategoryModificationRequestDto dto)
    {
        var response = await _categoryService.CreateCategory(userId, dto);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int userId, int id, [FromBody] CategoryModificationRequestDto dto)
    {
        var updatedCategory = await _categoryService.UpdateCategory(userId, id, dto);
        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int userId, int id)
    {
        var wasCategoryDeleted = await _categoryService.DeleteCategory(userId, id);
        return Ok(wasCategoryDeleted);

    }
}
