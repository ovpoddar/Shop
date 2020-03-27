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
        private readonly int _pageSize = 10;

        public ProductHandler(IGenericRepository<Product> repository, IProductRepositories productRepositories)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _productRepositories = productRepositories ?? throw new ArgumentNullException(nameof(productRepositories));
        }

        public bool AddProduct(Product product)
        {
            var oldProduct = _repository.GetAll().Where(o => o.ProductName == product.ProductName && o.Price == product.Price).ToList();
            if (oldProduct.Any())
            {
                var newProduct = new Product()
                {
                    BrandId = product.BrandId,
                    CategoriesId = product.CategoriesId,
                    MinimumWholesaleOrder = product.MinimumWholesaleOrder,
                    ProductName = product.ProductName,
                    WholesalePrice = product.WholesalePrice,
                    Price = product.Price,
                    OrderLevel = product.OrderLevel,
                    StockLevel = Convert.ToDouble(oldProduct.Select(o => o.StockLevel).FirstOrDefault()) + product.StockLevel,
                    BarCode = product.BarCode
                };

                _repository.Delete(oldProduct.FirstOrDefault());
                _repository.Add(newProduct);
                _repository.save();

                return false;
            }

            _repository.Add(product);
            _repository.save();
            return true;
        }

        public List<Product> Products(int pageNumber)
        {
            pageNumber = pageNumber == -1 ? 0 : pageNumber;
            return _repository.GetAll()
                .Include(p => p.Brand)
                .Include(p => p.Categories)
                .Include(p => p.ProductWholeSales)
                .OrderBy(o => o.ProductName)
                .Skip(pageNumber * _pageSize)
                .Take(_pageSize)
                .ToList();
        }

        public List<Product> Products(int id, int pageNumber)
        {
            pageNumber = pageNumber == -1 ? 0 : pageNumber;
            return _repository.GetAll()
                .Include(p => p.Brand)
                .Include(p => p.Categories)
                .Where(p => _productRepositories.GetGetCategoryIds(id).Contains(p.CategoriesId))
                .Skip(pageNumber * _pageSize)
                .Take(_pageSize)
                .ToList();
        }

        public int TotalCount(int id) =>
            (_repository.GetAll().Count(p => _productRepositories.GetGetCategoryIds(id).Contains(p.CategoriesId)) % _pageSize) == 0 ?
            (_repository.GetAll().Count(p => _productRepositories.GetGetCategoryIds(id).Contains(p.CategoriesId)) / _pageSize) :
            (_repository.GetAll().Count(p => _productRepositories.GetGetCategoryIds(id).Contains(p.CategoriesId)) / _pageSize) + 1;

        public int TotalCount() =>
            (_repository.GetAll().Count() % _pageSize) == 0 ?
            (_repository.GetAll().Count() / _pageSize) :
            (_repository.GetAll().Count() / _pageSize) + 1;
    }
}
