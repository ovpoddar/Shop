using System.Collections.Generic;
using System.Linq;

namespace Shop.Repositories
{
    public interface IProductRepositories
    {
        IEnumerable<int> GetGetCategoryIds(int id);
    }
}
