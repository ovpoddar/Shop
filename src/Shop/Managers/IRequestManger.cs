using System.Threading.Tasks;

namespace Shop.Managers
{
    public interface IRequestManger
    {
        Task<string> PatchRequest<T>(string uri, T entity);
    }
}
