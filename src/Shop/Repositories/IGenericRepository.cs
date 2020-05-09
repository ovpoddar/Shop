using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shop.Repositories
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        void Add(T model);
        void Delete(T model);
        void Save();
        void Update(T model);
        Task<int> SaveAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> expression);
    }
}
