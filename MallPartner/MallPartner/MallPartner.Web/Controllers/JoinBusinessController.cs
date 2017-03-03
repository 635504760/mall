using MallPartner.DataObject;
using StMallB.Entity;
using StMallB.Service;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StMallB.Web.Controllers
{
    [RoutePrefix("JoinBusiness")]
    public class JoinBusinessController : BaseController
    {
        private IJoinBusinessService joinBusinessService;
        private IJoinBusinessRechargeService JoinBusinessRechargeService;

        public JoinBusinessController(
            IJoinBusinessService joinBusinessService,
            IJoinBusinessRechargeService JoinBusinessRechargeService)
            : base(joinBusinessService)
        {
            this.joinBusinessService = joinBusinessService;
            this.JoinBusinessRechargeService = JoinBusinessRechargeService;
        }

        #region actions

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 开通
        /// </summary>
        /// <returns></returns>
        public ActionResult AddSubJoinBusiness(int isModal = 0)
        {
            ViewBag.isModal = isModal;
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        [Route("SubJoinBusinessRechargeList"), Route("SubJoinBusinessRechargeList/{keyword}")]
        public ActionResult SubJoinBusinessRechargeList(string keyword)
        {
            if (keyword == null)
            {
                keyword = "";
            }

            ViewBag.keyword = keyword;

            return View();
        }

        [Route("Recharge/{account}"), Route("Recharge")]
        public ActionResult Recharge(int isModal = 0, string account = null)
        {
            ViewBag.isModal = isModal;

            if (!string.IsNullOrEmpty(account))
            {
                ViewBag.SubJoinBusiness = joinBusinessService.GetJoinBusinessByAccount(account);
            }

            ViewBag.CurrentJoinBusiness = joinBusinessService.GetJoinBusinessByAccount(HttpContext.User.Identity.Name);

            return View();
        }

        /// <summary>
        /// 重置下级代理商密码
        /// </summary>
        /// <returns></returns>
        [Route("ResetSubPassword/{account}")]
        public ActionResult ResetSubPassword(string account)
        {
            ViewBag.Account = account;
            return View();
        }

        #endregion

        #region APIs

        [HttpGet, Route("List/{pageNum}/{pageSize}")]
        public JsonResult List(int pageNum, int pageSize, string keyword)
        {
            return Json(joinBusinessService.GetSubJoinBusinesses(pageNum, pageSize, keyword), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 下级代理商充值积分
        /// </summary>
        /// <param name="integral"></param>
        /// <param name="busAccount"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RechargeIntegral(decimal? integral, string busAccount)
        {
            try
            {
                if (integral == null || integral <= 0)
                {
                    return Json(new
                    {
                        msg = "充值积分必须为正数哦",
                        state = -1
                    });
                }

                joinBusinessService.RechargeIntegral((decimal)integral, busAccount);

                return Json(new
                {
                    msg = "successful",
                    state = 1
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    msg = ex.Message,
                    state = -1
                });
            }

        }

        /// <summary>
        /// 新增下级代理商
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(CreateJoinBusinessDto dto)
        {
            try
            {
                joinBusinessService.CreateJoinBusiness(dto);
                return Json(new
                {
                    msg = "successful",
                    state = 1
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    msg = ex.Message,
                    state = -1
                });
            }
        }

        /// <summary>
        /// 重置下级代理商密码
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("ResetSubPassword/{account}")]
        public JsonResult ResetSubPasswordPost(string account)
        {
            try
            {
                joinBusinessService.ResetSubPasswordPost(account);

                return Json(new
                {
                    msg = "successful",
                    state = 1
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    msg = ex.Message,
                    state = -1
                });
            }
        }

        [HttpGet, Route("SubJoinBusinessRechargeList/{pageNum}/{pageSize}")]
        public JsonResult SubJoinBusinessRechargeList(int pageNum, int pageSize, string keyword)
        {
            return Json(JoinBusinessRechargeService.FindSubJoinBusinessRecharge(pageNum, pageSize, keyword), JsonRequestBehavior.AllowGet);
        }

        [Route("SubJoinBusinesses")]
        public JsonResult SubJoinBusinesses()
        {
            return Json(joinBusinessService.GetSubJoinBusinesses(), JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}