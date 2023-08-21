using Microsoft.EntityFrameworkCore;
using Shop_Site.Data;
using Shop_Site.Models;

namespace Shop_Site.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Products> GetByIdAsync(string id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddAsync(Products product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Products product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Products product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
