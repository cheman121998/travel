using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(travel.Startup))]
namespace travel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
