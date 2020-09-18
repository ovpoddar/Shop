using DataAccess.Entities;

namespace Shop.Handlers.Interfaces
{
    public interface IBrandHandler
    {
        bool AddBrand(Brand brand);
        Brand AddBrandWithReturn(Brand brand);
        int GetId(string name);
    }
}