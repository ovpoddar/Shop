using Shop.Entities;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public interface IWholesaleHandler
    {
        bool Add(WholeSaleViewModel Details);
    }
}
