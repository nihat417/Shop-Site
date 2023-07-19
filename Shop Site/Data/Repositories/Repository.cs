using Microsoft.EntityFrameworkCore;
using Shop_Site.Models;

namespace Shop_Site.Data.Repositories
{
    public class Repository<T>:IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> dbset;

        public Repository(AppDbContext context, DbSet<T> dbset)
        {
            this.context = context;
            this.dbset = dbset;
        }

        public void Delete(int id)
        {
            var entity = dbset.FirstOrDefault(x => x.Id == id);
            if (entity is not null)
                dbset.Remove(entity);
        }

        public void Delete(T entity)
        {
            dbset.Remove(entity);
        }
    }
}
