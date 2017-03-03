using StMallB.Repository.DbContextFactory;
using StMallB.Repository.DbContextFactory.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Repository.UnitOfWork.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextFactory DbContextFactory { get; set; }

        private DapperContext _context;

        public DapperContext Context
        {
            get
            {
                if (this._context != null)
                {
                    return this._context;
                }
                this._context = DbContextFactory.GetDbContext();
                return this._context;
            }
        }

        public UnitOfWork(IDbContextFactory factory)
        {
            this.DbContextFactory = factory;
        }

        public void SaveChange()
        {

            // commits transation
            _context.Commit();
        }

        public void RollBack()
        {

            // commits transation
            _context.Rollback();
        }

        private bool disposed = false;



        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                    this._context = null;
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}