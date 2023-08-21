using Microsoft.EntityFrameworkCore;
using Shop_Site.Data;
using Shop_Site.Models;

namespace Shop_Site.Repository
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
