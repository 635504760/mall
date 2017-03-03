using StMallB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Repository
{
    public interface IMemberIntegralRepository : IRepository<MemberIntegral>
    {
        /// <summary>
        /// 通过userName获取MemberIntegral
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        MemberIntegral GetMemberIntegralByUserName(string userName);
        /// <summary>
        /// 判断是否第一次充值，通过userName获取MemberIntegral
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        MemberIntegral GetMemberIntegral(string userName);
        /// <summary>
        /// 用户充值积分
        /// </summary>
        /// <param name="integral">积分</param>
        /// <param name="userName">用户名称</param>
        void IncreaseIntegral(decimal integral, string userName);
           /// <summary>
        /// 第一次充值，新建MemberIntegral
        /// </summary>
        /// <param name="integral"></param>
        /// <param name="userName"></param>
        void FirstIncreaseIntegral(MemberIntegral entity);
    }
}
