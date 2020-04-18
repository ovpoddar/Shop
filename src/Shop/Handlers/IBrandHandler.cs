using Shop.Entities;

namespace Shop.Handlers
{
    public interface IBrandHandler
    {
        bool AddBrand(Brand brand);
        Brand AddBrandWithReturn(Brand brand);
        int GetId(string name);
    }
}