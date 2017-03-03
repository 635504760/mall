using System.Collections.Generic;
using StMallB.Entity;

namespace StMallB.Repository
{
    public interface IChangePswRepository : IRepository<JoinBusiness>
    {
        /// <summary>
        /// 根据账号获取合作商
        /// </summary>
        /// <param name="busName"></param>
        /// <param name="busAccount">合作商账号</param>
        /// <returns></returns>
        JoinBusinessRecharge GetJoinBusinessByBusAccount(string busName);
        void ChangePsaaword(string busAccount,string oldPsw,string newPsw);
        IList<JoinBusinessRecharge> Find(int pageNum, int pageSize,long busId, out long totalCount);
        
    }
}