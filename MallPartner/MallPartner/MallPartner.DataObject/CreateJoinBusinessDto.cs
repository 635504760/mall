using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallPartner.DataObject
{
    public class CreateJoinBusinessDto
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public int RegionId { get; set; }
        public string Address { get; set; }
        public string LinkMan { get; set; }
        public string LinkTel { get; set; }
        public decimal Integral { get; set; }
        public decimal IntegralLimit { get; set; }
        public string Des { get; set; }
    }
}
