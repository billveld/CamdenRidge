using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CamdenRidge.Startup))]
namespace CamdenRidge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
