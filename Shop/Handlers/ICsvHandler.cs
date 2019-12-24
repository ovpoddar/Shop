using Microsoft.AspNetCore.Http;
using Shop.ViewModels;

namespace Shop.Handlers
{
    public interface ICsvHandler
    {
        void Save(string name, string Path, string hash, string time);
        void Store(string name,IFormFile csv);
        void Delete(string path);
    }
}
