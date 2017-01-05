using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacock.PEP.WeChat.Model.ApiModel
{
    public class ResponseResult
    {       
        public bool Success { get; set; }

        public string Msg { get; set; }

        public object Data { get; set; }

        public string Others { get; set; }
    }
}
