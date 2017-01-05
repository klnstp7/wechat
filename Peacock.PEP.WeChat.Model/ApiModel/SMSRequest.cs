using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.PEP.WeChat.Model.ApiModel
{
    public class SMSRequest
    {

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 手机号 ","分割
        /// </summary>
        public string Phones { get; set; }

        /// <summary>
        /// 发送内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 登录用户所属公司名称
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 项目ID 
        /// </summary>
        public long ProjectId { get; set; }

    }
}
