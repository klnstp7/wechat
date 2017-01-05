
using PermissionsMiddle.Dto;
using System.Collections.Generic;
using Peacock.PEP.WeChat.Model;
using Peacock.PEP.WeChat.Model.ApiModel;

namespace Peacock.PEP.WeChat.DataAdapter.Interface
{
    public interface IUserAdapter
    {
       
        UserApiDto UserLogin(string userAccount, string password);

        UserApiDto GetCurrentUser();

        void Logout();

        ResponseResult SaveUserWeChat(long userId,string openId);

        ResponseResult DeleteUserWeChat(string openId);

        bool IsBinding(string openId);

        CompanyApiDto GetUserCompany(string userAccount = null);

        bool ValidUserPermission(string userAccount,string permissionCode);

        /// <summary>
        /// 根据AppCode获取用户权限
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        IList<UserPowerApiDto> GetUserPermissions(string userAccount = null);
    }
}
