using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Peacock.Common.Helper;
using Peacock.ESB.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Peacock.PEP.WeChat.Web.Help;
using System.Configuration;
using System.Web.Script.Serialization;
using Peacock.PEP.WeChat.Model.ApiModel;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Peacock.PEP.WeChat.Service;

namespace Peacock.PEP.WeChat.Web.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// 微信登录
        /// </summary>
        /// <returns></returns>
        public ActionResult WechatLogin()
        {
            string redirectUrl = string.Format("{0}/User/GetUserToken", ConfigHelper.WebDomainName);
            string authUrl = OAuthApi.GetAuthorizeUrl(ConfigHelper.WeChatAppId, redirectUrl, "STATE", OAuthScope.snsapi_base, "code", false);
            return Redirect(authUrl);
        }

        /// <summary>
        /// 微信重定向
        /// </summary>
        /// <param name="state"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetUserToken(string state, string code)
        {
            var token = OAuthApi.GetAccessToken(ConfigHelper.WeChatAppId, ConfigHelper.WeChatSecret, code);
            CookieHelper.WriteCookie("OpenId", token.openid);
            //判断是否绑定
            bool isBind = UserService.Instance.IsBinding(CookieHelper.GetCookie("OpenId"));
            if (!isBind)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return RedirectToAction("Index", "Charge");
            }
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ActionResult UserLogin(string userAccount, string password)
        {            
            ResponseResult response = null;            
            var result = UserService.Instance.UserLogin(userAccount, password);          
            if (result != null && ConfigHelper.BindingEnabled=="true")
            {    
                if (CookieHelper.GetCookie("OpenId") != null) response = UserService.Instance.SaveUserWeChat(result.Id,CookieHelper.GetCookie("OpenId"));
            }
            else if (result != null && ConfigHelper.BindingEnabled == "false")
            {
                response = new ResponseResult() { Success=true };
            }
            else
            {
                response = new ResponseResult() { Success = false };
            }           
            return Json(response, JsonRequestBehavior.AllowGet);          
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            ResponseResult response = null;           
            if (ConfigHelper.BindingEnabled == "true") {
                response = UserService.Instance.DeleteUserWeChat(CookieHelper.GetCookie("OpenId"));
            }
            else
            {
                response = new ResponseResult() { Success=true };
            }
            if (response.Success == true) 
            { 
                UserService.Instance.Logout();
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 公众号关注帮助页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AttentionHelp()
        {
            return View();
        }       
    }
}