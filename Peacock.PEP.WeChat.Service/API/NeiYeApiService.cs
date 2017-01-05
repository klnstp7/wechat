using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using System.Web;
using Peacock.Common.Helper;
using RestSharp;
using Newtonsoft.Json;
using Peacock.PEP.WeChat.Model.ApiModel;


namespace Peacock.PEP.WeChat.Service.API
{
    public class NeiYeApiService : SingModel<NeiYeApiService>
    {
        private RestClient _client;
        private RestRequest _request;
        private string _apiUrl;
        private NeiYeApiService()
        {
            _apiUrl = ConfigurationManager.AppSettings["NeiYeAPI"];
        }

        /// <summary>
        /// 流程跟进，项目反馈，收费确认
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseResult GetChargeList(ChargeRequest request)
        {
            return Post<ChargeRequest, ResponseResult>(request, string.Format("{0}","/api/Charge/GetChargeList"));
        }
      
        /// <summary>
        /// 发送短信接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseResult SendSMS(SMSRequest request)
        {
            return Post<SMSRequest, ResponseResult>(request, "/api/Charge/Send");
        }

        /// <summary>
        /// 收费确认接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseResult ChargeConfirm(ProjectRequest request)
        {
            return Get<ProjectRequest, ResponseResult>(request, "/api/Charge/Confirm");
        }

        /// <summary>
        /// 修改最低收费接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseResult UpdateStandardFee(UpdateStandardFeeRequest request)
        {
            return Get<UpdateStandardFeeRequest, ResponseResult>(request, "/api/Charge/UpdateStandardFee");
        }

        /// <summary>
        /// 修改应收金额接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseResult UpdateAdjustFee(UpdateAdjustFeeRequest request)
        {
            return Get<UpdateAdjustFeeRequest, ResponseResult>(request, "/api/Charge/UpdateAdjustFee");
        }

        /// <summary>
        /// 通过报告号获取所属公司
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseResult GetReportCrmDeparment(ReportDepartRequest request)
        {
            return Get<ReportDepartRequest, ResponseResult>(request, string.Format("{0}", "/Api/Projects/GetReportCrmDeparment"));
        }


        /// <summary>
        ///应收审批列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseResult GetFinanceAuditList(FinanceAuditListRequest request)
        {
            return Get<FinanceAuditListRequest, ResponseResult>(request, string.Format("{0}", "/api/Charge/GetFinanceAuditList"));
        }

        /// <summary>
        ///应收审批
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseResult FinanceAudit(FinanceAuditRequest request)
        {
            return Get<FinanceAuditRequest, ResponseResult>(request, string.Format("{0}", "/api/Charge/FinanceAudit"));
        }


        /// <summary>
        ///应收申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseResult FinanceApply(FinanceApplyRequest request)
        {
            return Post<FinanceApplyRequest, ResponseResult>(request, string.Format("{0}", "/api/Charge/FinanceApply"));
        }
        /// <summary>
        ///项目三审列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseResult GetProjectAuditList(ProjectAuditListRequest request)
        {
            return Get<ProjectAuditListRequest, ResponseResult>(request, string.Format("{0}", "/api/Charge/GetProjectAuditList"));
        }

        /// <summary>
        /// 项目三审
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseResult ProjectAuditSumbit(ProjectAuditRequest request)
        {
            return Get<ProjectAuditRequest, ResponseResult>(request, string.Format("{0}", "/api/Charge/ProjectAudit"));
        }

        protected string ApiBaseUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_apiUrl))
                {
                    throw new ConfigurationErrorsException("missing \"ApiUrl\" appSetting.");
                }
                return _apiUrl;
            }
        }

        protected RestRequest CreateRequest(Method method)
        {
            if (_request != null)
            {
                _request.Parameters.Clear();
                //return _request;
            }
            _request = new RestRequest(method);
            return _request;
        }

        protected RestClient CreateClient()
        {
            if (_client != null)
            {
                return _client;
            }
            _client = new RestClient { BaseUrl = new Uri(ApiBaseUrl) };

            return _client;
        }

        protected IRestResponse ClientExecute(RestRequest req)
        {
            _client = CreateClient();

            return _client.Execute(req);
        }

        protected string GetResponseContent(RestRequest req)
        {
            var response = GetResponse(req);

            if (!string.IsNullOrEmpty(response.Content))
            {
                return response.Content;
            }

            throw response.ErrorException;
        }

        protected IRestResponse GetResponse(RestRequest req)
        {
            return ClientExecute(req);
        }

        /// <summary>
        /// Post方式调用API
        /// </summary>
        protected TResult Post<TRequest, TResult>(TRequest request, string actionUrl)
        {
            return Execute<TRequest, TResult>(request, actionUrl, Method.POST);
        }

        /// <summary>
        /// Post方式调用API，但是返回html
        /// </summary>
        protected string Post<TRequest>(TRequest request, string actionUrl)
        {
            return Execute<TRequest>(request, actionUrl, Method.POST);
        }

        /// <summary>
        /// Get方式调用API
        /// </summary>
        protected TResult Get<TRequest, TResult>(TRequest request, string actionUrl)
        {
            return Execute<TRequest, TResult>(request, actionUrl, Method.GET);
        }


        private TResult Execute<TRequest, TResult>(TRequest request, string actionUrl, Method method)
        {
            var restRequest = CreateRequest(method);
            restRequest.Parameters.Clear();
            restRequest.Resource = actionUrl;

            switch (method)
            {
                case Method.GET:
                    var list = request.GetType()
                      .GetProperties()
                      .Where(p => p.PropertyType.IsValueType || p.PropertyType.Name.StartsWith("String"))
                      .Select(
                          m =>
                           HttpUtility.UrlEncode(m.Name, Encoding.UTF8) + ":"
                          + (request.GetType().InvokeMember(m.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty, null, request, null) ?? string.Empty).ToString());

                    foreach (var param in list)
                    {
                        var parmArr = param.Split(':');
                        restRequest.AddParameter(parmArr[0], parmArr[1], ParameterType.QueryString);
                    }
                    break;

                case Method.POST:
                    restRequest.AddJsonBody(request);
                    break;
            }

            var responseContent = GetResponseContent(restRequest);
            return JsonConvert.DeserializeObject<TResult>(responseContent);
        }

        private string Execute<TRequest>(TRequest request, string actionUrl, Method method)
        {
            var restRequest = CreateRequest(method);
            restRequest.Parameters.Clear();
            restRequest.Resource = actionUrl;

            switch (method)
            {
                case Method.GET:
                    var list = request.GetType()
                      .GetProperties()
                      .Where(p => p.PropertyType.IsValueType || p.PropertyType.Name.StartsWith("String"))
                      .Select(
                          m =>
                           HttpUtility.UrlEncode(m.Name, Encoding.UTF8) + ":"
                          + (request.GetType().InvokeMember(m.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty, null, request, null) ?? string.Empty).ToString());

                    foreach (var param in list)
                    {
                        var parmArr = param.Split(':');
                        restRequest.AddParameter(parmArr[0], parmArr[1], ParameterType.QueryString);
                    }
                    break;

                case Method.POST:
                    restRequest.AddJsonBody(request);
                    break;
            }

            var responseContent = GetResponseContent(restRequest);
            return responseContent;
        }
    }
}
