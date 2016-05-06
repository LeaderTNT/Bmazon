using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bmazon.Web.Startup))]
namespace Bmazon.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
