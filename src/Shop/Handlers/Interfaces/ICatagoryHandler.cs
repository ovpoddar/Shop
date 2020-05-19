using System.Collections.Generic;
using Shop.Entities;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Handlers.Interfaces
{
    public interface ICategoryHandler
    {
        List<Category> Categories();
        List<Category> GetAll();
        Results<IEnumerable<Category>> AddCategory(CategoryViewModel model);
        int GetId(string name);
    }
}
