using MallPartner.DataObject;
using StMallB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Service
{
    public interface IJoinBusinessService
    {
        /// <summary>
        /// 合作商登录
        /// </summary>
        /// <param name="busAccount">账号</param>
        /// <param name="busPassword">密码</param>
        /// <returns>是否成功</returns>
        bool Login(string busAccount, string busPassword);

        /// <summary>
        /// 充值积分
        /// </summary>
        /// <param name="integral">积分</param>
        /// <param name="busAccount">合作商登录帐号</param>
        void RechargeIntegral(decimal integral, string busAccount);

        /// <summary>
        /// 开通下级代理商
        /// </summary>
        /// <param name="dto"></param>
        void CreateJoinBusiness(CreateJoinBusinessDto dto);

        Page<JoinBusiness> GetList(int pageNum, int pageSize, string keyword);

        /// <summary>
        /// 获取下级代理商
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Page<JoinBusiness> GetSubJoinBusinesses(int pageNum, int pageSize, string keyword);

        /// <summary>
        /// 下级代理商重置密码
        /// </summary>
        /// <param name="account"></param>
        void ResetSubPasswordPost(string account);

        /// <summary>
        /// 根据账户获取合作商
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        JoinBusiness GetJoinBusinessByAccount(string account);

        /// <summary>
        /// 获取所有下级代理商
        /// </summary>
        /// <returns></returns>
        IList<JoinBusiness> GetSubJoinBusinesses();
    }
}
