using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tbPortfolio.Startup))]
namespace tbPortfolio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
