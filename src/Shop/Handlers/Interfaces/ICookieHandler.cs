using Microsoft.AspNetCore.Http;

namespace Shop.Handlers.Interfaces
{
    public interface ICookieHandler
    {
        void Create(string name, string value);
        void Create(string name, string value, CookieOptions options);
        string Get(string name);
        void Delete(string name);
    }
}
