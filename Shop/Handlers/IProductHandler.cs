using Shop.Entities;
using System.Collections.Generic;

namespace Shop.Handlers
{
    public interface IProductHandler
    {
        List<Product> Products();
        List<Product> Products(int id);
    }
}
