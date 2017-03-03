using MallPartner.DataObject;
using StMallB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Service
{
    public interface IJoinBusinessRechargeService
    {
        /// <summary>
        /// 获取给下级代理商的积分充值记录
        /// </summary>
        Page<JoinBusinessRechargeDto> FindSubJoinBusinessRecharge(int pageNum, int pageSize, string keyword);

        /// <summary>
        /// 获取充值记录
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Page<JoinBusinessRechargeDto> FindJoinBusinessRecharge(int pageNum, int pageSize);
    }
}
