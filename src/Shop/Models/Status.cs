using System.Collections.Generic;

namespace Shop.Models
{
    public class Status
    {
        public bool Success { get; set; }
        public List<string> Error { get; set; }
    }
}
