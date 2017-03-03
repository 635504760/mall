using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StMallB.Service;

namespace StMallB.Web.Controllers
{
     [RoutePrefix("JoinBusinessRecharge")]
    public class BusRechargeRecordController : Controller
    {
        private IChangePswService iChangePswService;
        public BusRechargeRecordController(IChangePswService iChangePswService)
        {
            this.iChangePswService = iChangePswService;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet, Route("List/{pageNum}/{pageSize}")]
        public JsonResult List(int pageNum, int pageSize)
        {
            return Json(iChangePswService.GetList(pageNum, pageSize), JsonRequestBehavior.AllowGet);
        }
    }
}