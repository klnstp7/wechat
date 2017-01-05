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
   
    public class UserService : SingModel<UserService>
    {
        private static readonly string AppCode = ConfigurationManager.AppSettings["YewuAppCode"];
        private static readonly int CacheTime = Convert.ToInt32(ConfigurationManager.AppSettings["CrmCacheTime"]);
        private static readonly PmsService PmsService = new PmsService(AppCode, CacheTime);
        

        private UserService()
        {

        }
        #region 用户操作
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserApiDto UserLogin(string userAccount, string password)
        {
            UserApiDto user = null;
            try
            {               
                user = PmsService.UserLogin(userAccount, password);
                if (user != null && user.Id > 0)
                {
                    this.SetAuth(user);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message,null);
            }
            
            return user;
        }

        /// <summary>
        /// 退出
        /// </summary>
        public void Logout()
        {
            CookieHelper.RemoveCookie(FormsAuthentication.FormsCookieName);
            FormsAuthentication.SignOut(); 
        }

        /// <summary>
        /// 赋权
        /// </summary>
        /// <param name="user"></param>
        private void SetAuth(UserApiDto user)
        {
            //直接把用户信息存入Cookie
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string strValues = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented, timeFormat);
            strValues = strValues.Replace("\r\n", "");
            //创建ticket
            var ticket = new FormsAuthenticationTicket(2, user.UserAccount, DateTime.Now, DateTime.Now.AddDays(FormsAuthentication.Timeout.Days), true, strValues);
            //加密ticket
            var cookieValue = FormsAuthentication.Encrypt(ticket);

            CookieHelper.WriteCookie(FormsAuthentication.FormsCookieName, cookieValue);

            //CookieHelper.SetCookieExpires(FormsAuthentication.FormsCookieName, FormsAuthentication.Timeout.Days);
        }
        #endregion

        #region Crm权限
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public UserApiDto GetCurrentUser()
        {
            UserApiDto user = null;
            string strCookie = CookieHelper.GetCookie(FormsAuthentication.FormsCookieName);
            if (!string.IsNullOrWhiteSpace(strCookie))
            {
                user = JsonConvert.DeserializeObject<UserApiDto>(FormsAuthentication.Decrypt(strCookie).UserData);
            }
            return user;
        }

        /// <summary>
        /// 获取CRM用户信息
        /// </summary>
        /// <returns></returns>
        private UserApiDto GetCrmUser(string userAccount = null)
        {
            string employeeName = string.Empty;
            if (userAccount == null)
            {
                var user = UserService.Instance.GetCurrentUser();
                if (user == null)
                {
                    throw new Exception("获取当前登录用户失败");
                }
                else
                {
                    employeeName = user.UserName;
                }
            }
            else
            {
                employeeName = userAccount;
            }
            return PmsService.GetUser(employeeName);
        }

        /// <summary>
        /// 获取当前用户所在的公司
        /// </summary>
        /// <returns></returns>
        public CompanyApiDto GetUserCompany(string userAccount = null)
        {
            var account = string.IsNullOrEmpty(userAccount) ? GetCrmUser(userAccount).UserAccount : userAccount;
            return PmsService.GetUserCompany(account);
        }

        /// <summary>
        /// 验证用户是否有权限
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        public bool ValidUserPermission(string userAccount, string permissionCode)
        {
            return PmsService.ValidUserPermission(userAccount, permissionCode);
        }

        /// <summary>
        /// 根据AppCode获取用户权限
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public IList<UserPowerApiDto> GetUserPermissions(string userAccount = null)
        {
            return PmsService.GetUserPermissions(GetCrmUser(userAccount).Id);
        }

        /// <summary>
        /// 获取用户 用于下拉列表
        /// </summary>
        /// <param name="permissionCode"></param>
        public IList<UserApiDto> GetUserDataList(string permissionCode)
        {          
            var userList = this.GetUserListByDepartment();
            var auditUserList = UserService.Instance.GetUsersByPermissionCode(permissionCode).Where(
                x => userList.Select(t => t.UserAccount).Contains(x.UserAccount)).ToList();
            return auditUserList;
        }


        /// <summary>
        /// 获取当前部门下所有用户列表(不包括子部门)
        /// </summary>
        /// <returns></returns>
        internal IList<UserApiDto> GetUserListByDepartment(string userAccount = null)
        {
            var department = GetUserDepartment(userAccount);
            return GetAllUsers().Where(x => x.CompanyId == department.Id).ToList();
        }

        /// <summary>
        /// 获取当前用户所在的部门
        /// </summary>
        /// <returns></returns>
        internal CompanyApiDto GetUserDepartment(string userAccount = null)
        {
            var account = string.IsNullOrEmpty(userAccount) ? GetCrmUser(userAccount).UserAccount : userAccount;
            return PmsService.GetUserDepartment(account);
        }

        /// <summary>
        /// 获取CRM所有用户
        /// </summary>
        /// <returns></returns>
        internal IList<UserApiDto> GetAllUsers()
        {
            return PmsService.GetAllUsers();
        }

        /// <summary>
        /// 根据权限编码获取用户列表
        /// </summary>
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        internal IList<UserApiDto> GetUsersByPermissionCode(string permissionCode)
        {
            return PmsService.GetUsersByPowerCode(permissionCode);
        }

        #endregion

        #region 微信
        /// <summary>
        /// 用户绑定OpenId
        /// </summary>
        public ResponseResult SaveUserWeChat(long userId,string openId)
        {            
            UserWeChatRequest request = new UserWeChatRequest()
            {
                UserId = userId,
                OpenId=openId
            };
            return API.UserApiService.Instance.SaveUserWeChat(request);
        }

        /// <summary>
        /// 解除绑定
        /// </summary>
        public ResponseResult DeleteUserWeChat(string openId)
        {
            UserWeChatRequest request = new UserWeChatRequest()
            {
                UserId = GetCurrentUser().Id,
                OpenId = openId
            };
            return API.UserApiService.Instance.DeleteUserWeChat(request);
        }

        /// <summary>
        /// 判断是否绑定OpenId
        /// </summary>
        /// <returns></returns>
        public bool IsBinding(string openId)
        {
            bool temp = false;
            UserWeChatRequest request = new UserWeChatRequest()
            {           
                UserId=0,
                OpenId = openId
            };
            ResponseResult response = API.UserApiService.Instance.GetUserWeChat(request);
            if (response.Success == true && response.Data!=null)
            {
                dynamic data = response.Data;
                long userId = Convert.ToInt64(data["UserId"]);
                if (userId > 0) 
                { 
                     UserApiDto user = PmsService.GetUser(userId);
                     this.SetAuth(user);
                     temp = true;
                }
            }
           
            return temp;
        }
        #endregion

       

    }
}
