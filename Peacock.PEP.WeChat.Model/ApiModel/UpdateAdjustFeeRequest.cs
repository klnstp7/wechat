using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.PEP.WeChat.Model.ApiModel
{
    public class UpdateAdjustFeeRequest
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public string AdjustFee { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UserName { get; set; }
    }
}
