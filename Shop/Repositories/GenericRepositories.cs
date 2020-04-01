using Shop.Entities;
using System;
using System.Linq;

namespace Shop.Repositories
{
    public class GenericRepositories<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepositories(ApplicationDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(_dbContext));

        public void Add(T model) =>
            _dbContext.Add(model);

        public void Delete(T model) =>
            _dbContext.Remove(model);

        public IQueryable<T> GetAll() =>
            _dbContext.Set<T>();

        public void save() =>
            _dbContext.SaveChanges();
    }
}
