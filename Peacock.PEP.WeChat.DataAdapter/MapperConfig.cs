using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using AutoMapper;

namespace Peacock.PEP.WeChat.DataAdapter
{
    /// <summary>
    /// Model和Entity之间的转换
    /// ConditionQuery和Condition之间的转换
    /// </summary>
    public static class MapperConfig
    {
        static MapperConfig()
        {
            Init();
        }
        private static void Init()
        {
            //AutoMapper.Mapper.CreateMap<Project, ProjectModel>();
           
        }

        public static TResult ToModel<TResult>(this object entity)
        {
            return Mapper.Map<TResult>(entity);
        }

        public static List<TResult> ToListModel<TResult, TInput>(this IEnumerable<TInput> list)
        {
            return list.Select(x => x.ToModel<TResult>()).ToList();
        }
    }
}
