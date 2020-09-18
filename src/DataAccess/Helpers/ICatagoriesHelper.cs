using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Helpers
{
    public interface ICatagoriesHelper
    {
        public List<Category> Categories { get; set; }
        void Getvalues(int id);
    }
}
