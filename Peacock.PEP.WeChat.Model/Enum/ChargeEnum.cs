#region << 版 本 注 释 >>
/*
 * ========================================================================
 * Copyright(c) 2004-2015 广州光汇软件科技有限公司, All Rights Reserved.
 * ========================================================================
*/
#endregion

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peacock.PEP.WeChat.Model.Enum
{
    /// <summary>
    /// 出纳复核枚举状态
    /// </summary>
    /// <remarks>
    ///     <para>    Creator：hl</para>
    ///     <para>CreatedTime：2015/6/9 20:16:31</para>
    /// </remarks>
    public enum ChargeEnum
    {
        我的任务=0,
        收款确认=1,
        项目跟进=2,
        反馈项目=3
       
    }

    public enum SendTypeFlag
    {
        短信 = 1,
        App = 2
    }

    /// <summary>
    /// 内业状态
    /// </summary>
    public enum StateFlag
    {
        暂存 = 1, 立项 = 5, 资源调度 = 7,
        内业待分配 = 11,
        内业未接收 = 14,
        内业进行中 = 16,// 内业挂起 = 5,

        一审未接收 = 20, 一审审核中 = 21,
        二审未接收 = 22, 二审审核中 = 23,
        三审未接收 = 24, 三审审核中 = 25,
        四审未接收 = 26, 四审审核中 = 27,
        审核完成 = 29,
        未盖章 = 31, 已盖章 = 34,
        装订完成 = 36,

        完成 = 40,

        已撤销 = 55,

        //已变更 = 65,

        //已归档=75

    }


    public enum WeChatNoticeEnum
    {
        立项 = 1,
        外采已挂起 = 2,
        内页已挂起 = 3,
        预约看房 = 4,
        外采反馈 = 5,
        发送报告 = 6,
        三审完成 = 7
    }
}
