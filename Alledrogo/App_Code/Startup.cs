using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Alledrogo.Startup))]
namespace Alledrogo
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
