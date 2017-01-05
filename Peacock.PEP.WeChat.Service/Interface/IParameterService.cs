using Peacock.InWork4.DataCollection.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.InWork4.Services
{
    /// <summary>
    /// 类：IParameterService
    /// 描述：系统参数服务接口
    /// 创建人：jgd
    /// 创建时间：2015-11-10
    /// </summary>
    /// <summary>
    /// 系统参数服务接口
    /// </summary>
    public interface IParameterService
    {
        /// <summary>
        /// 获取参数树列表
        /// </summary>
        /// <returns></returns>
        Parameter GetParameterTree(string name);

        /// <summary>
        /// 根据主键获取参数
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        Parameter GetParameterEntityById(long tid);

        /// <summary>
        /// 根据父节点获取数据列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IEnumerable<Parameter> GetListByParentId(long parentId);

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="dto"></param>
        void SaveEntity(Parameter entity);

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="id"></param>
        void Delete(long id);

        /// <summary>
        /// 获取最顶级参数列表
        /// </summary>
        IEnumerable<Parameter> GetParameterList();

        /// <summary>
        /// 通过名称查询数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Parameter GetDataByName(string name);

        /// <summary>
        /// 删除参数缓存
        /// </summary>
        void DeleteParameterCache();

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<Parameter> GetAll();
    }
}
