using Shop_Site.Models;

namespace Shop_Site.Data.Repositories
{
    public interface IRepository<T>where T : BaseEntity
    {
        void Delete(int id);
        void Delete(T entity);
    }
}
