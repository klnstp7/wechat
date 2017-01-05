using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Peacock.ESB.Helper;

using Peacock.PEP.WeChat.Web.Help;
using Peacock.PMS.Service.Dto;

namespace Peacock.PEP.WeChat.Web.Attributes
{
    public abstract class BaseAttribute : FilterAttribute, IActionFilter
    {
  

        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }
    }
}