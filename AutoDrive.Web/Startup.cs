using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoDrive.Web.Startup))]
namespace AutoDrive.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
