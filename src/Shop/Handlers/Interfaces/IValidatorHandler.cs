using System.Threading.Tasks;
using Shop.Entities;

namespace Shop.Handlers.Interfaces
{
    public interface IValidatorHandler
    {
        Task<bool> IsMember();
        Employer User();
    }
}
