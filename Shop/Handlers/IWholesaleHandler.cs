using Shop.ViewModels;

namespace Shop.Handlers
{
    public interface IWholesaleHandler
    {
        bool Add(WholeSaleViewModel Details);
        int GetId(int Size, int Package);
    }
}
