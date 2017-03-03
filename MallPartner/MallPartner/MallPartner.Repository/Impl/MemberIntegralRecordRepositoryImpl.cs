using StMallB.Entity;
using StMallB.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using DapperExtensions;
using System.Threading.Tasks;

namespace StMallB.Repository.Impl
{
    public class MemberIntegralRecordRepositoryImpl : Repository<MemberIntegralRecord>, IMemberIntegralRecordRepository
    {
        public MemberIntegralRecordRepositoryImpl(UnitOfWork.Impl.UnitOfWork uow)
            : base(uow) { }

        /// <summary>
        /// 积分充值，添加记录
        /// </summary>
        /// <param name="integral"></param>
        /// <param name="userName"></param>
        //public void InsertMemberIntegralRecord(long MemberId,decimal integral, string userName,IntegralType typeId)
        //{
        //    if (string.IsNullOrEmpty(userName))
        //    {
        //        throw new ArgumentNullException("userName");
        //    }
        //    context.Execute(
        //        "insert into StMall_MemberIntegralRecord (Id,MemberId,UserName,TypeId,Integral,RecordDate,Remark,SupplierId) values(@Id,@MemberId,@UserName,@TypeId,@Integral,@RecordDate,@ReMark,@SupplierId)",
        //        param: new {UserName=userName },
        //    );
        //}
        public IList<MemberIntegralRecord> GetAll(int pageNum, int pageSize, string keyword, long busId, out long totalCount)
        {

            PredicateGroup orPredict = new PredicateGroup()
            {
                Operator = GroupOperator.Or,
                Predicates = new List<IPredicate>()  {
                            Predicates.Field<MemberIntegralRecord>(x => x.RecordDate, Operator.Like, "%" + keyword + "%"),
                            Predicates.Field<MemberIntegralRecord>(x => x.ReMark, Operator.Like, "%" + keyword + "%"),
                            Predicates.Field<MemberIntegralRecord>(x => x.UserName, Operator.Like, "%" + keyword + "%"),
                            Predicates.Field<MemberIntegralRecord>(x => x.Integral, Operator.Like, "%" + keyword + "%"),
                        }
            };
            PredicateGroup andPredict = new PredicateGroup()
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()  {
                            orPredict,
                            Predicates.Field<MemberIntegralRecord>(x => x.TypeId, Operator.Eq,(int)IntegralType.JoinBusinessGive ),
                            Predicates.Field<MemberIntegralRecord>(x => x.BusId, Operator.Eq, busId)
                        }
            };

            var list = Find(andPredict, new List<ISort>() { new Sort() { PropertyName = "RecordDate", Ascending = false } }, pageNum, pageSize, out totalCount);
            return list;
        }

        public override void Insert(MemberIntegralRecord entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            //context.Execute(
            //     "insert into StMall_MemberIntegralRecord (MemberId,UserName,TypeId,Integral,RecordDate,ReMark,SupplierId) values(@MemberId,@UserName,@TypeId,@Integral,@RecordDate,@ReMark,@SupplierId)",
            //    param: new {
            //        MemberId=entity.MemberId,
            //        UserName=entity.UserName,
            //        TypeId=entity.TypeId,
            //        Integral=entity.Integral,
            //        RecordDate=entity.RecordDate,
            //        ReMark=entity.ReMark,
            //        SupplierId=entity.SupplierId }
            //    );
            context.Connection.Insert<MemberIntegralRecord>(entity);
        }

        public override void Update(MemberIntegralRecord entity)
        {
            throw new NotImplementedException();
        }
    }

}
