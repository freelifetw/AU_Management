using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AU_Management.Startup))]
namespace AU_Management
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
