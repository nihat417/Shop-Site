using Shop_Site.Models;

namespace Shop_Site.Repository
{
    public interface IProductRepository
    {
        Task<Products> GetByIdAsync(string id);
        Task<IEnumerable<Products>> GetAllAsync();
        Task AddAsync(Products product);
        Task UpdateAsync(Products product);
        Task DeleteAsync(Products product);
    }
}
