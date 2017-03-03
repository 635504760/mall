using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Repository
{
    public class CustomClassMapper<T> : AutoClassMapper<T> where T : class
    {
        public override void Table(string tableName)
        {
            tableName = "StMall_" + tableName;

            base.Table(tableName);
        }
    }
}
