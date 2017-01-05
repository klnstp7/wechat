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
    public class AuthCheckAdapter : IAuthCheckAdapter
    {

        public ReportModel GetReportDetail(string reportno, string trust)
        {
            return AuthCheckService.Instance.GetReportDetail(reportno, trust);
        }
    }
}