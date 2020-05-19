using Microsoft.EntityFrameworkCore;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Repositories
{
    public class ProductRepositories : IProductRepositories

    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepositories(ApplicationDbContext applicationDbContext) =>
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(_applicationDbContext));

        public IEnumerable<int> GetCategoryIds(int id) =>
            _applicationDbContext.Categories.FromSqlRaw($"exec spGetCategoryIds @id = {id}").Select(p => p.Id).ToList();
    }
}
