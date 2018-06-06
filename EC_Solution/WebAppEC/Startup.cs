using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppEC.Startup))]
namespace WebAppEC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
