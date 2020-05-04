using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiWorker.Manager
{
    public interface IRunApplication
    {
        Task RunSimulation();
    }
}
