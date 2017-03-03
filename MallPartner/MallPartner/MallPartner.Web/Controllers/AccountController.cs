using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StMallB.Web.Models;
using System.Web.Security;
using System;
using StMallB.Service;

namespace StMallB.Web.Controllers
{
    public class AccountController : Controller
    {
        private IJoinBusinessService joinBusinessService;

        public AccountController(IJoinBusinessService joinBusinessService)
        {
            this.joinBusinessService = joinBusinessService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            FormsAuthentication.SignOut();

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {

                if (joinBusinessService.Login(model.BusAccount, model.BusPassword))
                {
                    //创造票据
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(model.BusAccount, false, 360);
                    //加密票据
                    string ticString = FormsAuthentication.Encrypt(ticket);
                    //输出到客户端
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, ticString));
                    //跳转到登录前页面
                    string redirectTo = Request.QueryString["ReturnUrl"];
                    if (string.IsNullOrEmpty(redirectTo))
                    {
                        redirectTo = "/";
                    }

                    return Json(new
                    {
                        msg = "successful",
                        state = 1
                    });
                }
                else
                {
                    return Json(new
                    {
                        msg = "账号或密码不正确",
                        state = -1
                    });
                }
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    if (_userManager != null)
            //    {
            //        _userManager.Dispose();
            //        _userManager = null;
            //    }

            //    if (_signInManager != null)
            //    {
            //        _signInManager.Dispose();
            //        _signInManager = null;
            //    }
            //}

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}