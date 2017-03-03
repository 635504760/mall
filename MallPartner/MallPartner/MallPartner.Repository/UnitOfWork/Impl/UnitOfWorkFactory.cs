using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StMallB.Repository.UnitOfWork.Impl
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private IUnitOfWork _unitOfWork;

        public UnitOfWorkFactory(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IUnitOfWork Create()
        {
            if (this._unitOfWork != null)
            {
                return this._unitOfWork;
            }
            //this._unitOfWork = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            return this._unitOfWork;
        }
    }
}
