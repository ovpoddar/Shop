using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Shop.Handlers.Interfaces;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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
            var oldProduct = _repository.GetAll().Where(o => o.ProductName.ToUpper() == product.ProductName.ToUpper() && o.Price == product.Price).ToList();
            if (oldProduct.Any())
            {
                _repository.Delete(oldProduct.FirstOrDefault());

                var stockLevel = oldProduct.Select(o => o.StockLevel).FirstOrDefault() + product.StockLevel;

                _repository.Add(new Product()
                {
                    BrandId = product.BrandId,
                    CategoriesId = product.CategoriesId,
                    MinimumWholesaleOrder = product.MinimumWholesaleOrder,
                    ProductName = product.ProductName,
                    WholesalePrice = product.WholesalePrice,
                    Price = product.Price,
                    OrderLevel = product.OrderLevel,
                    StockLevel = stockLevel >= 0 ? stockLevel : 0,
                    BarCode = product.BarCode
                });
                _repository.Save();
                return false;
            }

            _repository.Add(product);
            _repository.Save();
            return true;
        }

        public Results<Product> GetProduct(string name)
        {
            var product = _repository.GetAll()
            .Where(o => o.ProductName == name)
            .Include(o => o.Brands)
            .Include(o => o.Categories)
            .ToList()
            .FirstOrDefault();

            return product == null ?
                new Results<Product>()
                {
                    Exception = "The Product is not found",
                    Result = null
                } :
                new Results<Product>()
                {
                    Success = true,
                    Result = product,
                    HttpStatusCode = HttpStatusCode.OK,
                };
        }

        public List<Product> Products(int pageNumber)
        {
            pageNumber = pageNumber == -1 ? 0 : pageNumber;
            return _repository.GetAll()
                .Include(p => p.Brands)
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
                .Include(p => p.Brands)
                .Include(p => p.Categories)
                .Where(p => _productRepositories.GetCategoryIds(id).Contains(p.CategoriesId))
                .Skip(pageNumber * _pageSize)
                .Take(_pageSize)
                .ToList();
        }

        public Results<ItemModel> RemoveProduct(ItemModel saleProduct)
        {
            var product = _repository.GetAll().FirstOrDefault(e => e.ProductName == saleProduct.Name);
            if (product == null)
                return new Results<ItemModel>()
                {
                    Exception = "Product Not Found",
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Result = saleProduct,
                    Success = false
                };
            var newStockLevel = product.StockLevel - saleProduct.Quantity;
            product.StockLevel = newStockLevel <= 0 ? 0 : newStockLevel;
            if (product.StockLevel <= 0) 
                return new Results<ItemModel>()
                {
                    Exception = "We Dont Have Enough Stock",
                    HttpStatusCode= HttpStatusCode.BadRequest,
                    Result = saleProduct,
                    Success= false
                };

            _repository.Update(product);
            if (product.StockLevel < product.OrderLevel)
                return new Results<ItemModel>()
                {
                    Exception = "Minimum Stock Level",
                    HttpStatusCode = HttpStatusCode.OK,
                    Result = saleProduct,
                    Success = true
                };
            return new Results<ItemModel>()
            {
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK,
                Result = saleProduct,
                Success = true
            };
        }

        public int TotalCount(int id) =>
            (_repository.GetAll().Count(p => _productRepositories.GetCategoryIds(id).Contains(p.CategoriesId)) % _pageSize) == 0 ?
            (_repository.GetAll().Count(p => _productRepositories.GetCategoryIds(id).Contains(p.CategoriesId)) / _pageSize) :
            (_repository.GetAll().Count(p => _productRepositories.GetCategoryIds(id).Contains(p.CategoriesId)) / _pageSize) + 1;

        public int TotalCount() =>
            (_repository.GetAll().Count() % _pageSize) == 0 ?
            (_repository.GetAll().Count() / _pageSize) :
            (_repository.GetAll().Count() / _pageSize) + 1;
    }
}
