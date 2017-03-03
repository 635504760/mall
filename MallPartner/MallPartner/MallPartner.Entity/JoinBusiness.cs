using StMallB.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Entity
{
    [Table(Name = "StMall_JoinBusiness")]
    public class JoinBusiness
    {
        public long Id { get; set; }
        public string BusName { get; set; }
        public string BusAccount { get; set; }
        public string BusPassword { get; set; }
        public long BusRelation { get; set; }
        public int BusRegionId { get; set; }
        public string BusAddress { get; set; }
        public string BusLinkMan { get; set; }
        public string BusLinkTel { get; set; }
        public decimal BusIntegral { get; set; }
        public decimal BusIntegralLimit { get; set; }
        public JoinBusinessState BusState { get; set; }
        public string BusDes { get; set; }
        public DateTime BusAddTime { get; set; }
        public DateTime BusUpTime { get; set; }
    }
}
