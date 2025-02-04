using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestWeb.Data;
using RestWeb.Models;

namespace RestWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CatalogContext _context;

        public CategoriesController(CatalogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.Include(c => c.Items).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _context.Categories.AnyAsync(c => c.Name == category.Name))
            {
                return Conflict("Category with the same name already exists.");
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("Category ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _context.Categories.AnyAsync(c => c.Id == id))
            {
                return NotFound("Category not found.");
            }

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var updatedCategory = await _context.Categories.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == id);
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
