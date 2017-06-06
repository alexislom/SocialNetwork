using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PL.Startup))]
namespace PL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
