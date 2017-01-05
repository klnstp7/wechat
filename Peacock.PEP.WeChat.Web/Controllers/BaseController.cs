using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Threading;
using Peacock.ESB.Helper;
using Peacock.PEP.WeChat.Web.Help;
using Peacock.Common.Helper;
using System.Web.Script.Serialization;
using Peacock.PEP.WeChat.Model.ApiModel;
using Peacock.PMS.Service.Dto;
using System.Configuration;
using System.Web;
using Peacock.PEP.WeChat.Service;

namespace Peacock.PEP.WeChat.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            bool isAuth=requestContext.HttpContext.User.Identity.IsAuthenticated;
            if (isAuth) 
            {
                //登录用户
                ViewBag.UserName = UserService.Instance.GetCurrentUser().UserName;
               
            }
            base.Initialize(requestContext);
        }
    }
}