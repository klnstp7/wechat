using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Peacock.PMS.Service.Dto;
using Peacock.PMS.Service.Enum;
using Peacock.PMS.Service.Services;
using Newtonsoft.Json.Converters;
using Peacock.Common.Helper;
using System.Web.Security;
using Peacock.PEP.WeChat.Model.ApiModel;
using Peacock.PEP.WeChat.Model.DTO;
using Peacock.PEP.WeChat.Model.Enum;
using Newtonsoft.Json;
using System.Collections;


namespace Peacock.PEP.WeChat.Service
{

    public class AuthCheckService : SingModel<AuthCheckService>
    {

        private AuthCheckService()
        {
        }

       /// <summary>
       /// 获取报告明细
       /// </summary>
       /// <param name="reportno"></param>
       /// <param name="trust"></param>
       /// <returns></returns>
        public ReportModel GetReportDetail(string reportno, string trust)
        {
            LogHelper.Ilog(string.Format("GetReportDetail?reportno={0}&trust={1}", reportno, trust), "微信端真伪查询-" + Instance.ToString());
            ReportModel report = null;
            ResponseResult response = null;
            
            //获取明细
            //string userKeyId = string.Format("{0}", ConfigurationManager.AppSettings["O2OUserKeyId"]);
            //string userAccessKey = string.Format("{0}", ConfigurationManager.AppSettings["O2OUserAccessKey"]);         
            //string timestamp=Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
            //string signValue = RsaHelperSdk.RsaHelper.computeSignature(userKeyId, timestamp, userAccessKey);//.Replace("+", "%2B").Replace("/", "%2F");
            string tokenValue = string.Format("{0}", ConfigurationManager.AppSettings["O2OToken"]);

            ReportRequest reportRequest = new ReportRequest()
            {
                city ="北京" ,
                //userKeyId = userKeyId,
                //time = timestamp,
                //sign = signValue
                token = tokenValue,
                status = (int)StateFlag.已盖章
            };
            response = API.O2OApiService.Instance.GetReportDetail(reportno, reportRequest);
            if (response.Success == true)
            {
                report = JsonConvert.DeserializeObject<ReportModel>(string.Format("{0}", response.Data));
            }

            if (report != null)
            {
                if (!report.evalEntrust.Contains(trust)) return null;

                //获取公司
                ReportDepartRequest reportDepartRequest = new ReportDepartRequest()
                {
                    reportno = reportno,
                    delegateName = report.evalEntrust
                };
                response = API.NeiYeApiService.Instance.GetReportCrmDeparment(reportDepartRequest);
                if (response.Success == true)
                {
                    long departmentId = long.Parse(string.Format("{0}", response.Data));
                    var company = DepartmentService.Instance.GetBaseCompany(departmentId);
                    report.companyname = company == null ? "" : company.CompanyName;

                     //公司名称映射关系
                    Hashtable ReportCompanyMapHT = (Hashtable)ConfigurationManager.GetSection("ReportCompanyMap");
                    if(ReportCompanyMapHT.Contains(report.companyname))
                    {
                        report.companyname = string.Format("{0}",ReportCompanyMapHT[report.companyname]);
                    }
                }
            }
            return report;
        }       
        
    }
}
