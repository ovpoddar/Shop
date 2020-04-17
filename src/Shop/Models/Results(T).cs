using System.Net;

namespace Shop.Models
{
    public class Results<T>
    {
        public T Result { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool Success { get; set; }
    }
}