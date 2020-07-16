using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Helpers
{
    public interface ICatagoriesHelper
    {
        public List<Category> Categories { get; set; }
        void Getvalues(int id);
    }
}
