using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.PEP.WeChat.Model.ApiModel
{
    public class ChargeRequest
    {
        /// <summary>
        /// 查询内容
        /// </summary>
        public string SearchNo { get; set; }

        /// <summary>
        /// 登录Id
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 0:我的任务，1：项目跟进，2：反馈项目，3：收款确认
        /// </summary>
        public int SearchFlag { get; set; }

        public long CompanyId { get; set; } 
    }
}
