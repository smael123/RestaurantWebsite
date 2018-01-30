using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestaurantWebsite.Startup))]
namespace RestaurantWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
