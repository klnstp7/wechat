using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peacock.PEP.WeChat.Model.Enum;

namespace Peacock.PEP.WeChat.Model.DTO
{
    public class ChargeModel
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        public string ProjectNo { get; set; }

        /// <summary>
        /// 报告号
        /// </summary>
        public string ReportNo { get; set; }

        /// <summary>
        /// 项目分类
        /// </summary>
        public string ReportType { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public string ItemType { get; set; }

        /// <summary>
        /// 项目地址
        /// </summary>
        public string ResidentialAreaAddress { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        public float BuildingArea { get; set; }

        /// <summary>
        /// 收费方式
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// 最低收费
        /// </summary>
        public decimal StandardFee { get; set; }

        /// <summary>
        /// 调后应收
        /// </summary>
        public decimal AdjustFee { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal ActualFee { get; set; }

        /// <summary>
        /// 免收金额
        /// </summary>
        public decimal FreeFee { get; set; }

        /// <summary>
        /// 是否反馈
        /// </summary>
        public bool? CustFeedBack { get; set; }

        /// <summary>
        /// 反馈时间
        /// </summary>
        public DateTime? CustFeedBackDate { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 最后反馈人
        /// </summary>
        public string FeedBackContent { get; set; }

        /// <summary>
        /// 最后反馈类型
        /// </summary>
        public string SendType { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public long? CustomerID { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary>
        public string CustomerTel { get; set; }

        /// <summary>
        /// 客户手机
        /// </summary>
        public string CustomerMobile { get; set; }

        /// <summary>
        /// 客户QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 看房联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// 看房联系人电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 评估总价（万元）
        /// </summary>

        public float EvaluateTotal { get; set; }

        /// <summary>
        /// 评估单价（元/㎡）
        /// </summary>

        public float EvaluatePrice { get; set; }

        /// <summary>
        /// 建筑面积（㎡）
        /// </summary>

        public decimal ArchitectureArea { get; set; }

        /// <summary>
        /// 建成年代
        /// </summary>

        public string BuiltYear { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int CurrentState { get; set; }

        /// <summary>
        /// 市场负责人
        /// </summary>
        public string MarketEmployeeName { get; set; }

        /// <summary>
        /// 收费确认权限
        /// </summary>
        public bool FeeModifyAuth { get; set; }

        /// <summary>
        /// 流程列表
        /// </summary>
        public IList<WorkFlowModel> FlowList { get; set; }

        /// <summary>
        /// 反馈列表
        /// </summary>
        public IList<FeedBackModel> FeedBackList { get; set; }
    }
}
