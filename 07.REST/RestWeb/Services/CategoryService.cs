using Microsoft.EntityFrameworkCore;
using RestWeb.Data;
using RestWeb.Interfaces;
using RestWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWeb.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CatalogContext _context;

        public CategoryService(CatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.Include(c => c.Items).ToListAsync();
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            if (await _context.Categories.AnyAsync(c => c.Name == category.Name))
            {
                throw new InvalidOperationException("Category with the same name already exists.");
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            if (id != category.Id)
            {
                throw new ArgumentException("Category ID mismatch.");
            }

            if (!await _context.Categories.AnyAsync(c => c.Id == id))
            {
                throw new KeyNotFoundException("Category not found.");
            }

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await _context.Categories.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
