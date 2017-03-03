using StMallB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Configuration;

namespace StMallB.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private IJoinBusinessService joinBusinessService;

        public BaseController(IJoinBusinessService joinBusinessService)
        {
            this.joinBusinessService = joinBusinessService;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //var memberAccount = string.Empty;
            ////var current = System.Web.HttpContext.Current;
            ////var cookie = requestContext.HttpContext.Request.Cookies[GlobalConfig.CookieDomain];
            ////if (current.User != null && current.User.Identity.IsAuthenticated &&
            ////    !string.IsNullOrEmpty(current.User.Identity.Name))
            ////{
            ////    memberAccount = current.User.Identity.Name.Replace("'", "");
            ////}
            //var identityName = Commond.GetSession();
            //if (!string.IsNullOrEmpty(identityName))
            //{
            //    memberAccount = identityName;
            //}
            //if (string.IsNullOrEmpty(memberAccount))
            //{
            //    //if (!(requestContext.HttpContext.Request.Url != null && requestContext.HttpContext.Request.Url.ToString().ToLower().IndexOf("/login", System.StringComparison.Ordinal) > -1))
            //    if (requestContext.HttpContext.Request.Url != null)
            //    {
            //        requestContext.HttpContext.Response.Write(
            //            "<script>if(window==window.top) {window.location.href='" + GlobalConfig.Domain +
            //            "Login/';} else {window.top.location.href='" + GlobalConfig.Domain +
            //            "Login/';}</script>");
            //    }
            //}
            //else
            //    ViewBag.saUserLoginName = memberAccount;


            string currentAccount = requestContext.HttpContext.User.Identity.Name;

            if (!string.IsNullOrEmpty(currentAccount))
            {
                ViewBag.CurrentJoinBusiness = joinBusinessService.GetJoinBusinessByAccount(currentAccount);
            }


            base.Initialize(requestContext);
        }
    }
}