using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.PEP.WeChat.Model.ApiModel
{
    public class ProjectAuditRequest
    {
        public string ProjectNo { get; set; }

        public bool IsPass { get; set; } 
        
        //public double? TotalScore { get; set; } 
        
        //public string PostScript { get; set; }

        public string UserName { get; set; } 
    }
}
