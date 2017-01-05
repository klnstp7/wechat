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
using Newtonsoft.Json;
using Peacock.Common.Helper;
using System.Web.Security;
using Peacock.PEP.WeChat.Model.ApiModel;
using Peacock.PEP.WeChat.Model.DTO;
using Peacock.PEP.WeChat.Model.Enum;

namespace Peacock.PEP.WeChat.Service
{
   
    public class ChargeService : SingModel<ChargeService>
    {

        private ChargeService()
        {

        }

        /// <summary>
        /// 获取流程跟进，项目反馈，收费确认列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="chargeType"></param>
        /// <returns></returns>
        public IList<ChargeModel> GetChargeList(string condition,ChargeEnum chargeType)
        {
            switch (chargeType)
            {
                case ChargeEnum.收款确认:
                    LogHelper.Ilog(string.Format("GetChargeList?condition={0}&chargeType={1}", condition, chargeType), "微信端获取确认收费列表-" + Instance.ToString());
                    break;
                case ChargeEnum.项目跟进:
                    LogHelper.Ilog(string.Format("GetChargeList?condition={0}&chargeType={1}", condition, chargeType), "微信端获取流程跟踪列表-" + Instance.ToString());
                    break;
                case ChargeEnum.反馈项目:
                    LogHelper.Ilog(string.Format("GetChargeList?condition={0}&chargeType={1}", condition, chargeType), "微信端获取项目反馈列表-" + Instance.ToString());
                    break;
                default:
                    break;
            }           
            IList<ChargeModel> list = new List<ChargeModel>();
            try 
            { 
                var userAccount = UserService.Instance.GetCurrentUser().UserAccount;
                ChargeRequest request = new ChargeRequest()
                {
                    SearchNo = condition,
                    UserName = userAccount,
                    SearchFlag = chargeType.GetHashCode(),
                    CompanyId = UserService.Instance.GetUserCompany(userAccount).Id
                };
                ResponseResult response = API.NeiYeApiService.Instance.GetChargeList(request);
                if (response.Success == true && response.Data != null)
                {
                    list = JsonConvert.DeserializeObject<IList<ChargeModel>>(string.Format("{0}",response.Data));
                    var curUserName = UserService.Instance.GetCurrentUser().UserName;
                    foreach (var item in list)
                    {
                        item.FeeModifyAuth = curUserName == item.MarketEmployeeName;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, null);
            }
            return list;
        }

       /// <summary>
        /// 发送短信
       /// </summary>
       /// <param name="username"></param>
       /// <param name="phones"></param>
       /// <param name="content"></param>
       /// <param name="projectId"></param>
       /// <returns></returns>
        public ResponseResult SendSMS(string username, string phones,string content, long projectId)
        {
            LogHelper.Ilog(string.Format("SendSMS?username={0}&phones={1}&content={2}&projectId={3}", username, phones, content, projectId), "微信端发送短信-" + Instance.ToString());
            SMSRequest request = new SMSRequest()
            {
                Phones = phones,
                UserName=UserService.Instance.GetCurrentUser().UserName,
                Content=content,
                ProjectId=projectId,
                Company= UserService.Instance.GetUserCompany().CompanyName
            };
            return API.NeiYeApiService.Instance.SendSMS(request);
        }

        #region 收费确认
        /// <summary>
        /// 收费确认
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ResponseResult ChargeConfirm(long projectId,string userAccount)
        {
            LogHelper.Ilog(string.Format("ChargeConfirm?projectId={0}&userAccount={1}", projectId, userAccount), "微信端进行收费确认-" + Instance.ToString());
            ProjectRequest request = new ProjectRequest()
            {
                projectId = projectId,
                UserAccount=userAccount
            };
            return API.NeiYeApiService.Instance.ChargeConfirm(request); 
        }


        /// <summary>
        /// 最低收费修改
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="standardFee"></param>
        /// <returns></returns>
        public ResponseResult UpdateStandardFee(long projectId, string standardFee)
        {
            LogHelper.Ilog(string.Format("UpdateStandardFee?projectId={0}&standardFee={1}", projectId, standardFee), "微信端最低收费修改-" + Instance.ToString());
            UpdateStandardFeeRequest request = new UpdateStandardFeeRequest()
            {
                ProjectId = projectId,
                StandardFee = standardFee,
                UserName = UserService.Instance.GetCurrentUser().UserAccount
            };
            return API.NeiYeApiService.Instance.UpdateStandardFee(request);
        }

        /// <summary>
        /// 应收金额修改
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="adjustFee"></param>
        /// <returns></returns>
        public ResponseResult UpdateAdjustFee(long projectId, string adjustFee)
        {
            LogHelper.Ilog(string.Format("UpdateAdjustFee?projectId={0}&adjustFee={1}", projectId, adjustFee), "微信端应收金额修改-" + Instance.ToString());
            UpdateAdjustFeeRequest request = new UpdateAdjustFeeRequest()
            {
                ProjectId = projectId,
                AdjustFee = adjustFee,
                UserName = UserService.Instance.GetCurrentUser().UserAccount
            };
            return API.NeiYeApiService.Instance.UpdateAdjustFee(request);
        }

        #endregion

        #region 应收审批
        /// <summary>
        /// 应收审批列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IList<FinanceAuditModel> GetFinanceAuditList(string condition)
        {
            LogHelper.Ilog(string.Format("GetFinanceAuditList?condition={0}", condition), "微信端获取应收审批列表-" + Instance.ToString());
            IList<FinanceAuditModel> list = new List<FinanceAuditModel>();
            try
            {
                var userAccount = UserService.Instance.GetCurrentUser().UserAccount;
                FinanceAuditListRequest request = new FinanceAuditListRequest()
                {
                    Content = condition,
                    UserName = userAccount
                };
                ResponseResult response = API.NeiYeApiService.Instance.GetFinanceAuditList(request);
                if (response.Success == true && response.Data != null)
                {
                    list = JsonConvert.DeserializeObject<IList<FinanceAuditModel>>(string.Format("{0}", response.Data));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, null);
            }
            return list;
        }

        /// <summary>
        /// 应收审批
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="isPass"></param>
        /// <param name="checkMessage"></param>
        /// <returns></returns>
        public ResponseResult FinanceAudit(long tid, bool isPass, string checkMessage)
        {
            LogHelper.Ilog(string.Format("FinanceAudit?tid={0}&ispass={1}&checkMessage={2}", tid, isPass, checkMessage), "微信端应收审批-" + Instance.ToString());
            var userAccount = UserService.Instance.GetCurrentUser().UserAccount;
            FinanceAuditRequest request = new FinanceAuditRequest()
            {
                TId = tid,
                IsPass = isPass,
                CheckMessage = checkMessage,
                UserName = userAccount
            };
            return API.NeiYeApiService.Instance.FinanceAudit(request);
        }

        /// <summary>
        /// 应收金额申请
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="applyReason"></param>
        /// <param name="checkName"></param>
        /// <returns></returns>
        public ResponseResult FinanceApply(long projectId, string applyReason, string checkName)
        {
            LogHelper.Ilog(string.Format("FinanceApply?projectId={0}&applyReason={1}&checkName={2}", projectId, applyReason, checkName), "微信端应收金额申请-" + Instance.ToString());
            FinanceApplyRequest request = new FinanceApplyRequest()
            {
               ProjectId = projectId,
               ApplyReason =applyReason,
               CheckName = checkName,
               ApplyUser = UserService.Instance.GetCurrentUser().UserName
            };
            return API.NeiYeApiService.Instance.FinanceApply(request);
        }

        #endregion

        #region 项目三审
        /// <summary>
        /// 项目三审列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IList<ProjectAuditModel> GetProjectAuditList(string condition)
        {
            LogHelper.Ilog(string.Format("GetProjectAuditList?condition={0}", condition), "微信端获取项目三审列表-" + Instance.ToString());
            IList<ProjectAuditModel> list = new List<ProjectAuditModel>();
            try
            {
                var userAccount = UserService.Instance.GetCurrentUser().UserAccount;
                ProjectAuditListRequest request = new ProjectAuditListRequest()
                {
                    Content = condition,
                    UserName = userAccount
                };
                ResponseResult response = API.NeiYeApiService.Instance.GetProjectAuditList(request);
                if (response.Success == true && response.Data != null)
                {
                    list = JsonConvert.DeserializeObject<IList<ProjectAuditModel>>(string.Format("{0}", response.Data));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, null);
            }
            return list;
        }

        /// <summary>
        /// 项目三审
        /// </summary>
        /// <param name="projectNo"></param>
        /// <param name="isPass"></param>
        /// <returns></returns>
        public ResponseResult ProjectAudit(string projectNo, bool isPass)
        {
            LogHelper.Ilog(string.Format("ProjectAudit?projectNo={0}", projectNo), "微信端项目三审-" + Instance.ToString());
            var userName = UserService.Instance.GetCurrentUser().UserName;
            ProjectAuditRequest request = new ProjectAuditRequest()
            {
                ProjectNo = projectNo,
                IsPass = isPass,
                UserName = userName
            };
            return API.NeiYeApiService.Instance.ProjectAuditSumbit(request);
        }
        #endregion
    }
}
