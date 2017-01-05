using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peacock.PEP.WeChat.Model.Enum;

namespace Peacock.PEP.WeChat.Model.DTO
{
    public class FeedBackModel
    {
        public long TID{get;set;}

        public string Operator { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public SendTypeFlag SendType { get; set; }

        public  long ProjectId { get; set; }  
    }
}
