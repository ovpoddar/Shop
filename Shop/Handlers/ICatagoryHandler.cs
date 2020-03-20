using Shop.Entities;
using Shop.ViewModels;
using System.Collections.Generic;

namespace Shop.Handlers
{
    public interface ICategoryHandler
    {
        List<Category> Categories();
        List<Category> GetAll();
        List<Category> AddCategory(CategoryViewModel model);
        int GetId(string name);
    }
}
