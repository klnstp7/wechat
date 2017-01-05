using Peacock.InWork4.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peacock.InWork4.Model
{
    public class ProjectChargeModel
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public long TID { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        public string ProjectNo { get; set; }

        /// <summary>
        /// 报告号
        /// </summary>
        public string ReportNo { get; set; }

       
        /// <summary>
        /// 报告类型(正式报告/非正式报告)
        /// </summary>
        public string ReportCategory { get; set; }
        /// <summary>
        /// 物业类型
        /// </summary>
        public string ProjectType { get; set; }
        /// <summary>
        /// 子物业类型
        /// </summary>
        public string ChildPropertyType { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string CalculateType{ get; set; }
        /// <summary>
        /// 评估总价
        /// </summary>
        public string EvaluateTotal { get; set; }
     
        /// <summary>
        /// 项目地址
        /// </summary>
        public string ResidentialAreaAddress { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public string ResidentialAreaName { get; set; }

        /// <summary>
        /// 标准收费
        /// </summary>
        public decimal StandardFee { get; set; }

        /// <summary>
        /// 调后应收
        /// </summary>
        public decimal AdjustFee { get; set; }

        /// <summary>
        /// 免单金额
        /// </summary>
        public decimal FreePrice { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal ActualFee { get; set; }

        /// <summary>
        /// 未确认金额
        /// </summary>
        public decimal NotConfirmFee { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public StateFlag CurrentState { get; set; }
    }
}
