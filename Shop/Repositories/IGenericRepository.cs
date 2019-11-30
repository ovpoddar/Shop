using System.Linq;

namespace Shop.Repositories
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        void Add(T model);
        T GetById(int id);
    }
}
