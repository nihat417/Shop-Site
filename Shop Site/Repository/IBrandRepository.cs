using Shop_Site.Models;

namespace Shop_Site.Repository
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllAsync();
    }
}
