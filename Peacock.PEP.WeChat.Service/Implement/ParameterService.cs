using MemcacheClient;
using Peacock.InWork2.Helper.Exceptions;
using Peacock.InWork4.DataCollection.Entities;
using Peacock.InWork4.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.InWork4.Services
{
    /// <summary>
    /// 类：IParameterService
    /// 描述：系统参数服务类
    /// 创建人：jgd
    /// 创建时间：2015-11-10
    /// </summary>
    /// <summary>
    /// 系统参数服务类
    /// </summary>
    public class ParameterService : Repository<Parameter>, IParameterService
    {
        //private ICacheClient MemcachedClient = CacheClient.GetInstance();
        //private static readonly String xxxx = new String('g',1);
        private static readonly string ParameterCacheKey = "InworkParameters";

        public ParameterService()
        {
            //MemcachedClient = CacheClient.GetInstance();
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="dto"></param>
        public void SaveEntity(Parameter entity)
        {
            if (entity.TID > 0)
            {
                Update(entity);
            }
            else
            {
                Insert(entity);
            }
            //新增加参数后删除缓存，让下次拿数据时缓存可以重新更新 
            DeleteParameterCache();
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="id"></param>
        public void Delete(long id)
        {
            Transaction(() =>
            {
                Parameter entity = GetEntity(t => t.TID == id);
                if (entity.Children.Count > 0)
                {
                    throw new InvalidDataException("参数项有下级参数，不能删除");
                }
                Delete(entity);
                DeleteParameterCache();
            });          
        }

        /// <summary>
        /// 获取最顶级参数列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parameter> GetParameterList()
        {
            //return GetAll().Where(x => x.ParentId == 0).ToList();
            return GetEntities(t => t.TID>0);
        }

        /// <summary>
        /// 获取参数树列表
        /// </summary>
        /// <returns></returns>
        public Parameter GetParameterTree(string name)
        {
            return GetAll().FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// 根据主键获取参数
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public Parameter GetParameterEntityById(long tid)
        {
            return GetAll().FirstOrDefault(x => x.TID == tid);
        }

        /// <summary>
        /// 根据父节点获取数据列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IEnumerable<Parameter> GetListByParentId(long parentId)
        {
            return GetAll().Where(x => x.ParentId == parentId).ToList();
        }

        /// <summary>
        /// 通过名称查询数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Parameter GetDataByName(string name)
        {
            return GetAll().FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// 删除参数缓存
        /// </summary>
        public void DeleteParameterCache()
        {
            //MemcachedClient.Delete(ParameterCacheKey);
        }

        /// <summary>
        /// 获取所有数据 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parameter> GetAll()
        {
            //MemcachedClient = CacheClient.GetInstance();
            IEnumerable<Parameter> list = null;
            //if (MemcachedClient.Get(ParameterCacheKey) == null)
            //{
            //    list = GetEntities(t => t.TID != null);
            //    MemcachedClient.Set(ParameterCacheKey, list, 60 * 60 * 24);//默认缓存时间1天
            //}
            //else
            //{
            //    list = MemcachedClient.Get<IList<Parameter>>(ParameterCacheKey);
            //}

            return list;
        }

    }
}
