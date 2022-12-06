using Microsoft.AspNetCore.Mvc;
using Tasky.Data;
using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Interfaces;
using Tasky.Models;

namespace Tasky.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryRepository)
        {
            _categoryService = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allCategories = _categoryService.ListCategories();
            return Ok(allCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = _categoryService.GetCategoryById(id);
                return Ok(category);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryRequestDto dto)
        {
            try
            {
                var response = _categoryService.CreateCategory(dto);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }   
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CategoryRequestDto dto)
        {
            try
            {
                var updatedCategory = _categoryService.UpdateCategory(id, dto);
                return Ok(updatedCategory);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                var wasCategoryDeleted = _categoryService.DeleteCategory(id);

                return Ok(wasCategoryDeleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
