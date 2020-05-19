using System.Collections.Generic;

namespace Shop.Repositories
{
    public interface IProductRepositories
    {
        IEnumerable<int> GetCategoryIds(int id);
    }
}
