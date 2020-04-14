using Shop.Entities;

namespace Shop.Handlers
{
    public interface IBalanceHandler
    {
        Balance GetLastBalance();
        bool AddBalance(Balance Balance);
    }
}
