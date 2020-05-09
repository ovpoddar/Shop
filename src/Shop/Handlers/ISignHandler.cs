using Shop.Models;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public interface ISignHandler
    {
        Task<LoginStatus> LogInAsync(string user, string password);
    }
}
