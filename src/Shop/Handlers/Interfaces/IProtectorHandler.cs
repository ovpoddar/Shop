using System.IO;

namespace Shop.Handlers.Interfaces
{
    public interface IProtectorHandler
    {
        string HashMd5(Stream name);
    }
}
