using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.PEP.WeChat.Model.ApiModel
{
    public class FinanceAuditRequest
    {
        public long TId{ get; set; }

        public bool IsPass{ get; set; }

        public string CheckMessage { get; set; }

        public string UserName { get; set; }
    }
}
