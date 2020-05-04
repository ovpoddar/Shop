using System.Threading.Tasks;

namespace Shop.Manager
{
    public interface IRequestManger
    {
        Task<string> PatchRequest<T>(string uri, T entity);
    }
}
