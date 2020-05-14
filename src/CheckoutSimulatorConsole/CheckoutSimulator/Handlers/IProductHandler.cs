using System;
using System.Threading.Tasks;

namespace CheckoutSimulator.Handlers
{
    public interface IProductHandler
    {
        Task PostExample(string uri);
        Task GetExample(string uri);
        Task PatchExample(string uri, bool throwError);
    }
}