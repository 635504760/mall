using MallPartner.DataObject;
using StMallB.Entity;
using StMallB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StMallB.Service.Impl
{
    public class JoinBusinessRechargeServiceImpl : IJoinBusinessRechargeService
    {
        private IJoinBusinessRechargeRepository joinBusinessRechargeRepository;
        private IJoinBusinessRepository joinBusinessRepository;

        public JoinBusinessRechargeServiceImpl(
            IJoinBusinessRechargeRepository joinBusinessRechargeRepository,
            IJoinBusinessRepository joinBusinessRepository)
        {
            this.joinBusinessRechargeRepository = joinBusinessRechargeRepository;
            this.joinBusinessRepository = joinBusinessRepository;
        }


        public Page<JoinBusinessRechargeDto> FindSubJoinBusinessRecharge(int pageNum, int pageSize, string keyword)
        {
            if (pageNum <= 0)
            {
                pageNum = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            string currentAccount = HttpContext.Current.User.Identity.Name;
            JoinBusiness currentJoinBusiness = joinBusinessRepository.GetJoinBusinessByBusAccount(currentAccount);
            if (currentJoinBusiness == null)
            {
                throw new ArgumentException("当前合作商不存在");
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
            }

            long totalCount = 0;
            var list = joinBusinessRechargeRepository.FindByParentAccount(pageNum, pageSize, keyword, currentJoinBusiness.BusAccount, out totalCount);

            var returnList = new List<JoinBusinessRechargeDto>();
            foreach (var item in list)
            {
                returnList.Add(new JoinBusinessRechargeDto()
                {
                    Id = item.Item1.Id,
                    Name = item.Item2.BusName,
                    Account = item.Item2.BusAccount,
                    RechargeIntegral = item.Item1.RechargeIntegral,
                    AddTime = item.Item1.AddTime,
                    Remark = item.Item1.Remark
                });
            }
            return new Page<JoinBusinessRechargeDto>(returnList, pageNum, pageSize, totalCount);
        }

        public Page<JoinBusinessRechargeDto> FindJoinBusinessRecharge(int pageNum, int pageSize)
        {
            if (pageNum <= 0)
            {
                pageNum = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            string currentAccount = HttpContext.Current.User.Identity.Name;
            JoinBusiness currentJoinBusiness = joinBusinessRepository.GetJoinBusinessByBusAccount(currentAccount);
            if (currentJoinBusiness == null)
            {
                throw new ArgumentException("当前合作商不存在");
            }

            long totalCount = 0;
            var list = joinBusinessRechargeRepository.FindByBusId(pageNum, pageSize, currentJoinBusiness.Id, out totalCount);

            var returnList = new List<JoinBusinessRechargeDto>();
            foreach (var item in list)
            {
                returnList.Add(new JoinBusinessRechargeDto()
                {
                    Id = item.Item1.Id,
                    Name = item.Item2.BusName,
                    Account = item.Item2.BusAccount,
                    RechargeIntegral = item.Item1.RechargeIntegral,
                    AddTime = item.Item1.AddTime,
                    Remark = item.Item1.Remark
                });
            }
            return new Page<JoinBusinessRechargeDto>(returnList, pageNum, pageSize, totalCount);
        }
    }
}
