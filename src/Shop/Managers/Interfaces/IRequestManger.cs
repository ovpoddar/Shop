using System.Threading.Tasks;

namespace Shop.Managers.Interfaces
{
    public interface IRequestManger
    {
        Task<string> PatchRequest<T>(string uri, T entity);
    }
}
