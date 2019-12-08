using Shop.ViewModels;

namespace Shop.Managers
{
    public interface IProductManager
    {
        ProductListViewModel GetModel();
        ProductListViewModel GetModel(int PageNumber);
        ProductListViewModel GetFilteredModel(int id, int PageNumber);
    }
}
