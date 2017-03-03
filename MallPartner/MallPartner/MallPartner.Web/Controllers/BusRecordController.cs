using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StMallB.Service;
using System.Web.Security;

namespace StMallB.Web.Controllers
{
    [RoutePrefix("BusRecord")]
    public class BusRecordController : BaseController
    {
        private IChangePswService changePswService;
        private IJoinBusinessRechargeService joinBusinessRechargeService;

        public BusRecordController(
            IJoinBusinessService joinBusinessService,
            IChangePswService changePswService,
            IJoinBusinessRechargeService joinBusinessRechargeService)
            : base(joinBusinessService)
        {
            this.changePswService = changePswService;
            this.joinBusinessRechargeService = joinBusinessRechargeService;
        }
        // GET: BusRecord
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Record()
        {
            return View();
        }

        public ActionResult ChangePsw()
        {
            return View();
        }
        [HttpGet, Route("List/{pageNum}/{pageSize}")]
        public JsonResult List(int pageNum, int pageSize)
        {
            return Json(joinBusinessRechargeService.FindJoinBusinessRecharge(pageNum, pageSize), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangePsaaword(string oldPsw, string newPsw)
        {
            try
            {
                changePswService.ChangePsaaword(oldPsw, newPsw);

                FormsAuthentication.SignOut();

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
    }
}