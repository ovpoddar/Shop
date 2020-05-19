using Shop.ViewModels;

namespace Shop.Handlers.Interfaces
{
    public interface IWholesaleHandler
    {
        bool Add(WholeSaleViewModel details);
        int GetId(int size, int package);
    }
}
