using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IProductRepositories
    {
        IEnumerable<int> GetCategoryIds(int id);
    }
}
