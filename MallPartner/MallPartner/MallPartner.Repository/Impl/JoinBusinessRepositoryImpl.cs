using StMallB.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using System.Data.SqlClient;

namespace StMallB.Repository.Impl
{
    public class JoinBusinessRepositoryImpl : Repository<JoinBusiness>, IJoinBusinessRepository
    {
        public JoinBusinessRepositoryImpl(UnitOfWork.Impl.UnitOfWork uow)
            : base(uow) { }


        /// <summary>
        /// 根据账号获取合作商
        /// </summary>
        /// <param name="busAccount">合作商账号</param>
        /// <returns></returns>
        public JoinBusiness GetJoinBusinessByBusAccount(string busAccount)
        {
            if (string.IsNullOrEmpty(busAccount))
            {
                throw new ArgumentNullException("busAccount");
            }
            return context.Query<JoinBusiness>("select top 1 * from StMall_JoinBusiness where BusAccount = @BusAccount", new { BusAccount = busAccount }).FirstOrDefault();
        }

        /// <summary>
        /// 增加积分
        /// </summary>
        /// <param name="integral">积分</param>
        /// <param name="busAccount">合作商登录帐号</param>
        public void IncreaseIntegral(decimal integral, string busAccount)
        {
            if (string.IsNullOrEmpty(busAccount))
            {
                throw new ArgumentNullException("busAccount");
            }

            context.Execute(
                "UPDATE StMall_JoinBusiness SET BusIntegral = BusIntegral+@BusIntegral WHERE BusAccount = @BusAccount",
                param: new { BusIntegral = integral, BusAccount = busAccount }
            );
        }

        public IList<JoinBusiness> Find(int pageNum, int pageSize, string keyword, out long totalCount)
        {
            PredicateGroup predict = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                predict = new PredicateGroup()
                {
                    Operator = GroupOperator.Or,
                    Predicates = new List<IPredicate>()  {
                            Predicates.Field<JoinBusiness>(x => x.BusAccount, Operator.Like, "%" + keyword + "%"),
                            Predicates.Field<JoinBusiness>(x => x.BusName, Operator.Like, "%" + keyword + "%"),
                            Predicates.Field<JoinBusiness>(x => x.BusAddress, Operator.Like, "%" + keyword + "%")
                        }
                };
            }
            var list = Find(predict, new List<ISort>() { new Sort() { PropertyName = "BusAddTime", Ascending = false } }, pageNum, pageSize, out totalCount);

            return list;
        }

        public IList<JoinBusiness> FindByRelation(int pageNum, int pageSize, long relation, string keyword, out long totalCount)
        {
            PredicateGroup predictKeyword = new PredicateGroup()
            {
                Operator = GroupOperator.Or,
                Predicates = new List<IPredicate>()  
                {
                            Predicates.Field<JoinBusiness>(x => x.BusAccount, Operator.Like, "%" + keyword + "%"),
                            Predicates.Field<JoinBusiness>(x => x.BusName, Operator.Like, "%" + keyword + "%"),
                            Predicates.Field<JoinBusiness>(x => x.BusAddress, Operator.Like, "%" + keyword + "%")
                }
            };

            PredicateGroup predictMain = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()  
                {
                    predictKeyword,
                    Predicates.Field<JoinBusiness>(x => x.BusRelation, Operator.Eq, relation)
                }
            };

            var list = Find(predictMain, new List<ISort>() { new Sort() { PropertyName = "BusAddTime", Ascending = false } }, pageNum, pageSize, out totalCount);

            return list;
        }

        public JoinBusiness GetJoinBusinessByBusName(string busName)
        {
            if (string.IsNullOrEmpty(busName))
            {
                throw new ArgumentNullException("busName");
            }
            return context.Query<JoinBusiness>("select top 1 * from StMall_JoinBusiness where BusName = @BusName", new { BusName = busName }).FirstOrDefault();
        }


        public IList<JoinBusiness> FindByRelation(long relation)
        {
            return context.Connection.GetList<JoinBusiness>(
                   Predicates.Field<JoinBusiness>(x => x.BusRelation, Operator.Eq, relation),
                   new List<ISort>() { new Sort() { PropertyName = "BusAddTime", Ascending = false } }).ToList();
        }
    }
}
