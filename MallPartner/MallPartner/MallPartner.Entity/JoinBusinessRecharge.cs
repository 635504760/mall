using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Entity
{
    public class JoinBusinessRecharge
    {
        public long Id { get; set; }
        public long BusId { get; set; }
        public decimal RechargeIntegral { get; set; }
        public DateTime AddTime { get; set; }
        public string Remark { get; set; }
    }
}
