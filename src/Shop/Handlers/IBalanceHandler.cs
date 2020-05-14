using Shop.Entities;

namespace Shop.Handlers
{
    public interface IBalanceHandler
    {
        Balance GetLastBalance();
        void AddBalance(Balance Balance);
    }
}
