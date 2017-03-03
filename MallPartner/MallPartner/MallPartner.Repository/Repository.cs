using StMallB.Repository.DbContextFactory.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;

namespace StMallB.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public readonly UnitOfWork.Impl.UnitOfWork unitOfwork;
        public readonly DapperContext context;
        public Repository(UnitOfWork.Impl.UnitOfWork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
            context = unitOfwork.Context;
        }

        public virtual IList<T> GetAll()
        {
            return context.Connection.GetList<T>(transaction: context.GetTransaction(), commandTimeout: context.GetCommandTimeout()).ToList();
        }

        public virtual void Insert(T entity)
        {
            context.Connection.Insert<T>(entity, context.GetTransaction(), context.GetCommandTimeout());
        }

        public virtual void Update(T entity)
        {
            context.Connection.Update<T>(entity, context.GetTransaction(), context.GetCommandTimeout());
        }

        public IList<T> Find(object predicate, IList<ISort> sort, int pageNum, int pageSize, out long totalCount)
        {
            var models = context.Connection.GetPage<T>(predicate, sort, pageNum - 1, pageSize, transaction: context.GetTransaction(), commandTimeout: context.GetCommandTimeout());
            totalCount = context.Connection.Count<T>(predicate, transaction: context.GetTransaction(), commandTimeout: context.GetCommandTimeout());

            return models.ToList();
        }
    }
}
