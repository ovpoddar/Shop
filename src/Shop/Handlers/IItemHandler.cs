using Shop.Entities;
using System.Collections.Generic;

namespace Shop.Handlers
{
    public interface IItemHandler<T>
    {
        List<T> List { get; set; }
        void addItem(Product product, int quantity);
        void updateItem(Product old, int quantity);
    }
}
