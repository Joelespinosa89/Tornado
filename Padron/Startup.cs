using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Padron.Startup))]
namespace Padron
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
