using Shop.Entities;
using Shop.Models;
using Shop.ViewModels;
using System.Threading.Tasks;

namespace Shop.Helpers.Interfaces
{
    public interface IUserHelper
    {
        Status CreateEmployer(SignInViewModel model);
        Task<Status> SaveAsync(Status status);
        Employer GetEmployerByUserName(string username);
        Employer GetEmployerByEmail(string email);
        Employer GetEmployerByNumber(long number);
        Employer GetEmployerByUnicId(string number);
        Task<Employer> FindEmployerAsync(string userId, string password);
    }
}
