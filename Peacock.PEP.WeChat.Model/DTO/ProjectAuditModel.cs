using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.PEP.WeChat.Model.DTO
{
    public class ProjectAuditModel
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public long TID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Expansion { set; get; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public string ApproveCompleteTime { set; get; }
        /// <summary>
        /// 紧急程度
        /// </summary>
        public string PriorityLevel { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string ProjectNo { get; set; }
        /// <summary>
        /// 报告编号
        /// </summary>
        public string ReportNo { get; set; }
        /// <summary>
        /// 旧流水号
        /// </summary>
        public string OldProjectNo { get; set; }
        /// <summary>
        /// 旧报告编号
        /// </summary>
        public string OldReportNo { get; set; }
        /// <summary>
        /// 完整地址
        /// </summary>
        public string ResidentialAreaAddress { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public string ResidentialAreaName { get; set; }

        /// <summary>
        /// 最低收费
        /// </summary>
        public decimal StandardFee { get; set; }
        /// <summary>
        /// 调后应收
        /// </summary>
        public decimal AdjustFee { get; set; }
        /// <summary>
        /// 报单人
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 立项人
        /// </summary>
        public string ProjectCreatorName { get; set; }

        /// <summary>
        /// 报告创建人
        /// </summary>
        public string ReportCreatorName { get; set; }


        public string ReportProperty { get; set; }

        /// <summary>
        /// 报告类型
        /// </summary>
        public string ReportCategory { get; set; }
    }
}
