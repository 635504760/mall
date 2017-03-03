using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MallPartner.DataObject;
using StMallB.Entity;

namespace StMallB.Service
{
    public interface IChangePswService
    {
        void ChangePsaaword(string oldPsw, string newPsw);
        Page<JoinBusinessRecharge> GetList(int pageNum, int pageSize, string busName);
    }
}
