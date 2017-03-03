using StMallB.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Entity
{
    public class MemberIntegralRecord
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public string UserName { get; set; }
        public IntegralType TypeId { get; set; }
        public decimal Integral { get; set; }
        public DateTime RecordDate { get; set; }
        public string ReMark { get; set; }
        public long BusId { get; set; }
    }
}
