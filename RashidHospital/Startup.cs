using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RashidHospital.Startup))]
namespace RashidHospital
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
