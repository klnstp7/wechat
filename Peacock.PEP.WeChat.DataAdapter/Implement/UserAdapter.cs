using System;
using System.Data;

using PermissionsMiddle.Dto;
using System.Collections.Generic;
using Peacock.PEP.WeChat.Model;
using Peacock.PEP.WeChat.Model.ApiModel;
using Peacock.PEP.WeChat.DataAdapter.Interface;
using Peacock.PEP.WeChat.Service;


namespace Peacock.PEP.WeChat.DataAdapter.Implement
{
    public class UserAdapter : IUserAdapter
    {

        public UserApiDto UserLogin(string userAccount, string password)
        {
            return UserService.Instance.UserLogin(userAccount, password);
        }

        public UserApiDto GetCurrentUser()
        {
            return UserService.Instance.GetCurrentUser();
        }

        public void Logout()
        {
            UserService.Instance.Logout();
        }

        public ResponseResult SaveUserWeChat(long userId,string openId)
        {
            return UserService.Instance.SaveUserWeChat(userId,openId);
        }

        public ResponseResult DeleteUserWeChat(string openId)
        {
            return UserService.Instance.DeleteUserWeChat(openId);
        }

        public bool IsBinding(string openId)
        {
            return UserService.Instance.IsBinding(openId);
        }

        public CompanyApiDto GetUserCompany(string userAccount = null)
        {
            return UserService.Instance.GetUserCompany(userAccount);
        }

        public bool ValidUserPermission(string userAccount, string permissionCode)
        {
            return UserService.Instance.ValidUserPermission(userAccount, permissionCode);
        }

        /// <summary>
        /// 根据AppCode获取用户权限
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public IList<UserPowerApiDto> GetUserPermissions(string userAccount = null)
        {
            return UserService.Instance.GetUserPermissions(userAccount);
        }
    }
}