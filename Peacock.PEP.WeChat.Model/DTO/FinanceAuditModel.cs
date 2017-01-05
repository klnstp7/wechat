using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peacock.PEP.WeChat.Model.Enum;

namespace Peacock.PEP.WeChat.Model.DTO
{
    public class FinanceAuditModel
    {
        /// <summary>
        /// 
        /// </summary>
        public long TID{get;set;}

        public string ProjectNo { get; set; }

        public string ReportNo { get; set; }

        public string ResidentialAreaAddress { get; set; }

        public string ResidentialAreaName { get; set; }

        public string EmployeeName { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal ActualFee { get; set; }

        public decimal StandardFee { get; set; }

        public decimal AdjustFee { get; set; }

        public decimal FreePrice { get; set; }

        public string ReportProperty { get; set; }

        public string PostMessage { get; set; }
    }
}
