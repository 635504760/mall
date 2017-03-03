using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Entity
{
    public class MemberIntegral
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public string UserName { get; set; }
        public decimal HistoryIntegrals { get; set; }
        public decimal AvailableIntegrals { get; set; }
    }
}
