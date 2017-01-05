using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Peacock.Common.Helper;
using Peacock.ESB.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Peacock.PEP.WeChat.Web.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return Content("测试");
        }
    }
}