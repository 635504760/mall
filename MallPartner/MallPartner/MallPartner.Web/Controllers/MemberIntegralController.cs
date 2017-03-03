using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StMallB.Entity;
using StMallB.Service;
namespace StMallB.Web.Controllers
{
    [RoutePrefix("MemberIntegral")]
    public class MemberIntegralController : BaseController
    {
        private IMemberIntegralService memberIntegralService;
        public MemberIntegralController(IJoinBusinessService joinBusinessService,
            IMemberIntegralService memberIntegralService)
            : base(joinBusinessService)
        {
            this.memberIntegralService = memberIntegralService;
        }

        //
        // GET: /MemberIntegral/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RecordList()
        {
            return View();
        }

        [HttpGet, Route("RecordList/{pageNum}/{pageSize}")]
        public ActionResult RecordList(int pageNum, int pageSize, string keyword, string userName)
        {
            return Json(memberIntegralService.ShowHistoryRecord(pageNum, pageSize, keyword, userName), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult RechargeIntegral(MemberIntegralRecord entity)
        {
            try
            {
                if (entity != null&&entity.UserName!=null&&entity.Integral>0)
                {

                    if (memberIntegralService.IsExist(entity.UserName))
                    {

                        memberIntegralService.RechargeIntegral(entity);
                        return Json(new
                        {
                            msg = "successful",
                            state = 1
                        });

                    }

                    return Json(new
                    {
                        msg = "不存在此用户",
                        state = -2
                    });
                }
                return Json(new
                    {
                        msg = "用户名，积分不能为空",
                        state = -3
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