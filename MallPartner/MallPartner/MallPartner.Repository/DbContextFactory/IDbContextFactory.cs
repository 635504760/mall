using StMallB.Repository.DbContextFactory.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Repository.DbContextFactory
{
    public interface IDbContextFactory
    {
        DapperContext GetDbContext();
    }
}
