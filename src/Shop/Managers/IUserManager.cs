using Shop.Entities;
using Shop.Models;
using Shop.ViewModels;
using System.Threading.Tasks;

namespace Shop.Managers
{
    public interface IUserManager
    {
        Task<Status> CreateUserAsync(SignInViewModel model);
        Task<Employer> FindEmployerAsync(string userId, string password);
        string GetUserName(string query);
    }
}
