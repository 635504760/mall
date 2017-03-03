using MallPartner.DataObject;
using StMallB.Entity;
using StMallB.Entity.Enum;
using StMallB.Repository;
using StMallB.Repository.UnitOfWork;
using StMallB.Repository.UnitOfWork.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using StMallB.Common.Util;
using StMallB.Repository.DbContextFactory;
using StMallB.Repository.DbContextFactory.Impl;

namespace StMallB.Service.Impl
{
    public class JoinBusinessServiceImpl : IJoinBusinessService
    {
        private IUnitOfWork unitOfWork;
        private IJoinBusinessRepository joinBusinessRepository;
        private IJoinBusinessRechargeRepository joinBusinessRechargeRepository;

        public JoinBusinessServiceImpl(
            IUnitOfWork unitOfWork,
            IJoinBusinessRepository joinBusinessRepository,
            IJoinBusinessRechargeRepository joinBusinessRechargeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.joinBusinessRepository = joinBusinessRepository;
            this.joinBusinessRechargeRepository = joinBusinessRechargeRepository;
        }

        /// <summary>
        /// 合作商登录
        /// </summary>
        /// <param name="busAccount">账号</param>
        /// <param name="busPassword">密码</param>
        /// <returns>是否成功</returns>
        public bool Login(string busAccount, string busPassword)
        {
            if (string.IsNullOrEmpty(busAccount))
            {
                throw new ArgumentNullException("用户名不能为空");
            }
            if (string.IsNullOrEmpty(busPassword))
            {
                throw new ArgumentNullException("密码不能为空");
            }

            string md5Password = FormsAuthentication.HashPasswordForStoringInConfigFile(busPassword, "MD5");

            JoinBusiness joinBusiness = joinBusinessRepository.GetJoinBusinessByBusAccount(busAccount);

            if (joinBusiness != null && joinBusiness.BusPassword.Equals(md5Password, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 充值积分
        /// </summary>
        /// <param name="integral">积分</param>
        /// <param name="busAccount">合作商登录帐号</param>
        public void RechargeIntegral(decimal integral, string busAccount)
        {
            if (integral <= 0)
            {
                throw new ArgumentException("充值积分必须大于0");
            }
            if (string.IsNullOrEmpty(busAccount))
            {
                throw new ArgumentNullException("充值账号不能为空");
            }

            string currentAccount = HttpContext.Current.User.Identity.Name;
            JoinBusiness currentJoinBusiness = joinBusinessRepository.GetJoinBusinessByBusAccount(currentAccount);

            JoinBusiness subJoinBusiness = joinBusinessRepository.GetJoinBusinessByBusAccount(busAccount.Trim());

            if (currentJoinBusiness == null)
            {
                throw new ArgumentException("当前合作商不存在");
            }
            if (subJoinBusiness == null)
            {
                throw new ArgumentException("下级代理商不存在");
            }
            if (subJoinBusiness.BusRelation != currentJoinBusiness.Id)
            {
                throw new ArgumentException("代理商不是当前合作商下级");
            }
            if (currentJoinBusiness.BusIntegral < integral)
            {
                throw new ArgumentException("当前积分余额不足");
            }

            try
            {
                unitOfWork.Context.BeginTransaction();

                joinBusinessRepository.IncreaseIntegral(integral, subJoinBusiness.BusAccount);
                joinBusinessRepository.IncreaseIntegral(-integral, currentJoinBusiness.BusAccount);
                joinBusinessRechargeRepository.Insert(new JoinBusinessRecharge()
                {
                    BusId = subJoinBusiness.Id,
                    RechargeIntegral = integral,
                    AddTime = DateTime.Now
                });

                unitOfWork.SaveChange();
            }
            catch (Exception)
            {
                unitOfWork.RollBack();
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }

        /// <summary>
        /// 开通下级代理商
        /// </summary>
        /// <param name="dto"></param>
        public void CreateJoinBusiness(CreateJoinBusinessDto dto)
        {
            if (dto.Account.HasChinese())
            {
                throw new ArgumentException("账户不能有中文");
            }
            if (dto.Password.HasChinese())
            {
                throw new ArgumentException("密码不能有中文");
            }

            if (joinBusinessRepository.GetJoinBusinessByBusAccount(dto.Account) != null)
            {
                throw new ArgumentException("账户已存在");
            }
            if (joinBusinessRepository.GetJoinBusinessByBusName(dto.Name) != null)
            {
                throw new ArgumentException("代理商公司名称已存在");
            }

            string currentAccount = HttpContext.Current.User.Identity.Name;
            JoinBusiness currentJoinBusiness = joinBusinessRepository.GetJoinBusinessByBusAccount(currentAccount);

            if (currentJoinBusiness == null)
            {
                throw new ArgumentException("当前合作商不存在或请重新登录");
            }
            if (currentJoinBusiness.BusIntegral < dto.Integral)
            {
                throw new ArgumentException("积分余额不足");
            }

            JoinBusiness joinBusiness = new JoinBusiness();
            joinBusiness.BusName = dto.Name.Trim();
            joinBusiness.BusAccount = dto.Account.Trim();
            joinBusiness.BusPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(dto.Password, "MD5");
            joinBusiness.BusRelation = currentJoinBusiness.Id;
            joinBusiness.BusRegionId = dto.RegionId;
            joinBusiness.BusAddress = dto.Address;
            joinBusiness.BusLinkMan = dto.LinkMan.Trim();
            joinBusiness.BusLinkTel = dto.LinkTel;
            joinBusiness.BusIntegral = dto.Integral;
            joinBusiness.BusIntegralLimit = dto.IntegralLimit;
            joinBusiness.BusState = JoinBusinessState.Enabled;
            joinBusiness.BusDes = dto.Des;
            joinBusiness.BusAddTime = DateTime.Now;
            joinBusiness.BusUpTime = DateTime.Now;

            joinBusinessRepository.Insert(joinBusiness);
        }

        public Page<JoinBusiness> GetList(int pageNum, int pageSize, string keyword)
        {
            if (pageNum <= 0)
            {
                pageNum = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            long totalCount = 0;
            var list = joinBusinessRepository.Find(pageNum, pageSize, keyword.Trim(), out totalCount);

            return new Page<JoinBusiness>(list, pageNum, pageSize, totalCount);
        }


        public Page<JoinBusiness> GetSubJoinBusinesses(int pageNum, int pageSize, string keyword)
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
            var list = joinBusinessRepository.FindByRelation(pageNum, pageSize, currentJoinBusiness.Id, keyword.Trim(), out totalCount);

            return new Page<JoinBusiness>(list, pageNum, pageSize, totalCount);
        }


        public void ResetSubPasswordPost(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                throw new ArgumentException("下级代理商账户为空");
            }

            string currentAccount = HttpContext.Current.User.Identity.Name;
            JoinBusiness currentJoinBusiness = joinBusinessRepository.GetJoinBusinessByBusAccount(currentAccount);

            JoinBusiness subJoinBusiness = joinBusinessRepository.GetJoinBusinessByBusAccount(account.Trim());

            if (currentJoinBusiness == null)
            {
                throw new ArgumentException("当前合作商不存在");
            }
            if (subJoinBusiness == null)
            {
                throw new ArgumentException("下级代理商不存在");
            }
            if (subJoinBusiness.BusRelation != currentJoinBusiness.Id)
            {
                throw new ArgumentException("代理商不是当前合作商下级");
            }

            subJoinBusiness.BusPassword = FormsAuthentication.HashPasswordForStoringInConfigFile("123456", "MD5");
            subJoinBusiness.BusUpTime = DateTime.Now;

            joinBusinessRepository.Update(subJoinBusiness);
        }


        public JoinBusiness GetJoinBusinessByAccount(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                return null;
            }

            return joinBusinessRepository.GetJoinBusinessByBusAccount(account.Trim());
        }


        public IList<JoinBusiness> GetSubJoinBusinesses()
        {
            string currentAccount = HttpContext.Current.User.Identity.Name;
            JoinBusiness currentJoinBusiness = joinBusinessRepository.GetJoinBusinessByBusAccount(currentAccount);
            if (currentJoinBusiness == null)
            {
                throw new ArgumentException("当前合作商不存在");
            }

            return joinBusinessRepository.FindByRelation(currentJoinBusiness.Id);
        }
    }
}
