using Shop.Entities;
using System.Threading.Tasks;

namespace Shop.Handlers.Interfaces
{
    public interface IValidatorHandler
    {
        Task<bool> IsMember();
        Employer User();
    }
}
