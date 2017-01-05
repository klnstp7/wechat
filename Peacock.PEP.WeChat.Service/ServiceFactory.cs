using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.InWork4.Services
{
    /// <summary>
    /// 类：ServiceFactory
    /// 描述：业务服务工厂类,供统一访问业务服务层入口使用
    /// 创建人：jgd
    /// 创建时间：2015-10-22
    /// <summary>
    /// <summary>
    /// 业务服务工厂类
    /// </summary>
    public class ServiceFactory
    {
        #region 变量
        private readonly IParameterService m_ParameterService;
        private readonly IProjectService m_ProjectService;
        private readonly IProjectFreeService m_ProjectFreeService;
        private readonly IProjectReportInfoService m_ProjectReportInfoService;
        private readonly IFreeDetailService m_FreeDetailService;
        private readonly IEvaluationTargetService m_EvaluationTargetService;
        #endregion

        public ServiceFactory(IParameterService _ParameterService, IProjectService _ProjectService, IProjectFreeService _ProjectFreeService)
        {
            m_ParameterService = _ParameterService;
            m_ProjectService = _ProjectService;
            m_ProjectFreeService = _ProjectFreeService;
        }
        public ServiceFactory(IProjectReportInfoService _ProjectReportInfoService, IFreeDetailService _FreeDetailService, IEvaluationTargetService _EvaluationTargetService)
        {
            m_ProjectReportInfoService = _ProjectReportInfoService;
            m_FreeDetailService = _FreeDetailService;
            m_EvaluationTargetService = _EvaluationTargetService;
        }
        /// <summary>
        /// ParameterService服务
        /// </summary>
        public IParameterService ParameterService
        {
            get
            {
                return m_ParameterService;
            }
        }

        /// <summary>
        /// ProjectService服务
        /// </summary>
        public IProjectService ProjectService
        {
            get
            {
                return m_ProjectService;
            }
        }

        /// <summary>
        /// ProjectFreeService服务
        /// </summary>
        public IProjectFreeService ProjectFreeService
        {
            get
            {
                return m_ProjectFreeService;
            }
        }
        public IProjectReportInfoService ProjectReportInfo
        {
            get
            {
                return m_ProjectReportInfoService;
            }
        }
        public IFreeDetailService FreeDetailService
        {
            get {
                return m_FreeDetailService;
            }
        }
        public IEvaluationTargetService EvaluationTargetService
        {
            get
            {
                return m_EvaluationTargetService;
            }
        }
    }
}
