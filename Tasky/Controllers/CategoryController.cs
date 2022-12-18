using Microsoft.AspNetCore.Mvc;
using Tasky.Dtos.Request;
using Tasky.Interfaces.Services;

namespace Tasky.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allCategories = await _categoryService.ListCategories();
            return Ok(allCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
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
        public async Task<IActionResult> DeleteById(int id)
        {
            var wasCategoryDeleted = await _categoryService.DeleteCategory(id);
            return Ok(wasCategoryDeleted);

        }
    }
}
