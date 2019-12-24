using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class Csv : BaseEntity
    {
        public string FileName { get; set; }
        public string HashName { get; set; }
        public string Filepath { get; set; }
        public string UpdateDate { get; set; }
    }
}
