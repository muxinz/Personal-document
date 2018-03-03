using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(simjxweb.Startup))]
namespace simjxweb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
