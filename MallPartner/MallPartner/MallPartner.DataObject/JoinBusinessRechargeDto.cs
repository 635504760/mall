using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallPartner.DataObject
{
    public class JoinBusinessRechargeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public decimal RechargeIntegral { get; set; }
        public DateTime AddTime { get; set; }
        public string Remark { get; set; }
    }
}
