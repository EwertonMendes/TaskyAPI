using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Tasky.Dtos.Request;
using Tasky.Interfaces.Services;
using Tasky.Controllers.BaseControllers;

namespace Tasky.Controllers
{
    [Authorize]
    [ApiController]
    public class CategoryController : AuthorizedBaseControllerV1
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(int userId)
        {
            var allCategories = await _categoryService.ListCategories(userId);
            return Ok(allCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int userId, int id)
        {
            var category = await _categoryService.GetCategoryById(userId, id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryRequestDto dto)
        {
            var response = await _categoryService.CreateCategory(dto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequestDto dto)
        {
            var updatedCategory = await _categoryService.UpdateCategory(id, dto);
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int userId, int id)
        {
            var wasCategoryDeleted = await _categoryService.DeleteCategory(userId, id);
            return Ok(wasCategoryDeleted);

        }
    }
}
