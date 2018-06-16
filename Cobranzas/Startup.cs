using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cobranzas.Startup))]
namespace Cobranzas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
