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
    public class HelloWorldController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            var result = new MonitorResponseResult();
            try
            {
                result.Code = (int)CodeEnum.正常等级;
                result.Msgs = new List<MonitorMsg>();
            }
            catch (Exception outException)
            {
                result.Code = (int)CodeEnum.一般严重等级;
                result.Msgs = new List<MonitorMsg>(){new MonitorMsg()
                {
                    Time=string.Format("{0:yyyy-MM-dd HH:mm:ss}",DateTime.Now),
                    Msg=string.Format("{0}",outException.Message)
                }};
            }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

    }

    /// <summary>
    /// 监控返回结果
    /// </summary>
    public class MonitorResponseResult
    {
        public int Code { get; set; }

        public IList<MonitorMsg> Msgs { set; get; }

    }

    /// <summary>
    /// 错误信息
    /// </summary>
    public class MonitorMsg
    {
        public string Time { get; set; }

        public string Msg { get; set; }
    }

    /// <summary>
    /// 结果代码
    /// </summary>
    public enum CodeEnum
    {
        正常等级 = 0,
        警告等级 = 1,
        一般严重等级 = 2,
        严重等级 = 3,
        灾难 = 4
    }
}