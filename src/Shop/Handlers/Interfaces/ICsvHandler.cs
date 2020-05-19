using Microsoft.AspNetCore.Http;

namespace Shop.Handlers.Interfaces
{
    public interface ICsvHandler
    {
        void SaveCsv(string name, string Path, string hash, string time);
        void StoreCsvAsFile(string name, IFormFile csv);
        void Delete(string path);
    }
}
