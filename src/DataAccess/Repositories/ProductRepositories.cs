using DataAccess.Entities;
using DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ProductRepositories : IProductRepositories
    {
        private readonly ICatagoriesHelper _helper;

        public ProductRepositories(ICatagoriesHelper helper)
        {
            _helper = helper ?? throw new ArgumentNullException(nameof(_helper));
        }

        public IEnumerable<int> GetCategoryIds(int id)
        {
            _helper.Getvalues(id);
            return _helper.Categories
                .Select(p => p.Id)
                .ToList();
        }
    }
}
