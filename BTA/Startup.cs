using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BTA.Startup))]
namespace BTA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
