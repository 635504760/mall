using StMallB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Repository
{
    public interface IJoinBusinessRechargeRepository : IRepository<JoinBusinessRecharge>
    {
        /// <summary>
        /// 根据合作商家编号查找合作商家积分充值记录
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="BusId"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IList<Tuple<JoinBusinessRecharge, JoinBusiness>> FindByBusId(int pageNum, int pageSize, long busId, out long totalCount);

        /// <summary>
        /// 根据父级账号查找下级代理商积分充值记录
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <param name="parentAccount"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IList<Tuple<JoinBusinessRecharge, JoinBusiness>> FindByParentAccount(int pageNum, int pageSize, string keyword, string parentAccount, out long totalCount);
    }
}
