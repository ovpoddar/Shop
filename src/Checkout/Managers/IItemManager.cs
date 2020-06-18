using Checkout.ViewModels;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Managers
{
    public interface IItemManager
    {
        Task Add(ItemViewModel model);
        void remove(int id);
        ItemModel GetItem(string name);
        Task<ItemViewModel> Model(string name);
    }
}
