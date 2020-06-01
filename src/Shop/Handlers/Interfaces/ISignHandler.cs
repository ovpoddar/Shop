using System.Threading.Tasks;
using Shop.Models;

namespace Shop.Handlers.Interfaces
{
    public interface ISignHandler
    {
        Task<LoginStatus> LogInAsync(string user, string password);
    }
}
