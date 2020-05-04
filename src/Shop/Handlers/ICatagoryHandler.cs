using Shop.Entities;
using Shop.ViewModels;
using System.Collections.Generic;
using Shop.Models;

namespace Shop.Handlers
{
    public interface ICategoryHandler
    {
        List<Category> Categories();
        List<Category> GetAll();
        Results<IEnumerable<Category>> AddCategory(CategoryViewModel model);
        int GetId(string name);
    }
}
