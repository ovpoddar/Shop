using System.Collections.Generic;

namespace Shop.Repositories
{
    public interface IProductRepositories
    {
        IEnumerable<int> GetGetCategoryIds(int id);
    }
}
