using Shop.Models;
using Shop.ViewModels;
using System.Threading.Tasks;

namespace Checkout.Managers
{
    public interface IloginManager
    {
        Task<Status> LogMeIn(LogInViewModel logInView);
        void LogMeOutAsync();
    }
}
