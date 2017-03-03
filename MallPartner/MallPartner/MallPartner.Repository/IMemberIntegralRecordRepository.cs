using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StMallB.Entity;
namespace StMallB.Repository
{
    public interface IMemberIntegralRecordRepository:IRepository<MemberIntegralRecord>
    {
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="entity"></param>
        void Insert(MemberIntegralRecord entity);

        IList<MemberIntegralRecord> GetAll(int pageNum, int pageSize, string keyword, long busId, out long totalCount);
    }
}
