using Shop.Entities;
using System.Collections.Generic;

namespace Shop.Handlers
{
    public interface IProductHandler
    {
        List<Product> Products(int PageNumber);
        List<Product> Products(int id, int PageNumber);
        int TotalCount();
    }
}
