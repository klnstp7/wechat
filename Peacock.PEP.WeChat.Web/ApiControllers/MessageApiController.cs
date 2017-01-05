using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Peacock.Common.Helper;
using Peacock.PEP.WeChat.Model.ApiModel;
using Peacock.PEP.WeChat.Model.Enum;
using Senparc.Weixin;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.CommonAPIs;


namespace Peacock.PEP.WeChat.Web.ApiControllers
{
     [RoutePrefix("Api/Message")]
    public class MessageApiController : BaseApiController
    {
        /// <summary>
        /// 消息提醒
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Notice")]
        [HttpPost]
        public ResponseResult Notice([FromBody]NoticeRequest request)
        {
            var responseResult = new ResponseResult();
            responseResult.Success = false;
            try
            {
                #region 参数判断
                if (string.IsNullOrEmpty(request.OpenId))
                {
                    throw new Exception("OpenId不能为空");
                }
                var flowNowList = EnumberHelper.EnumToList<WeChatNoticeEnum>().Select(x=>x.EnumValue).ToList();
                if (!flowNowList.Contains(request.FlowNode))
                {
                    throw new Exception("流程节点值错误");
                }
                if (string.IsNullOrEmpty(request.ProjectNo))
                {
                    throw new Exception("流水号不能为空");
                }
                if (string.IsNullOrEmpty(request.ReportNo))
                {
                    throw new Exception("报告号不能为空");
                }
                if (string.IsNullOrEmpty(request.ProjectAddress))
                {
                    throw new Exception("项目地址不能为空");
                }
                #endregion

                #region 消息推送
                var result = SendProjectMessage(request);
                bool wechatResult = (result.errcode == ReturnCode.请求成功);
                if (!wechatResult)
                {
                    LogHelper.Error(string.Format("调用微信官方API错误,错误编码：{0}，错误信息：{1}", result.errcode, result.errmsg), null);
                }
                else
                {
                    responseResult.Success = true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                responseResult.Success = false;
                responseResult.Msg = ErrorMsg;
                LogHelper.Error(string.Format("调用消息提醒API错误:{0},请求消息：{1}", ex.Message,request.ToJson()), null);
            }
            return responseResult;
        }

         /// <summary>
         /// 发送消息
         /// </summary>
         /// <param name="request"></param>
         /// <returns></returns>
        public SendTemplateMessageResult SendProjectMessage(NoticeRequest request)
         {
             var templateId = string.Format("{0}", ConfigurationManager.AppSettings["ProjectTemplateId"]);//模板Id
             var accessToken = AccessTokenContainer.GetAccessToken(ConfigHelper.WeChatAppId);
             var message = new
             {
                 first = new TemplateDataItem(request.Title,"#000000"),
                 keyword1 = new TemplateDataItem(request.ProjectNo, "#000000"),
                 keyword2 = new TemplateDataItem(request.ReportNo, "#000000"),
                 keyword3 = new TemplateDataItem(request.ProjectAddress, "#000000"),
                 remark = new TemplateDataItem(request.Remark, "#000000")
             };
             var result = TemplateApi.SendTemplateMessage(accessToken, request.OpenId, templateId, "#000000", "", message);
            return result;
         }
    }
}
