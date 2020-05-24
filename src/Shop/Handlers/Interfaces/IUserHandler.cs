using System.Threading.Tasks;
using Shop.Entities;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Handlers.Interfaces
{
    public interface IUserHandler
    {
        Task<Status> CreateUserAsync(SignInViewModel model);
        Task<Employer> FindEmployerAsync(string userId, string password);
        string GetUserName(string query);
    }
}
