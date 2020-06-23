using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class OverallResult<T>
    {
        public bool Success { get; set; }
        public T Objects { get; set; }
    }
}
