using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Peacock.PEP.WeChat.Web.Attributes
{
    /// <summary>
    /// 系统权限验证特性（加在Action上）
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class PermissionAttribute : BaseAttribute
    {
        protected string PermissionCode { get; private set; }

      
        public PermissionAttribute(string permissionCode)
        {
            PermissionCode = permissionCode;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            base.OnActionExecuting(filterContext);
        }
       
    }
}