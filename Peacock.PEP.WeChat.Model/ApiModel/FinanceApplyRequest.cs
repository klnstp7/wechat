using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.PEP.WeChat.Model.ApiModel
{
    public class FinanceApplyRequest
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// 申请理由
        /// </summary>
        public string ApplyReason { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string CheckName { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplyUser { get; set; } 
    }
}
