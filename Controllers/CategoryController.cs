using expense_tracker_api.Domain;
using expense_tracker_api.DTOs;
using expense_tracker_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace expense_tracker_api.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly TransactionDbContext _db;

        public CategoryController(TransactionDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _db.Categories
                .Select(c => new CategoryDTO 
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id) 
        {
            var category = await _db.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefaultAsync();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO request)
        {
            if (request == null)
                return BadRequest();

            var category = new Category { Name = request.Name };

            _db.Categories.Add(category);  
            await _db.SaveChangesAsync();

            var responseDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            };

            return Ok(responseDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {

            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO request, Guid id)
        {
            if (request == null)
                return BadRequest();

            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            category.Name = request.Name;
            await _db.SaveChangesAsync();

            var responseDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(responseDTO);
        }
    }
}
