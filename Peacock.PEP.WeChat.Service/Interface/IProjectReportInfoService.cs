using Peacock.InWork4.DataCollection.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.InWork4.Services
{
    public interface IProjectReportInfoService
    {
        IEnumerable<ProjectReportInfo> GetEntitys();
    }
}
