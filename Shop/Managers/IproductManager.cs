using Shop.ViewModels;

namespace Shop.Managers
{
    public interface IProductManager
    {
        ProductListViewModel GetModel();
        ProductListViewModel GetFilteredModel(int id);
    }
}
