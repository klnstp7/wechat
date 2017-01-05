using System.Collections;
using Peacock.Common.Helper;
using Peacock.PEP.WeChat.Model.DTO;
using Peacock.PEP.WeChat.Service;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Peacock.PEP.WeChat.Web.Help
{
    /// <summary>
    /// 返回消息
    /// </summary>
    public class CustomMessageHandler : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
      
        public CustomMessageHandler(Stream inputStream, PostModel postModel)
            : base(inputStream, postModel)
        {

        }

        #region 消息
        /// <summary>
        /// 默认处理
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "本公众号不提供此服务，请迟点再试！";
            return responseMessage;
        }

        /// <summary>
        /// 文字消息处理
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();

            string sendMsg = requestMessage.Content;

            //检查格式
            bool checkformat = false;
            char[] splitCharArray = new[] {'#', '＃', ' '};
            IList<string> paraList=null;
            foreach (var chr in splitCharArray )
            {
                paraList = sendMsg.Split(chr).ToList();
                if (paraList.Count == 2)
                {
                    checkformat = true;
                    break;
                }
            }                       
            if (checkformat==false)
            {
                responseMessage.Content = "格式不正确,请按照要求的格式输入！";
            }
            else
            {
                string reportno = paraList[0];
                string trust = paraList[1];
                responseMessage.Content = this.SendReportContent(reportno,trust);
            }
            return responseMessage;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 订阅（关注）
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = "欢迎关注PEP微信端！";
            return responseMessage;
        }

        /// <summary>
        /// 菜单点击
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            IResponseMessageBase reponseMessage = null;
            switch (requestMessage.EventKey)
            {
                //手输验证
                case "authcheck_manual":
                    { 
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        reponseMessage = strongResponseMessage;
                        strongResponseMessage.Content = "请输入你需验证的报告编号+委托方名称，以空格键或#隔开；\n如1234567 张三 或1234567#张三 \n注意：存在多个委托方名称只需输入任意一个";
                    }
                    break;               
                default:
                    break;
            }
            return reponseMessage;
        }

        
        /// <summary>
        /// 事件之扫码推事件且弹出“消息接收中”提示框(scancode_waitmsg)
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ScancodeWaitmsgRequest(RequestMessageEvent_Scancode_Waitmsg requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            switch (requestMessage.EventKey)
            {
                case "authcheck_scan":
                    string scanResult=requestMessage.ScanCodeInfo.ScanResult;
                    LogHelper.Error(HttpUtility.UrlDecode(scanResult),null);
                    if (string.IsNullOrEmpty(scanResult))
                    {
                        responseMessage.Content = "扫描的结果为空！";
                    }
                    else
                    {        
                        string baseurl=string.Empty;
                        NameValueCollection paracollection;
                        HttpHelper.ParseUrl(scanResult, out baseurl, out paracollection);
                        if (baseurl != string.Format("{0}AuthCheck", ConfigHelper.WebDomainName) || paracollection["reportno"] == null || paracollection["trust"] == null)
                        {
                            responseMessage.Content = "报告不存在，请扫描正确的二维码！";//域名，报告号，委托人信息格式不正确
                        }
                        else
                        {
                            string reportno = HttpUtility.UrlDecode(paracollection["reportno"]);
                            string trust = HttpUtility.UrlDecode(paracollection["trust"]);
                            responseMessage.Content = this.SendReportContent(reportno, trust);
                        }
                    }
                    
                    break;
                default:
                    break;
            }
            return responseMessage;
        }
        #endregion

        #region 推送内容
        private string SendReportContent(string reportno,string trust)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            ReportModel report = AuthCheckService.Instance.GetReportDetail(reportno, trust);
            if (report == null)
            {
               builder.Append( "报告不存在，请输入正确的报告号与估计委托人信息！");
            }
            else 
            { 
                builder.AppendFormat("{0}", report.companyname).Append("\n");
                builder.AppendFormat("报告真伪查询结果").Append("\n");
                builder.AppendFormat("报告编号:{0}", report.reportNum).Append("\n");
                builder.AppendFormat("项目位置:{0}", report.address).Append("\n");
                builder.AppendFormat("委托方:{0}", report.evalEntrust).Append("\n");
                builder.AppendFormat("总价:{0}万元", report.evaluateTotal).Append("\n");
                if (report.official == true)
                {
                    builder.AppendFormat("单价:{0}元", report.price).Append("\n");
                }
                else
                {
                    builder.AppendFormat("单价:{0}元", report.pePrice).Append("\n");
                }               
                builder.AppendFormat("建成年代:{0}年", report.buildedYear).Append("\n");
                builder.AppendFormat("产权性质:{0}", report.property).Append("\n");
                if (!string.IsNullOrEmpty(report.physicalChange))
                {
                    builder.AppendFormat("物理结构是否变化:{0}", report.physicalChange).Append("\n");
                }
                if (!string.IsNullOrEmpty(report.presentStatus))
                {
                    builder.AppendFormat("使用现状:{0}", report.presentStatus).Append("\n");
                }                
                //大地需求
                string otherLinkCompany = string.Format("{0}", ConfigurationManager.AppSettings["OtherLinkCompany"]);
                string otherLinkUrl = string.Format("{0}", ConfigurationManager.AppSettings["OtherLinkUrl"]);
               
                if (report.companyname == otherLinkCompany)
                {
                    builder.Append("\n").AppendFormat("到<a href=\"{0}\">机构官网</a>验证真伪", otherLinkUrl);
                }
            }
            return builder.ToString();
        }
        #endregion
    }
}