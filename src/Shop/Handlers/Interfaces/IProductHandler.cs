using System.Collections.Generic;
using Shop.Entities;
using Shop.Models;

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
        Product GetProduct(int id);
        Product GetProduct(string name);
    }
}
