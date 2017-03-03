using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using MallPartner.DataObject;
using StMallB.Entity;
using StMallB.Repository;

namespace StMallB.Service.Impl
{
    public class ChangePswServiceImpl : IChangePswService
    {
        private IChangePswRepository changePswRepository;
        private IJoinBusinessRepository joinBusinessRepository;
        public ChangePswServiceImpl(IChangePswRepository changePswRepository, IJoinBusinessRepository joinBusinessRepository)
        {
            this.changePswRepository = changePswRepository;
            this.joinBusinessRepository = joinBusinessRepository;
        }
        public void ChangePsaaword(string oldPsw, string newPsw)
        {
            if (string.IsNullOrEmpty(oldPsw) || string.IsNullOrEmpty(newPsw))
            {
                throw new ArgumentException("请输入密码");
            }
            if (newPsw.Length < 6)
            {
                throw new ArgumentException("密码必须至少6位");
            }

            string currentAccount = HttpContext.Current.User.Identity.Name;
            JoinBusiness JoinBusiness = joinBusinessRepository.GetJoinBusinessByBusAccount(currentAccount);
            string md5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(oldPsw, "MD5");
            string newmd5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(newPsw, "MD5");

            if (!JoinBusiness.BusPassword.Equals(md5Password, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ArgumentException("原始密码错误");
            }
            changePswRepository.ChangePsaaword(currentAccount, oldPsw, newmd5Password);
        }

        public Page<JoinBusinessRecharge> GetList(int pageNum, int pageSize, string busName)
        {
            busName = HttpContext.Current.User.Identity.Name;
            JoinBusiness joinBusiness = joinBusinessRepository.GetJoinBusinessByBusAccount(busName);
            long busId = joinBusiness.Id;
            if (string.IsNullOrEmpty(busName))
            {
                throw new ArgumentNullException("busName");
            }
            if (pageNum <= 0)
            {
                pageNum = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            long totalCount = 0;
            var list = changePswRepository.Find(pageNum, pageSize, busId, out totalCount);

            return new Page<JoinBusinessRecharge>(list, pageNum, pageSize, totalCount);
        }

    }
}
