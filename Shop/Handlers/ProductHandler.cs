using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using Shop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Handlers
{
    public class ProductHandler : IProductHandler
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IProductRepositories _productRepositories;
        private readonly int _pageSize = 4;

        public ProductHandler(IGenericRepository<Product> repository, IProductRepositories productRepositories)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _productRepositories = productRepositories ?? throw new ArgumentNullException(nameof(productRepositories));
        }

        public List<Product> Products(int PageNumber)
        {
            if (PageNumber == -1)
                PageNumber = 0;
            return _repository.GetAll()
                .Include(p => p.Brand)
                .Include(p => p.Categories)
                .Include(p => p.ProductWholeSales)
                .OrderBy(o => o.ProductName)
                .Skip(PageNumber * _pageSize)
                .Take(_pageSize)
                .ToList();
        }

        public List<Product> Products(int id, int PageNumber)
        {
            if (PageNumber == -1)
                PageNumber = 0;
            return _repository.GetAll()
                .Include(p => p.Brand)
                .Include(p => p.Categories)
                .Where(p => _productRepositories.GetGetCategoryIds(id).Contains(p.CategoriesId))
                .Skip(PageNumber * _pageSize)
                .Take(_pageSize)
                .ToList();
        }

        public int TotalCount(int id)
        {
            var product = _repository.GetAll().Where(p => _productRepositories.GetGetCategoryIds(id).Contains(p.CategoriesId)).Count();
            var groupPage = (product / _pageSize);
            var extraPage = (product % _pageSize);
            switch (extraPage)
            {
                case 0:
                    return groupPage;
                default:
                    return groupPage + 1;
            }
        }

        public int TotalCount()
        {
            var product = _repository.GetAll().Count();
            var groupPage = (product / _pageSize);
            var extraPage = (product % _pageSize);
            switch (extraPage)
            {
                case 0:
                    return groupPage;
                default:
                    return groupPage + 1;
            }
        }
    }
}
