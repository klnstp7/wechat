using Peacock.InWork4.DataCollection;
using Peacock.InWork4.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peacock.InWork4.DataCollection.Entities;

namespace Peacock.InWork4.Services
{
    public class ProjectService : Repository<Project>, IProjectService
    {
        public IEnumerable<Project> GetEntitys()
        {
           //DataTable tb = ExecuteSqlToTable("select top 10 * from project_project");
           return  GetEntities(t=>t.ActualFee>0);
        }

        public bool UpdateEntity(long tid)
        {
            bool result = false;
            Transaction(() =>
            {
                Project entity = GetEntity(t => t.TID == tid);
                if (entity != null)
                {
                    entity.PropertyRemark = DateTime.Now.ToString("yyyyMMddHHmmss");
                    result = Update(entity);
                }
            });
            return result;
        }
    }
}
