using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.InWork4.Model
{
    public class ProjectModel
    {
        public long TID { get; set; }
        public string CalculateType { get; set; }
        public string PaymentType { get; set; }
        public string PaymentAccount { get; set; }
        public string ReportSupplyType { get; set; }
        public string ReportProperty { get; set; }
        public string PropertyRemark { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectSource { get; set; }
        public string ProjectType { get; set; }
        public string BusinessType { get; set; }
    }
}
