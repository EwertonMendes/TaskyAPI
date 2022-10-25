using Microsoft.AspNetCore.Mvc;
using Tasky.Data;
using Tasky.Dtos;
using Tasky.Interfaces;
using Tasky.Models;

namespace Tasky.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allCategories = _categoryRepository.GetAll().ToList();
            return Ok(allCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = _categoryRepository.GetById(id);

            if(category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
        {
            try
            {
                _categoryRepository.AddNewCategory(dto);

                return Ok("Created");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryDto dto)
        {

            try
            {
                _categoryRepository.UpdateCategory(dto);
                return Ok("Updated");
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
                _categoryRepository.RemoveCategory(id);

                return Ok("Removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
