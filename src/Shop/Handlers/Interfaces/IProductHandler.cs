using DataAccess.Entities;
using Shop.Models;
using System.Collections.Generic;

namespace Shop.Handlers.Interfaces
{
    public interface IProductHandler
    {
        List<Product> Products(int pageNumber);
        List<Product> Products(int id, int pageNumber);
        int TotalCount(int id);
        int TotalCount();
        bool AddProduct(Product product);
        Results<SaleProduct> RemoveProduct(SaleProduct saleProduct);
        Results<Product> GetProduct(string name);
    }
}
