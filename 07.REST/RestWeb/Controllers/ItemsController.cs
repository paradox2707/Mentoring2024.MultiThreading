using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestWeb.Data;
using RestWeb.Models;

namespace RestWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly CatalogContext _context;

        public ItemsController(CatalogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems([FromQuery] int? categoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Items.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(i => i.CategoryId == categoryId);
            }

            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return items;
        }

        [HttpPost]
        public async Task<ActionResult<Item>> AddItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItems), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
