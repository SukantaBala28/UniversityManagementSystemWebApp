using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UCRMS_Version2.Startup))]
namespace UCRMS_Version2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
