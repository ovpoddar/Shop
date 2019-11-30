using Shop.Entities;
using System;
using System.Linq;

namespace Shop.Repositories
{
    public class GenericRepositories<T> : IGenericRepository<T> where T : BaseEntity
    {
        public readonly ApplicationDbContext DbContext;

        public GenericRepositories(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(DbContext));
        }

        public void Add(T model)
        {
            DbContext.Add(model);
            DbContext.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public T GetById(int id)
        {
            return DbContext.Set<T>().Find(id);
        }
    }
}
