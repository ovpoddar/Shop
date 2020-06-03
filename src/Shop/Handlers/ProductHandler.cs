using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Shop.Handlers.Interfaces;
using Shop.Models;
using Shop.Repositories;
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

        public Product GetProduct(int id) => _repository.GetAll()
            .Where(o => o.Id == id)
            .Include(o => o.Brands)
            .Include(o => o.Categories)
            .ToList()
            .FirstOrDefault();

        public Product GetProduct(string name) => _repository.GetAll()
            .Where(o => o.ProductName == name)
            .Include(o => o.Brands)
            .Include(o => o.Categories)
            .ToList()
            .FirstOrDefault();

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

        public Results<SaleProduct> RemoveProduct(SaleProduct saleProduct)
        {
            try
            {
                var product = _repository.GetAll().FirstOrDefault(p => p.Id == saleProduct.ProductId);

                if (product == null) throw new ArgumentNullException();

                var newStockLevel = product.StockLevel - saleProduct.SaleQuantity;
                product.StockLevel = newStockLevel <= 0 ? 0 : newStockLevel;

                _repository.Update(product);

                saleProduct.StockLevel = product.StockLevel;
                saleProduct.OrderLevel = product.OrderLevel;

                if (product.StockLevel < product.OrderLevel) saleProduct.Message = MessageValues.MinimumStock;
                if (product.StockLevel <= 0) saleProduct.Message = MessageValues.NoStock;

                return new Results<SaleProduct>
                { HttpStatusCode = HttpStatusCode.OK, Success = true, Result = saleProduct };
            }
            catch (Exception exception)
            {
                saleProduct.Message = "Product does not exist";
                return new Results<SaleProduct> { Exception = exception.Message, Result = saleProduct };
            }
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
