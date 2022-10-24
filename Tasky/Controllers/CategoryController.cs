using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasky.Data;
using Tasky.Dtos;
using Tasky.Models;

namespace Tasky.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        public readonly Context _context;
        public CategoryController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_context.Category.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = _context.Category.FirstOrDefault(o => o.Id == id);

            if(category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
        {
            var newCategory = new Category
            {
                Name = dto.Name,
                CreatedDate = dto.CreatedDate,
            };

            try
            {
                var result = _context.Category.Add(newCategory);
                _context.SaveChanges();

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

            var category = _context.Category.FirstOrDefault(category => category.Id == dto.Id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = dto.Name;
            category.CreatedDate = dto.CreatedDate;
            
            _context.Category.Update(category);
            _context.SaveChanges();

            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var category = _context.Category.FirstOrDefault(o => o.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Remove(category);
            _context.SaveChanges();

            return Ok("Removed");
        }
    }
}
