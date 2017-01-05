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
    public interface IAuthCheckAdapter
    {
        ReportModel GetReportDetail(string reportno, string trust);
    }
}
