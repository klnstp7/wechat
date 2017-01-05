using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.PEP.WeChat.Model.ApiModel
{
    public class FinanceAuditListRequest
    {
        /// <summary>
        /// 查询内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 登录Id
        /// </summary>
        public string UserName { get; set; }
    }
}
