using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Shop.Handlers.Interfaces
{
    public interface IEmployerHandler
    {
        Task BlockEmployerAsync(string name);
        Task<IdentityResult> LastcheckInAsync(Employer employer);
        bool IsAccessable(Employer employer);
        Employer GetEmployer(string query);
    }
}
