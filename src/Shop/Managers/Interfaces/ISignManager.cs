using Shop.Models;
using Shop.ViewModels;
using System.Threading.Tasks;

namespace Shop.Managers.Interfaces
{
    public interface ISignManager
    {
        Task<LoginStatus> LoginAsyncMethod(LogInViewModel user, string password);
        void LogOut();
    }
}
