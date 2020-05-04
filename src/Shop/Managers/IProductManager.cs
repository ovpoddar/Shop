using Shop.Entities;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Managers
{
    public interface IProductManager
    {
        ProductListViewModel GetModel(int pageNumber);
        ProductListViewModel GetFilteredModel(int id, int pageNumber);
        Results<SaleProduct> UpdateStockLevel(SaleProduct saleProduct);
    }
}
