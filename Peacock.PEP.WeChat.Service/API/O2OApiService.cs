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
    public class O2OApiService : SingModel<O2OApiService>
    {
        private RestClient _client;
        private RestRequest _request;
        private string _apiUrl;
        private O2OApiService()
        {
            _apiUrl = ConfigurationManager.AppSettings["O2OAPI"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseResult GetReportDetail(string reprotNo, ReportRequest request)
        {
            return Get<ReportRequest, ResponseResult>(request, string.Format("api/appraisal/{0}/getInfo.do",reprotNo));
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
                          + HttpUtility.UrlEncode((request.GetType().InvokeMember(m.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty, null, request, null) ?? string.Empty).ToString(), Encoding.UTF8));

                    foreach (var param in list)
                    {
                        var parmArr = param.Split(':');
                        restRequest.AddParameter(parmArr[0], HttpUtility.UrlDecode(parmArr[1]), ParameterType.QueryString);
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
                          + HttpUtility.UrlEncode((request.GetType().InvokeMember(m.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty, null, request, null) ?? string.Empty).ToString(), Encoding.UTF8));

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
