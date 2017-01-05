#region << 版 本 注 释 >>
/*
 * ========================================================================
 * Copyright(c) 2004-2015 广州光汇软件科技有限公司, All Rights Reserved.
 * ========================================================================
*/
#endregion

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Configuration;
using System.Threading;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using Peacock.PMS.Service.Dto;
using Peacock.PMS.Service.Services;

using RestSharp;

namespace Peacock.Common.Helper
{
    /// <summary>
    /// 日志写入类
    /// </summary>
    /// <remarks>
    ///     <para>    Creator：hl</para>
    ///     <para>CreatedTime：2015-01-23 11:07:21</para>
    /// </remarks>
    public static class LogHelper
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// 调试
            /// </summary>
            Debug = 1,

            /// <summary>
            /// 用户操作日志
            /// </summary>
            Info = 2,

            /// <summary>
            /// 错误日志
            /// </summary>
            Error = 3,
        }

        /// <summary>
        /// 日志级别
        /// </summary>
        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");

        /// <summary>
        /// 日志级别
        /// </summary>
        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

        private static readonly string AppCode = ConfigurationManager.AppSettings["YewuAppCode"];
        private static readonly int CacheTime = Convert.ToInt32(ConfigurationManager.AppSettings["CrmCacheTime"]);
        private static readonly PmsService PmsService = new PmsService(AppCode, CacheTime);

        private static Dictionary<string, string> iPAndCity = new Dictionary<string, string>();
        public static string GetCityByIP(string ip)
        {
            string result = "北京";
            string ipcity = ConfigurationManager.AppSettings["ipcity"];
            var client = new RestClient(string.Format(ipcity, ip));
            var request = new RestRequest("", Method.GET);
            var data = client.Execute<IPAndCity>(request);
            try
            {
                IPAndCity city = JsonConvert.DeserializeObject<IPAndCity>(data.Content);
                result = city.data.city;
            }
            catch { }
            return result;
        }

        /// <summary>
        /// 配置
        /// </summary>
        public static void SetConfig()
        {
            // log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="configFile">文件属性</param>
        public static void SetConfig(FileInfo configFile)
        {
            //log4net.Config.XmlConfigurator.Configure(configFile);
        }

        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="info">消息</param>
        public static void WriteLog(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="info">错误标题</param>
        /// <param name="se">异常消息</param>
        public static void WriteLog(string info, Exception se)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, se);
            }
        }

        /// <summary>
        /// 错误记录封装
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static void Error(string message, Exception ex)
        {
            if (logerror.IsErrorEnabled)
            {
                if (!string.IsNullOrEmpty(message) && ex == null)
                {
                    logerror.ErrorFormat("<br/>【附加信息】 : {0}<br>", new object[] { message });
                }
                else if (!string.IsNullOrEmpty(message) && ex != null)
                {
                    string errorMsg = BeautyErrorMsg(ex);
                    logerror.ErrorFormat("<br/>【附加信息】 : {0}<br>{1}", new object[] { message, errorMsg });
                }
                else if (string.IsNullOrEmpty(message) && ex != null)
                {
                    string errorMsg = BeautyErrorMsg(ex);
                    logerror.Error(errorMsg);
                }
            }
        }

        /// <summary>
        /// 美化错误信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>错误信息</returns>
        private static string BeautyErrorMsg(Exception ex)
        {
            string errorMsg = string.Format("【异常类型】：{0} <br>【异常信息】：{1} <br>【堆栈调用】：{2}",
                new object[] { ex.GetType().Name, ex.Message, ex.StackTrace });
            errorMsg = errorMsg.Replace("\r\n", "<br>");
            errorMsg = errorMsg.Replace("位置", "<strong style=\"color:red\">位置</strong><br/>");
            return errorMsg;
        }

        ///// <summary>
        ///// 指定格式写入日志
        ///// </summary>
        ///// <param name="concent">记录内容</param>
        //public static void ILog(string concent)
        //{
        //    try
        //    {
        //        string userAccount = CookieHelper.GetCookie(CookieHelper.UserStateKey);
        //        string httpClientDoMain = ConfigurationManager.AppSettings["ReLogInUrl"];
        //        string clientIp = CookieHelper.GetWebClientIp();
        //        string insiteIp = CookieHelper.GetWebSiteIpAndPort();
        //        string fwVersion = Environment.Version.ToString();
        //        string port = CookieHelper.GetWebSitePort();
        //        string iis = CookieHelper.GetIIS();
        //        //10.172.216.209 - - [06 / Apr / 2016:06:42:00 + 0800] "basedbapi.yunfangdata.com" "GET /beijing/api/yh/participleSearch/search.do?u=yyf&p=yyf123456&searchName=null HTTP/1.1" 200 7178 "-" "Apache-HttpClient/4.3.5 (java 1.5)" "-" 0.216 0.216 200 10.171.13.129:22001
        //        //{0}客户端{1}时间{2}访问地址{3}内容{4}执行状态200{5}端口{6}使用类库{7}版本{8}站点版本{9}0.216{10}0.216{11}200{12}上传站点地址
        //        string logConcentFormat = "{0} - - [{1}] \"{2}\" \"{3}\" {4} {5} \" - \" \"{6}-{7} / {8}\" \" - \" {9} {10} {11} {12}";
        //        string logConcent = userAccount + string.Format(logConcentFormat, clientIp, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), httpClientDoMain, concent, "200", port, "csharp-framework", fwVersion, "iis 1.5", "0.216", "0.216", "200", insiteIp);
        //        string token = DateTime.Now.ToString("yyyyMMdd") + ".log";
        //        string logRoot = FileStreamHelper.GetPathAtApplication("Logs\\" + DateTime.Now.ToString("yyyy"));
        //        if (!Directory.Exists(logRoot))
        //        {
        //            Directory.CreateDirectory(logRoot);
        //        }
        //        StreamWriter sw = new StreamWriter(logRoot + "\\" + token, true);
        //        sw.WriteLine(logConcent);//写入
        //        sw.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Error(ex.Message, ex);
        //    }
        //}

        /// <summary>
        /// 指定格式写入日志
        /// </summary>
        /// <param name="concent">记录内容</param>
        public static void Ilog(string http_referer, string concent)
        {
            try
            {
                string userAccount = string.Empty;
                try
                {
                    string strCookie = CookieHelper.GetCookie(FormsAuthentication.FormsCookieName);
                    if (!string.IsNullOrWhiteSpace(strCookie))
                    {
                        userAccount =
                            JsonConvert.DeserializeObject<UserApiDto>(FormsAuthentication.Decrypt(strCookie).UserData)
                                .UserAccount;
                    }
                }
                catch
                {
                    //userAccount = "评E评管理员";
                }
                finally
                {
                    if (string.IsNullOrWhiteSpace(userAccount))
                    {
                        userAccount = "评E评管理员";
                    }
                }
                string httpClientDoMain = ConfigurationManager.AppSettings["ReLogInUrl"];
                string clientIp = CookieHelper.GetWebClientIp();
                string insiteIp = CookieHelper.GetWebSiteIpAndPort();
                //string fwVersion = Environment.Version.ToString();
                //string port = CookieHelper.GetWebSitePort();
                string serverIp = HttpContext.Current.Request.ServerVariables.Get("Local_Addr");
                string company = PmsService.GetUserCompany(userAccount).CompanyName;
                string city = "";
                if (!iPAndCity.Keys.Contains(clientIp))
                {
                    city = GetCityByIP(clientIp);
                    iPAndCity.Add(clientIp, city);
                }
                else
                {
                    city = iPAndCity[clientIp];
                }
                //{0}          - {1}          {2}          [{3}]         {4}   {5}      {6}             {7}            {8}          {9}
                //$remote_addr - $remote_user $remote_city [$time_local] $host $request $requestconcent $upstream_addr $server_addr $remote_company
                string logConcentFormat = "{0} - {1} {2} [{3}] {4} {5} {6} {7} {8} {9}";
                string logConcent = string.Format(logConcentFormat, clientIp, userAccount, city,
                    DateTime.Now.ToString("yyyyMMddHHmmss"), httpClientDoMain, http_referer, concent, insiteIp, serverIp,
                    company);
                string token = DateTime.Now.ToString("yyyyMMdd") + ".log";
                string logRoot = FileStreamHelper.GetPathAtApplication("Logs\\" + DateTime.Now.ToString("yyyy"));
                if (!Directory.Exists(logRoot))
                {
                    Directory.CreateDirectory(logRoot);
                }
                StreamWriter sw = new StreamWriter(logRoot + "\\" + token, true);
                sw.WriteLine(logConcent);//写入
                sw.Close();
            }
            catch (Exception ex)
            {
                Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 操作结果返回
        /// </summary>
        /// <remark>
        ///     <para>    Creator：贺隽 </para>
        ///     <para>CreatedTime：2016-4-1</para>
        /// </remark>
        private class IPAndCity
        {
            /// <summary>
            /// 是否成功
            /// </summary>
            public int code { set; get; }

            /// <summary>
            /// 附加数据
            /// </summary>
            public IPAndCityInfo data { set; get; }

            public IPAndCity()
            {
                code = 0;
            }

        }

        /// <summary>
        /// 操作结果返回
        /// </summary>
        /// <remark>
        ///     <para>    Creator：贺隽 </para>
        ///     <para>CreatedTime：2016-4-1</para>
        /// </remark>
        private class IPAndCityInfo
        {
            public string country { set; get; }
            public string country_id { set; get; }
            public string area { set; get; }
            public string area_id { set; get; }
            public string region { set; get; }
            public string region_id { set; get; }
            public string city { set; get; }
            public string city_id { set; get; }
            public string county { set; get; }
            public string county_id { set; get; }
            public string isp { set; get; }
            public string isp_id { set; get; }
            public string ip { set; get; }
        }
    }

}