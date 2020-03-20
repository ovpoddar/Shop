using Shop.ViewModels;

namespace Shop.Managers
{
    public interface IProductManager
    {
        ProductListViewModel GetModel(int pageNumber);
        ProductListViewModel GetFilteredModel(int id, int pageNumber);
    }
}
