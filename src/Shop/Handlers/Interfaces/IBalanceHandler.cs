using DataAccess.Entities;

namespace Shop.Handlers.Interfaces
{
    public interface IBalanceHandler
    {
        Balance GetLastBalance();
        void AddBalance(Balance Balance);
    }
}
