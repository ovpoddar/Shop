using System.Collections.Generic;
using Shop.Entities;

namespace Shop.Handlers.Interfaces
{
    public interface IItemHandler<T>
    {
        List<T> List { get; set; }
        void addItem(Product product, int quantity);
        void updateItem(Product old, int quantity);
    }
}
