using Shop.Entities;
using System.Collections.Generic;

namespace Shop.Handlers
{
    public interface IProductHandler
    {
        List<Product> Products(int pageNumber);
        List<Product> Products(int id, int pageNumber);
        int TotalCount(int id);
        int TotalCount();
        bool AddProduct(Product product);
    }
}
