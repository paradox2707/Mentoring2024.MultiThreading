using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestWeb.Data;
using RestWeb.Interfaces;
using RestWeb.Models;

namespace RestWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems([FromQuery] int? categoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var items = await _itemService.GetItemsAsync(categoryId, page, pageSize);
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> AddItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdItem = await _itemService.AddItemAsync(item);
            return CreatedAtAction(nameof(GetItems), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedItem = await _itemService.UpdateItemAsync(id, item);
            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _itemService.DeleteItemAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
