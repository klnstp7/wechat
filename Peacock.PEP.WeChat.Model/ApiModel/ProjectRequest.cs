using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.PEP.WeChat.Model.ApiModel
{
    public class ProjectRequest
    {
        /// <summary>
        ///项目Id
        /// </summary>
        public long projectId { get; set; }

        public string UserAccount { get; set; }
    }
}
