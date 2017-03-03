using StMallB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Repository
{
    public interface IJoinBusinessRepository : IRepository<JoinBusiness>
    {
        /// <summary>
        /// 根据账号获取合作商
        /// </summary>
        /// <param name="busAccount">合作商账号</param>
        /// <returns></returns>
        JoinBusiness GetJoinBusinessByBusAccount(string busAccount);

        /// <summary>
        /// 根据合作商家公司名称获取合作商
        /// </summary>
        /// <param name="busName">合作商家公司名称</param>
        /// <returns></returns>
        JoinBusiness GetJoinBusinessByBusName(string busName);

        /// <summary>
        /// 增加积分
        /// </summary>
        /// <param name="integral">积分</param>
        /// <param name="busAccount">合作商登录帐号</param>
        void IncreaseIntegral(decimal integral, string busAccount);

        IList<JoinBusiness> Find(int pageNum, int pageSize, string keyword, out long totalCount);

        /// <summary>
        /// 根据级别关系获取下级代理商
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="relation"></param>
        /// <param name="keyword"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IList<JoinBusiness> FindByRelation(int pageNum, int pageSize, long relation, string keyword, out long totalCount);

        /// <summary>
        /// 根据级别关系获取所有下级代理商
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        IList<JoinBusiness> FindByRelation(long relation);
    }
}
