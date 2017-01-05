using Peacock.InWork4.DataCollection.Entities;
using Peacock.InWork4.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.InWork4.Services
{
    public class EvaluationTargetService : Repository<EvaluationTarget>,IEvaluationTargetService
    {
        public IEnumerable<EvaluationTarget> GetEntitys()
        {
            return GetEntitys();
        }
    }
}
