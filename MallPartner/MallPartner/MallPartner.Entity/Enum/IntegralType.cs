﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Entity.Enum
{
    /// <summary>
    /// 积分类型
    /// </summary>
    public enum IntegralType
    {
        /// <summary>
        /// 消费
        /// </summary>
        [Description("交易获得")]
        Consumption = 1,

        [Description("积分抵扣")]
        Exchange = 2,
        /// <summary>
        /// 会员邀请
        /// </summary>
        [Description("会员邀请")]
        InvitationMemberRegiste = 3,
        /// <summary>
        /// 返利积分
        /// </summary>
        //[Description("返利积分")]
        //ProportionRebate = 4,
        /// <summary>
        /// 每日登录
        /// </summary>
        [Description("每日登录")]
        Login = 5,
        /// <summary>
        /// 绑定微信
        /// </summary>
        [Description("绑定微信")]
        BindWX = 6,
        /// <summary>
        /// 商品评论
        /// </summary>
        [Description("订单评价")]
        Comment = 7,
        /// <summary>
        /// 管理员操作
        /// </summary>
        [Description("管理员操作")]
        SystemOper = 8,
        /// <summary>
        /// 用户注册
        /// </summary>
        [Description("完善用户信息")]
        Reg = 9,

        [Description("取消订单")]
        Cancel = 10,

        [Description("其他")]
        Others = 11,
        /// <summary>
        /// 签到
        /// </summary>
        [Description("签到")]
        SignIn = 12,

        [Description("微信活动")]
        WeiActivity = 13,

        [Description("合作商赠送")]
        JoinBusinessGive = 14,
    }
}
