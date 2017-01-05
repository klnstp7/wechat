using Peacock.InWork4.DataCollection.Entities;
using Peacock.InWork4.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.InWork4.Services
{
    public class FreeDetailService : Repository<FeeDetail>, IFreeDetailService
    {
        public IEnumerable<FeeDetail> GetEntitys()
        {
            return GetEntities(null);
        }
        public bool Delete(long tid)
        {
            return base.Delete(x=>x.TID==tid);
        }

        public bool Update(FeeDetail entity)
        {
            return base.Update(entity);
        }
    }
}
