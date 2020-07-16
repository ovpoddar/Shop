using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Helpers
{
    public class CatagoriesHelper : ICatagoriesHelper
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CatagoriesHelper(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(_applicationDbContext));
        }

        public List<Category> Categories { get; set; } = new List<Category>();

        public void Getvalues(int id)
        {
            var parent = _applicationDbContext.Categories.Where(e => e.ParentId == id);
            Categories.Add(_applicationDbContext.Categories.First(e => e.Id == id));
            if (parent.Any())
                Getvalues(parent.First().Id);
            return;
        }
    }
}
