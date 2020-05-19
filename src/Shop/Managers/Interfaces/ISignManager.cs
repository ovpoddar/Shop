using System.Threading.Tasks;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Managers.Interfaces
{
    public interface ISignManager
    {
        Task<LoginStatus> LoginAsyncMethod(LogInViewModel user, string password);
        void LogOut();
    }
}
