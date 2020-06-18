using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Handlers.Interfaces
{
    public interface IItemHandler
    {
        Results<ItemModel> AddItem(string productName, int Quantity);
    }
}
