using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StMallB.Entity;
using Dapper;
using DapperExtensions;
namespace StMallB.Repository.Impl
{
    public class MemberIntegralRepositoryImpl : Repository<MemberIntegral>, IMemberIntegralRepository
    {
        public MemberIntegralRepositoryImpl(UnitOfWork.Impl.UnitOfWork uow)
            : base(uow) { }

        /// <summary>
        /// 判断用户是否存在,通过userName获取MemberIntegral
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public MemberIntegral GetMemberIntegralByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("用户名为空");
            }
            return context.Query<MemberIntegral>("select top 1 * from StMall_Members where UserName = @UserName", new { UserName = userName }).FirstOrDefault();
        }
        /// <summary>
        /// 判断是否第一次充值，通过userName获取MemberIntegral
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public MemberIntegral GetMemberIntegral(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("用户名为空");
            }
            return context.Query<MemberIntegral>("select top 1 * from StMall_MemberIntegral where UserName=@UserName", new { UserName = userName }).FirstOrDefault();
        }
        /// <summary>
        /// 第一次充值，新建MemberIntegral
        /// </summary>
        /// <param name="integral"></param>
        /// <param name="userName"></param>
        public void FirstIncreaseIntegral(MemberIntegral entity)
        {
            if (entity==null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Connection.Insert<MemberIntegral>(entity);
        }
        /// <summary>
        /// 积分充值，修改
        /// </summary>
        /// <param name="integral"></param>
        /// <param name="userName"></param>
        public void IncreaseIntegral(decimal integral, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("用户名为空");
            }
            context.Execute(
                "UPDATE StMall_MemberIntegral SET AvailableIntegrals = AvailableIntegrals+@AvailableIntegrals WHERE UserName = @UserName",
                param: new { AvailableIntegrals = integral, UserName = userName }
            );
        }
        public IList<MemberIntegral> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(MemberIntegral entity)
        {
            throw new NotImplementedException();
        }

        public void Update(MemberIntegral entity)
        {
            throw new NotImplementedException();
        }

    }
}
