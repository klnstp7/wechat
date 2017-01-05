using System;
using System.Data;
using PermissionsMiddle.Dto;
using System.Collections.Generic;
using Peacock.PEP.WeChat.Model;
using Peacock.PEP.WeChat.Model.ApiModel;
using Peacock.PEP.WeChat.Model;
using Peacock.PEP.WeChat.Model.DTO;
using Peacock.PEP.WeChat.Model.Enum;

namespace Peacock.PEP.WeChat.DataAdapter.Interface
{
    public interface IChargeAdapter
    {
        IList<ChargeModel> GetChargeList(string condition, ChargeEnum chargeType);

        ResponseResult ChargeConfirm(long projectId,string userAccount);

        ResponseResult SendSMS(string username, string phones,string content,long projectId);

        ResponseResult UpdateAdjustFee(long projectId, string adjustFee);

        ResponseResult UpdateStandardFee(long projectId, string standardFee);

        IList<FinanceAuditModel> GetFinanceAuditList(string condition);

        ResponseResult FinanceAudit(long tid, bool isPass, string checkMessage);
    }
}
