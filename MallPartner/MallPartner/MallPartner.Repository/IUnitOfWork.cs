using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Repository
{
    public interface IUnitOfWork<T> : IDisposable
    {
        Repository<T> Repository { get; }

        void Commit();
    }
}
