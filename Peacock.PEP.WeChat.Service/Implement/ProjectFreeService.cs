using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peacock.InWork4.DataCollection.Entities;
using Peacock.InWork4.Repository;
using System.Data;
using Peacock.InWork4.DataCollection.DTO;
using Peacock.InWork4.Repository.Context;

namespace Peacock.InWork4.Services
{
    public class ProjectFreeService : Repository<ProjectFree>, IProjectFreeService
    {
        /// <summary>
        /// 获取列表集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProjectFree> GetEntitys()
        {
            return GetEntities(t => t.TID > 80);
        }
        //public List<ProjectChargeModel> GetProjectChargeList()
        //{
        //    var aa = from item in _read.ProjectReportInfoCollection join b in _read.EvaluationTargetCollection 
        //}
        /// <summary>
        /// 线程加锁查询操作
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProjectFree> GetEntitysThread()
        {
            IEnumerable<ProjectFree> list=null;
            ThreadLock(() =>
            {
                list = GetEntities(t => t.TID > 80);
            });
            return  list;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public bool AddEntity()
        {
            bool result = false;
            ProjectFree entity = new ProjectFree();
            entity.Remark = "新增测试";
            entity.Reason = DateTime.Now.ToString("yyyyMMddHHmmss");
            result = Insert(entity);
            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public bool UpdateEntity(long tid)
        {
            bool result = false;
            Transaction(() =>
            {
                ProjectFree entity = GetEntity(t => t.TID == tid);
                if (entity != null)
                {
                    entity.Reason = DateTime.Now.ToString("yyyyMMddHHmmss");
                    result = Update(entity);                    
                }
            });
            return result;
        }

        /// <summary>
        /// 线程加锁更新操作
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public bool UpdateEntityThread(long tid)
        {
            bool result = false;
            ThreadLock(() =>
            {
                ProjectFree entity = GetEntity(t => t.TID == tid);
                if (entity != null)
                {
                    entity.Reason = DateTime.Now.ToString("yyyyMMddHHmmss");
                    result = Update(entity);
                }
            });
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public bool DeleteEntity(long tid)
        {
            bool result = false;
            Transaction(() =>
            {
                ProjectFree entity = GetEntity(t => t.TID == tid);
                if (entity != null)
                {
                    result = Delete(entity);
                }
            });
            return result;
        }

        private long _tid=-100;
        public bool UpdateEntity2()
        {
            ProjectFree entity = GetEntity(t => t.TID == _tid);
            if (entity == null)
            {
                return false;
            }
            entity.Reason = DateTime.Now.ToString("yyyyMMddHHmmss");
            return Update(entity);
        }

        public bool TestTransaction(long tid)
        {
            _tid = tid;
            return DoTransaction(UpdateEntity2);
        }

    }
}
