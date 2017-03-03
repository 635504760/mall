using StMallB.Service;
using System.Web.Mvc;

namespace StMallB.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IJoinBusinessService joinBusinessService)
            : base(joinBusinessService)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}