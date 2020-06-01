using Shop.Models;
using System.Threading.Tasks;

namespace Shop.Handlers.Interfaces
{
    public interface ISignHandler
    {
        Task<LoginStatus> LogInAsync(string user, string password);
    }
}
