using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace Peacock.PEP.WeChat.Web.ApiControllers
{
    public class BaseApiController : ApiController, IRequiresSessionState
    {
 
        protected static readonly string ErrorMsg = "内部服务器错误";
        protected string IpAddress
        {
            get
            {
                string userIP; 
                HttpRequest Request = HttpContext.Current.Request; // ForumContext.Current.Context.Request;  
                // 如果使用代理，获取真实IP  
                if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
                    userIP = Request.ServerVariables["REMOTE_ADDR"];
                else
                    userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (userIP == null || userIP == "")
                    userIP = Request.UserHostAddress;
                return userIP;
            }
        }

        public BaseApiController()
        {
           
        }
    }
}
