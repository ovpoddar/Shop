using System.Linq;

namespace Shop.Repositories
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        void Add(T model);
        void Delete(T model);
        void Save();
        void Update(T model);
    }
}
