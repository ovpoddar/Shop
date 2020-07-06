using Shop.Models;
using Shop.ViewModels;
using System.Collections.Generic;

namespace Shop.Managers.Interfaces
{
    public interface IProductManager
    {
        ProductListViewModel GetModel(int pageNumber);
        ProductListViewModel GetFilteredModel(int id, int pageNumber);
        OverallResult<List<Results<ItemModel>>> SalesProduct(PurchaseModel purchaseModel, string user);
    }
}
