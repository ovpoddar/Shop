using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<int> GetGetCategoryIds(int id) =>
            _applicationDbContext.Categories.FromSql($"exec spGetCategoryIds @id = {id}").Select(p => p.Id).ToList();
    }
}
