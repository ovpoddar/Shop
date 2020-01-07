using System.Linq;

namespace Shop.Repositories
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T model);
        void Delete(T model);
        void save();
    }
}
