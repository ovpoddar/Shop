using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ProductRepositories : IProductRepositories
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepositories(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(_applicationDbContext));
        }

        //public IEnumerable<int> GetCategoryIds(int id)
        //{
        //    return _applicationDbContext.Categories.FromSqlRaw($"exec spGetCategoryIds @id = {id}")
        //        .Select(p => p.Id)
        //        .ToList();
        //}

        public IEnumerable<int> GetCategoryIds(int id)
        {
            Getvalues(id);
            return ints
                .Select(p => p.Id)
                .ToList();
        }
        public List<Category> ints { get; set; } = new List<Category>();

        private void Getvalues(int id)
        {
            var parent = _applicationDbContext.Categories.Where(e => e.ParentId == id);
            ints.Add(_applicationDbContext.Categories.First(e => e.Id == id));
            if (parent.Any())
                Getvalues(parent.First().Id);
            return;
        }

    }
}
