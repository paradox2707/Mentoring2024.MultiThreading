using Microsoft.EntityFrameworkCore;
using RestWeb.Data;
using RestWeb.Interfaces;
using RestWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWeb.Services
{
    public class ItemService : IItemService
    {
        private readonly CatalogContext _context;

        public ItemService(CatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(int? categoryId, int page, int pageSize)
        {
            var query = _context.Items.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(i => i.CategoryId == categoryId);
            }

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            if (await _context.Items.AnyAsync(i => i.Name == item.Name && i.CategoryId == item.CategoryId))
            {
                throw new InvalidOperationException("Item with the same name already exists in this category.");
            }

            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> UpdateItemAsync(int id, Item item)
        {
            if (id != item.Id)
            {
                throw new ArgumentException("Item ID mismatch.");
            }

            if (!await _context.Items.AnyAsync(i => i.Id == id))
            {
                throw new KeyNotFoundException("Item not found.");
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await _context.Items.Include(i => i.Category).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return false;
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
