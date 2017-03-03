using StMallB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StMallB.Entity;
using System.Web;
using MallPartner.DataObject;
using StMallB.Entity.Enum;
namespace StMallB.Service.Impl
{
    public class MemberIntegralServiceImpl : IMemberIntegralService
    {
        private IMemberIntegralRepository memberIntegralRepository;
        private IMemberIntegralRecordRepository memberIntegralRecordRepository;
        private IJoinBusinessRepository joinBusinessRepository;

        public MemberIntegralServiceImpl(IMemberIntegralRepository memberIntegralRepository, IMemberIntegralRecordRepository memberIntegralRecordRepository, IJoinBusinessRepository joinBusinessRepository)
        {
            this.memberIntegralRepository = memberIntegralRepository;
            this.memberIntegralRecordRepository = memberIntegralRecordRepository;
            this.joinBusinessRepository = joinBusinessRepository;
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsExist(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("用户名不能为空");
            }
            MemberIntegral memberIntegral = memberIntegralRepository.GetMemberIntegralByUserName(userName);
            if (memberIntegral != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断是否是第一次充值
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsExistInMemberIntegral(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("用户名不能为空");
            }
            MemberIntegral memberIntegral = memberIntegralRepository.GetMemberIntegral(userName);
            if (memberIntegral != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 改变积分
        /// </summary>
        /// <param name="integral"></param>
        /// <param name="userName"></param>
        public void RechargeIntegral(MemberIntegralRecord entity)
        {
            string busAccount = HttpContext.Current.User.Identity.Name;
            if (entity == null)
            {
                throw new ArgumentException("用户名，积分不能为空");
            }
            if (entity.Integral <= 0)
            {
                throw new ArgumentException("积分不能为负值");
            }
            if (string.IsNullOrEmpty(entity.UserName))
            {
                throw new ArgumentNullException("用户名不能为空");
            }
            JoinBusiness joinBusiness = joinBusinessRepository.GetJoinBusinessByBusAccount(busAccount);
            if (joinBusiness.BusIntegral < entity.Integral)
            {
                throw new ArgumentException("当前积分余额不足");
            }
            MemberIntegral memberIntegral = memberIntegralRepository.GetMemberIntegralByUserName(entity.UserName);
            if (!IsExistInMemberIntegral(entity.UserName))
            {
                MemberIntegral newMemberIntegral = new MemberIntegral();
                newMemberIntegral.AvailableIntegrals = entity.Integral;
                newMemberIntegral.HistoryIntegrals = 0;
                newMemberIntegral.MemberId = memberIntegral.Id;
                newMemberIntegral.UserName = entity.UserName;               
                memberIntegralRepository.FirstIncreaseIntegral(newMemberIntegral);
            }
            else
            {
                memberIntegralRepository.IncreaseIntegral(entity.Integral, entity.UserName);
            }         
            MemberIntegralRecord memberIntegralRecord = new MemberIntegralRecord();
           
            memberIntegralRecord.MemberId = memberIntegral.Id;
            memberIntegralRecord.Integral = entity.Integral;
            memberIntegralRecord.RecordDate = DateTime.Now;
            memberIntegralRecord.ReMark = entity.ReMark;
            memberIntegralRecord.UserName = entity.UserName;
            memberIntegralRecord.BusId = joinBusiness.Id;
            memberIntegralRecord.TypeId = IntegralType.JoinBusinessGive;    
            joinBusinessRepository.IncreaseIntegral(-entity.Integral, busAccount);
            memberIntegralRecordRepository.Insert(memberIntegralRecord);
        }
        /// <summary>
        /// 显示积分充值历史记录
        /// </summary>
        /// <param name="userName"></param>
        public Page<MemberIntegralRecord> ShowHistoryRecord(int pageNum, int pageSize, string keyword, string userName)
        {
            userName = HttpContext.Current.User.Identity.Name;
            JoinBusiness  joinBusiness=joinBusinessRepository.GetJoinBusinessByBusAccount(userName);
            long busId=joinBusiness.Id;
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("用户名不能为空");
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
            var list = memberIntegralRecordRepository.GetAll(pageNum, pageSize, keyword, busId, out totalCount);
            return new Page<MemberIntegralRecord>(list, pageNum, pageSize, totalCount);
        }

    }
}
