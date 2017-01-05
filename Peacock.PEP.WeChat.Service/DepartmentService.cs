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


namespace Peacock.PEP.WeChat.Service
{

    public class DepartmentService : SingModel<DepartmentService>
    {
        private static readonly string AppCode = ConfigurationManager.AppSettings["YewuAppCode"];
        private static readonly int CacheTime = Convert.ToInt32(ConfigurationManager.AppSettings["CrmCacheTime"]);
        private static readonly PmsService PmsService = new PmsService(AppCode, CacheTime);

        private DepartmentService()
        {

        }
  
        /// <summary>
        /// 获取所有组织架构（公司和部门）
        /// </summary>
        /// <returns></returns>
        internal IList<CompanyApiDto> GetAllStructures()
        {
            return PmsService.GetAllStructures();
        }

        /// <summary>
        /// 获取部门所属的公司
        /// </summary>
        /// <returns></returns>
        public CompanyApiDto GetBaseCompany(long departmentId)
        {
            var result = GetAllStructures().FirstOrDefault(x => x.Id == departmentId);
            if (result.Tag != StructureTag.Company)
            {
                result = GetBaseCompany(result.BaseCompanyId.Value);
            }

            return result;
        }

    }
}
