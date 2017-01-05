using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peacock.InWork4.DataCollection.Entities;

namespace Peacock.InWork4.Services
{
    public interface IProjectFreeService
    {
        IEnumerable<ProjectFree> GetEntitys();
        bool AddEntity();
        bool UpdateEntity(long tid);
        bool DeleteEntity(long tid);
        bool TestTransaction(long tid);
        IEnumerable<ProjectFree> GetEntitysThread();
        bool UpdateEntityThread(long tid);
    }
}
