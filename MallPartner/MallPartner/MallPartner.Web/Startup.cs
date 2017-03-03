using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StMallB.Web.Startup))]
namespace StMallB.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
