using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bit_Mascot.Startup))]
namespace Bit_Mascot
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
