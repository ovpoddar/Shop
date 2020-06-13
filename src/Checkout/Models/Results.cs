using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Checkout.Models
{
    public class Results<T>
    {
        public T Result { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool Success { get; set; }
        public string Exception { get; set; }
    }
}
