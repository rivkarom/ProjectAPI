using ChineseAuctionProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using StoreApi.Data;

namespace ChineseAuctionProject.Repositories
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
           .Include(c => c.Gifts)
           .ToListAsync();
        }
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                        .Include(c => c.Gifts)
                        .FirstOrDefaultAsync(c => c.Id == id);

        }
        public async Task<Category> CreateAsync(Category category)
        {
            category.CreatedAt = DateTime.UtcNow;
            category.UpdatedAt = DateTime.UtcNow;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;

        }
        public async Task<Category?> UpdateAsync(Category category)
        {
            var categoruU = await _context.Categories.FindAsync(category.Id);
            if (categoruU == null) return null;

            _context.Entry(categoruU).CurrentValues.SetValues(category);
            categoruU.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return categoruU;

        }
        public async Task<bool> DeleteAsync(int id)
        {
            var categoryD = await _context.Categories.FindAsync(id);
            if (categoryD == null) return false;
            _context.Remove(categoryD);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ExistsAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null) return false;
            return true;
        }


    }
}
