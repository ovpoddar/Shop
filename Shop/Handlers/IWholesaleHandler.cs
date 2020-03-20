using Shop.ViewModels;

namespace Shop.Handlers
{
    public interface IWholesaleHandler
    {
        bool Add(WholeSaleViewModel details);
        int GetId(int size, int package);
    }
}
