using System.IO;

namespace Shop.Handlers
{
    public interface IProtectorHandler
    {
        string Protect(string name);
        string UnProtect(string name);
        string Hashsha512(string name);
        string Hashpbkdf2(string name, int length);
        string HashMd5(string name);
        string HashMd5(Stream name);
        string HashSha256(string name);
    }
}
