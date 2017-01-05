using System.Web.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Peacock.Common.Helper;
using Peacock.ESB.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Peacock.PEP.WeChat.Service;
using Peacock.PEP.WeChat.Web.Help;
using System.Configuration;
using System.Net;
using System.Web.Script.Serialization;
using Peacock.PEP.WeChat.Model.ApiModel;
using System.Web.Security;
using Peacock.PEP.WeChat.Model.DTO;
using System.Text;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace Peacock.PEP.WeChat.Web.Controllers
{
    public class AuthCheckController :Controller
    {
        /// <summary>
        /// 扫码首页
        /// </summary>
        /// <param name="reportno"></param>
        /// <param name="trust"></param>
        /// <returns></returns>
        public ActionResult Index(string reportno, string trust)
        {
            string redirectUrl = string.Format("{0}/AuthCheck/CheckConCern?reportno={1}&trust={2}", ConfigHelper.WebDomainName, reportno, trust);
            string authUrl = OAuthApi.GetAuthorizeUrl(ConfigHelper.WeChatAppId, redirectUrl, "STATE", OAuthScope.snsapi_base,"code", false);
            return Redirect(authUrl);
        }

        /// <summary>
        /// 公众号扫码验证
        /// </summary>
        /// <param name="reportno"></param>
        /// <param name="trust"></param>
        /// <returns></returns>
        public ActionResult CheckConCern(string reportno, string trust,string state, string code)
        {           
            var token = OAuthApi.GetAccessToken(ConfigHelper.WeChatAppId, ConfigHelper.WeChatSecret,code);
            var userInfo = UserApi.Info(ConfigHelper.WeChatAppId, token.openid);
            if (userInfo.subscribe==0)//未关注
            {
                return RedirectToAction("Help", "AuthCheck");
            }
            else//已关注
            {
                return RedirectToAction("Details", "AuthCheck", new { reportno = reportno, trust = trust });  
            }
        }

        /// <summary>
        /// 帮助页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Help()
        {
            return View();
        }

       /// <summary>
        /// 二维码真伪页面 
       /// </summary>
       /// <param name="reportno"></param>
       /// <param name="trust"></param>
       /// <returns></returns>
        public ActionResult Details(string reportno, string trust)
        {
            //显示真伪查询页面
            ReportModel model = AuthCheckService.Instance.GetReportDetail(reportno, trust);
            if (model == null) model = new ReportModel();
            ViewBag.Report = model;

            return View();
        }
    }
}