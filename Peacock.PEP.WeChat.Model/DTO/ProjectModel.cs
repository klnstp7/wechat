using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.PEP.WeChat.Model.DTO
{
    public class ProjectModel
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public long TID { get; set; }

        /// <summary>
        /// (委托编号) 内部使用编号
        /// </summary>
        public string ProjectNo { get; set; }

        /// <summary>
        /// 报告编号
        /// </summary>
        public string ReportNo { get; set; }

        /// <summary>
        /// 项目地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int TaskType { get; set; }
    }
}
