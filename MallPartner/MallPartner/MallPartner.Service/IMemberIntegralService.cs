using MallPartner.DataObject;
using StMallB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Service
{
    public interface IMemberIntegralService
    {
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="phone"></param>
        bool IsExist(string phone);
        /// <summary>
        /// 用户充值积分
        /// </summary>
        /// <param name="integral">输入积分</param>
        /// <param name="userName">用户名称</param>
        /// <param name="entity">积分记录</param>
        void RechargeIntegral(MemberIntegralRecord entity);
        /// <summary>
        /// 显示积分充值历史记录
        /// </summary>
        /// <param name="userName"></param>
        Page<MemberIntegralRecord> ShowHistoryRecord(int pageNum, int pageSize, string keyword,string userName);
    }
}
