using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Repositories
{
    public class ProductRepositories : IProductRepositories

    {
        private readonly ApplicationDbContext ApplicationDbContext;

        public ProductRepositories(ApplicationDbContext _ApplicationDbContext)
        {
            ApplicationDbContext = _ApplicationDbContext ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }

        public IEnumerable<int> GetGetCategoryIds(int id)
        {
            return ApplicationDbContext.Categories.FromSql($"exec spGetCategoryIds @id = {id}").Select(p => p.Id).ToList();
        }
    }
}
