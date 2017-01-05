using Peacock.InWork4.DataCollection;
using Peacock.InWork4.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peacock.InWork4.DataCollection.Entities;

namespace Peacock.InWork4.Services
{
    public interface IProjectService 
    {
        IEnumerable<Project> GetEntitys();
        //IEnumerable<>
        bool UpdateEntity(long tid);
    }
}
