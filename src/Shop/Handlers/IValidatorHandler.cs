using Shop.Entities;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public interface IValidatorHandler
    {
        Task<bool> IsMember();
        Employer User();
    }
}
