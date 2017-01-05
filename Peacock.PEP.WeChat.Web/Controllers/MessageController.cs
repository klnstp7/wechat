using Peacock.Common.Helper;
using Peacock.PEP.WeChat.Model.DTO;
using Peacock.PEP.WeChat.Web.Help;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Peacock.PEP.WeChat.Web.Controllers
{
    public class MessageController : Controller
    {
        
        /// <summary>
        /// 微信后台验证地址
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, ConfigHelper.WeChatToken))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + postModel.Signature + "," + CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, ConfigHelper.WeChatToken) + "。");
            }
        }

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, ConfigHelper.WeChatToken))
            {
                return Content("参数错误！");
            }
            postModel.Token = ConfigHelper.WeChatToken;
            postModel.EncodingAESKey = ConfigHelper.WeChatEncodingAESKey;
            postModel.AppId = ConfigHelper.WeChatAppId;

            var messageHandler = new CustomMessageHandler(Request.InputStream, postModel);//接收消息
            messageHandler.Execute();//执行微信处理过程
            return Content(messageHandler.ResponseDocument.ToString());//返回xml信息
        }
    }

}