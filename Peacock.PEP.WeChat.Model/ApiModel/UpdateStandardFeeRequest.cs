using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.PEP.WeChat.Model.ApiModel
{
    public class UpdateStandardFeeRequest
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// 最低收费
        /// </summary>
        public string StandardFee { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UserName { get; set; }
    }
}
