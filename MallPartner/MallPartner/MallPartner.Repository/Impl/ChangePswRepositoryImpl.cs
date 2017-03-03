using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using StMallB.Entity;

namespace StMallB.Repository.Impl
{
    public class ChangePswRepositoryImpl : Repository<JoinBusiness>, IChangePswRepository
    {
        public ChangePswRepositoryImpl(UnitOfWork.Impl.UnitOfWork unitOfwork) : base(unitOfwork)
        {
        }

        public JoinBusinessRecharge GetJoinBusinessByBusAccount(string busName)
        {
            if (string.IsNullOrEmpty(busName))
            {
                throw new ArgumentNullException("busName");
            }
            return context.Query<JoinBusinessRecharge>("select top 1 * from StMall_JoinBusiness where BusAccount = @busName", new { BusAccount = busName }).FirstOrDefault();
        }

        public void ChangePsaaword(string busAccount, string oldPsw, string newPsw)
        {
            
            if (string.IsNullOrEmpty(busAccount))
            {
                throw new ArgumentNullException("busAccount");
            }
            context.Execute(
                "UPDATE StMall_JoinBusiness SET BusPassword = @newPsw WHERE BusAccount = @busAccount",
                param: new { newPsw = newPsw, BusAccount = busAccount }
                );
        }

        IList<JoinBusinessRecharge> IChangePswRepository.Find(int pageNum, int pageSize, long busId, out long totalCount)
        {
            throw new NotImplementedException();
        }

        public IList<JoinBusiness> Find(int pageNum, int pageSize, long busId, out long totalCount)
        {
            PredicateGroup predict = null;
            var list = Find(predict, new List<ISort>() { new Sort() { PropertyName = "AddTime", Ascending = false } }, pageNum, pageSize, out totalCount);

            return list;
        }

        public IList<JoinBusiness> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(JoinBusinessRecharge entity)
        {
            throw new NotImplementedException();
        }

        public void Update(JoinBusinessRecharge entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(JoinBusiness entity)
        {
            throw new NotImplementedException();
        }

        public void Update(JoinBusiness entity)
        {
            throw new NotImplementedException();
        }
    }
}
