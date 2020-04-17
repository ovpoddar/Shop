using Shop.Entities;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Data;

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

        public void Save() =>
            _dbContext.SaveChanges();

        public void Update(T model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
