using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Repository.DbContextFactory
{
    interface IContext : IDisposable
    {
        /// <summary>
        /// Indicates if transaction is started.
        /// </summary>
        bool IsTransactionStarted { get; }

        /// <summary>
        /// Begins transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits operations of transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks operations of transaction.
        /// </summary>
        void Rollback();
    }
}
