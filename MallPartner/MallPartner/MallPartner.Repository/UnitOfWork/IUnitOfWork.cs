using StMallB.Repository.DbContextFactory;
using StMallB.Repository.DbContextFactory.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        DapperContext Context { get; }

        void SaveChange();
        void RollBack();
    }
}
