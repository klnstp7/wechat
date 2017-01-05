using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Peacock.PEP.WeChat.Model.DTO;
using Peacock.PEP.WeChat.Model.Enum;
using Peacock.PEP.WeChat.Service;
using Peacock.PMS.Service.Dto;
using Peacock.PEP.WeChat.Model.ApiModel;
using System.Configuration;

namespace Peacock.PEP.WeChat.Web.Controllers
{
    public class ChargeController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.CurrentType = ChargeEnum.我的任务.GetHashCode();
            if (!string.IsNullOrEmpty(Request.QueryString["currentType"]))
            {
                ViewBag.CurrentType = int.Parse(Request.QueryString["currentType"]);
            }

            var notMenuKeys = new List<string> { "InWork.WeChat", "Wechat.FeeModify" };
            //菜单控制
            var permissionList = UserService.Instance.GetUserPermissions();
            var menuList = permissionList.Where(p => p.ModulName == "微信端" && !notMenuKeys.Contains(p.PowerCode) && p.BelongSystem == BelongSystem.估价流程与管理系统).OrderBy(p => p.SortOrder);
            ViewBag.MenuList = menuList.Select(x => new MenuModel()
                {
                    Name = x.PowerName,
                   Url=  x.LinkUrl
                }).ToList();
            return View();
        }

        #region 项目跟进
        /// <summary>
        /// 项目跟进列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult ItemDetails(string condition)
        {
            ViewBag.ChargeList = ChargeService.Instance.GetChargeList(condition, ChargeEnum.项目跟进);
            return View();
        }
        #endregion

        #region 项目反馈
        /// <summary>
        /// 项目反馈列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult FeedbackDetails(string condition)
        {
            ViewBag.ChargeList = ChargeService.Instance.GetChargeList(condition, ChargeEnum.反馈项目);
            UserApiDto user = UserService.Instance.GetCurrentUser();
            ViewBag.CompayName = UserService.Instance.GetUserCompany(user.UserAccount).CompanyName;
            return View();
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phones"></param>
        /// <param name="username"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public ActionResult SendSMS(string username, string phones, string content, long projectId)
        {
            ResponseResult result = ChargeService.Instance.SendSMS(username, phones, content, projectId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 收费确认
        /// <summary>
        /// 收费确认列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Receivables(string condition)
        {
            ViewBag.ChargeList = ChargeService.Instance.GetChargeList(condition, ChargeEnum.收款确认);
            //项目收费权限控制
            string userAccount = UserService.Instance.GetCurrentUser().UserName;            
            ViewBag.ModifyFee = UserService.Instance.ValidUserPermission(userAccount, "Wechat.FeeModify");
            ViewBag.FinanceApply =UserService.Instance.ValidUserPermission(userAccount, "Market.ChargeConfirm.Apply");  
            ViewBag.CheckUsers = UserService.Instance.GetUserDataList("ProjectFinanceApply.Check");            
            return View();
        }

        /// <summary>
        /// 收费确认
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult Confirm(long projectId)
        {
            string userAccount = UserService.Instance.GetCurrentUser().UserAccount;
            ResponseResult result = ChargeService.Instance.ChargeConfirm(projectId, userAccount);
            return Json(result, JsonRequestBehavior.AllowGet);           
        }

        /// <summary>
        /// 最低收费修改
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="standardFee"></param>
        /// <returns></returns>
        public ActionResult UpdateStandardFee(long projectId, string standardFee)
        {
            ResponseResult result = ChargeService.Instance.UpdateStandardFee(projectId, standardFee);
            return Json(result, JsonRequestBehavior.AllowGet);     
        }

        /// <summary>
        /// 应收金额修改
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="adjustFee"></param>
        /// <returns></returns>
        public ActionResult UpdateAdjustFee(long projectId, string adjustFee)
        {
            ResponseResult result = ChargeService.Instance.UpdateAdjustFee(projectId, adjustFee);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 应收审批

        /// <summary>
        /// 应收审批列表
        /// </summary>
        /// <returns></returns>
        public ActionResult FinanceAudit(string condition)
        {
            var financeAuditList = ChargeService.Instance.GetFinanceAuditList(condition);
            ViewBag.FinanceAuditList = financeAuditList;
            return View();
        }

        /// <summary>
        /// 应收审批
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="isPass"></param>
        /// <param name="checkMessage"></param>
        /// <returns></returns>
        public ActionResult FinanceAuditSumbit(long tid, bool isPass, string checkMessage)
        {
            ResponseResult result = ChargeService.Instance.FinanceAudit(tid, isPass, checkMessage);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 应收金额申请
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="applyReason"></param>
        /// <param name="checkName"></param>
        /// <returns></returns>
        public ActionResult FinanceApply(long projectId, string applyReason, string checkName)
        {
            ResponseResult result = ChargeService.Instance.FinanceApply(projectId, applyReason, checkName);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 项目三审
        /// <summary>
        /// 项目三审列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ThreeAudit(string condition)
        {
            var projectAuditList = ChargeService.Instance.GetProjectAuditList(condition);
            ViewBag.ProjectAuditList = projectAuditList;
            return View();
        }

        /// <summary>
        /// 项目三审
        /// </summary>
        /// <param name="projectNo"></param>
        /// <param name="isPass"></param>
        /// <returns></returns>
        public ActionResult ProjectAuditSumbit(string projectNo, bool isPass)
        {
            ResponseResult result = ChargeService.Instance.ProjectAudit(projectNo, isPass);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 汇总信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Information()
        {
            return View();
        }
        #endregion 

        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>
        public ActionResult AboutUS()
        {
            return View();
        }
    }
}