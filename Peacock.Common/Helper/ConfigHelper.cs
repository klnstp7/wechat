using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Peacock.Common.Helper
{
    public static class ConfigHelper
    {
        public static string WeChatAppId = ConfigurationManager.AppSettings["WeChatAppId"];

        public static string WeChatSecret = ConfigurationManager.AppSettings["WeChatSecret"];

       public static string WebDomainName = ConfigurationManager.AppSettings["WebDomainName"];

       public static string BindingEnabled = ConfigurationManager.AppSettings["BindingEnabled"];

       public static string WeChatToken = ConfigurationManager.AppSettings["WeChatToken"];

       public static string WeChatEncodingAESKey = ConfigurationManager.AppSettings["WeChatEncodingAESKey"];
    }
}
