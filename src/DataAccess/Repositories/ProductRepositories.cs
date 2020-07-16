using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ProductRepositories : IProductRepositories

    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepositories(ApplicationDbContext applicationDbContext) =>
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(_applicationDbContext));

        public IEnumerable<int> GetCategoryIds(int id)
        {
            return Getvalues(id);
        }

        //public IEnumerable<int> GetCategoryIds(int id) =>
        //    _applicationDbContext.Categories.FromSqlRaw($"exec spGetCategoryIds @id = {id}")
        //    .Select(p => p.Id)
        //    .ToList();

        private IEnumerable<int> Getvalues(int id)
        {
            var list = new List<int>();
            var parent = _applicationDbContext.Categories.Where(e => e.ParentId == id);
            list.Add(_applicationDbContext.Categories.First(e => e.Id == id).Id);
            if (parent.Any())
                return list.AsEnumerable();
            return Getvalues(parent.First().Id);
        }
    }
}
