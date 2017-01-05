using System;
using System.Data;
using PermissionsMiddle.Dto;
using System.Collections.Generic;
using Peacock.PEP.WeChat.Model;
using Peacock.PEP.WeChat.Model.DTO;
using Peacock.PEP.WeChat.Model.Enum;
using Peacock.PEP.WeChat.DataAdapter.Interface;
using Peacock.PEP.WeChat.Service;
using Peacock.PEP.WeChat.Model.ApiModel;

namespace Peacock.PEP.WeChat.DataAdapter.Implement
{
    public class ChargeAdapter : IChargeAdapter
    {

        public IList<ChargeModel> GetChargeList(string condition, ChargeEnum chargeType)
        {
            return ChargeService.Instance.GetChargeList(condition, chargeType);
        }

        public ResponseResult ChargeConfirm(long projectId,string userAccount)
        {
            return ChargeService.Instance.ChargeConfirm(projectId,userAccount);
        }

        public ResponseResult SendSMS(string username, string phones, string content,long projectId)
        {
            return ChargeService.Instance.SendSMS(username, phones,content, projectId);
        }

        public ResponseResult UpdateAdjustFee(long projectId, string adjustFee)
        {
            return ChargeService.Instance.UpdateAdjustFee(projectId, adjustFee);
        }

        public ResponseResult UpdateStandardFee(long projectId, string standardFee)
        {
            return ChargeService.Instance.UpdateStandardFee(projectId, standardFee);
        }

        public IList<FinanceAuditModel> GetFinanceAuditList(string condition)
        {
            
        }
    }
}