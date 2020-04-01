using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using Shop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

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
            var oldProduct = _repository.GetAll().Where(o => string.Equals(o.ProductName, product.ProductName, StringComparison.OrdinalIgnoreCase) && o.Price == product.Price).ToList();
            if (oldProduct.Any())
            {

                _repository.Delete(oldProduct.FirstOrDefault());
                _repository.Add(new Product()
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
                });
                _repository.save();
                return false;
            }

            _repository.Add(product);
            _repository.save();
            return true;
        }

        public Product GetProduct(int id) => _repository.GetAll()
            .Where(o => o.Id == id)
            .Include(o => o.Brand)
            .Include(o => o.Categories)
            .FirstOrDefault();

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

        public void RemoveProduct(Product product, int quantity)
        {
            _repository.GetAll()
                .Where(o => string.Equals(o.ProductName, product.ProductName, StringComparison.CurrentCultureIgnoreCase) && o.Price == product.Price)
                .FirstOrDefault().StockLevel -= quantity;
            _repository.save();
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
