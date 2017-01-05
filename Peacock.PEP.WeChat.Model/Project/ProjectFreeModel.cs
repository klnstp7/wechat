using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.InWork4.Model
{
    public class ProjectFreeModel
    {
        public long TID { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ViewDate { get; set; }
        public string Idea { get; set; }
        public Nullable<bool> IsPass { get; set; }
        public Nullable<bool> IsAudit { get; set; }
        public Nullable<decimal> FreePrice { get; set; }
        public Nullable<int> ProjectId { get; set; }
        //public Nullable<int> FeeState { get; set; }
    }
}
