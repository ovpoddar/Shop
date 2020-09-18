using DataAccess.Entities;

namespace Shop.Handlers.Interfaces
{
    public interface ITokenHandler
    {
        string GenerateToken(Employer employer);
    }
}
