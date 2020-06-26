using DataAccess.Entities;
using Shop.Models;
using Shop.ViewModels;
using System.Threading.Tasks;

namespace Shop.Handlers.Interfaces
{
    public interface IUserHandler
    {
        Task<Status> CreateUserAsync(SignInViewModel model);
        Task<Employer> FindEmployerAsync(string userId, string password);
        string GetUserName(string query);
        void DetectLogin(Employer employer);
    }
}
