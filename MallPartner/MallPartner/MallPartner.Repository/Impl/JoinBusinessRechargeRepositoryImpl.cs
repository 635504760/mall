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
    public class JoinBusinessRechargeRepositoryImpl : Repository<JoinBusinessRecharge>, IJoinBusinessRechargeRepository
    {
        public JoinBusinessRechargeRepositoryImpl(UnitOfWork.Impl.UnitOfWork uow)
            : base(uow) { }

        public IList<Tuple<JoinBusinessRecharge, JoinBusiness>> FindByBusId(int pageNum, int pageSize, long busId, out long totalCount)
        {
            string sql = @"  select {0} from StMall_JoinBusinessRecharge t1
                            join StMall_JoinBusiness t2
                            on t1.BusId = t2.Id
                            where t1.BusId = @BusId ";

            string fieldspage = @"Row_number() over(order by t1.AddTime desc) as rownum ,t1.Id,t1.BusId,t1.RechargeIntegral,t1.AddTime,t1.Remark, t2.BusName, t2.BusAccount, t2.BusRelation";
            string fieldcount = "count(1)";

            string pagesql = string.Format("select * from ( {0}) A where A.rownum between @beginindex and @endindex ", string.Format(sql, fieldspage));
            string countsql = string.Format(sql, fieldcount);

            var models = context.Query<JoinBusinessRecharge, JoinBusiness, Tuple<JoinBusinessRecharge, JoinBusiness>>(
                pagesql,
                (joinBusinessRecharge, joinBusiness) =>
                {
                    return new Tuple<JoinBusinessRecharge, JoinBusiness>(joinBusinessRecharge, joinBusiness);
                },
                param: new
                {
                    BusId = busId,
                    BeginIndex = (pageNum - 1) * pageSize + 1,
                    EndIndex = pageNum * pageSize
                },
                splitOn: "BusName");

            totalCount = context.ExecuteScalar<int>(countsql, new { BusId = busId });

            return models.ToList();
        }

        public IList<Tuple<JoinBusinessRecharge, JoinBusiness>> FindByParentAccount(int pageNum, int pageSize, string keyword, string parentAccount, out long totalCount)
        {

            string sql = @"  select {0} from StMall_JoinBusinessRecharge t1
                            join StMall_JoinBusiness t2
                            on t1.BusId = t2.Id
                            where t2.BusRelation = (select t3.Id from StMall_JoinBusiness t3 where t3.BusAccount = @ParentAccount) ";

            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " and (t2.BusName like '%' + @Keyword + '%' or t2.BusAccount = @Keyword) ";

                if (keyword.Contains(' '))
                {
                    string k1 = keyword.Split(' ')[0];
                    string k2 = keyword.Split(' ')[1];

                    sql += string.Format(" or ((t2.BusName = '{0}' or t2.BusAccount = '{1}') or (t2.BusName = '{1}' or t2.BusAccount = '{0}'))", k1, k2);
                }
            }

            string fieldspage = @"Row_number() over(order by t1.AddTime desc) as rownum ,t1.Id,t1.BusId,t1.RechargeIntegral,t1.AddTime,t1.Remark, t2.BusName, t2.BusAccount, t2.BusRelation";
            string fieldcount = "count(1)";

            string pagesql = string.Format("select * from ( {0}) A where A.rownum between @beginindex and @endindex ", string.Format(sql, fieldspage));
            string countsql = string.Format(sql, fieldcount);

            var models = context.Query<JoinBusinessRecharge, JoinBusiness, Tuple<JoinBusinessRecharge, JoinBusiness>>(
                pagesql,
                (joinBusinessRecharge, joinBusiness) =>
                {
                    return new Tuple<JoinBusinessRecharge, JoinBusiness>(joinBusinessRecharge, joinBusiness);
                },
                param: new
                {
                    Keyword = keyword,
                    ParentAccount = parentAccount,
                    BeginIndex = (pageNum - 1) * pageSize + 1,
                    EndIndex = pageNum * pageSize
                },
                splitOn: "BusName");

            totalCount = context.ExecuteScalar<int>(countsql, new { Keyword = keyword, ParentAccount = parentAccount });

            return models.ToList();
        }
    }
}
