using Microsoft.EntityFrameworkCore;
using Shop_Site.Data;
using Shop_Site.Models;

namespace Shop_Site.Repository
{
    public class BrandRepository: IBrandRepository
    {
        private readonly AppDbContext _context;

        public BrandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.Brands.ToListAsync();
        }
    }
}
