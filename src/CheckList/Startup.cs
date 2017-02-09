using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CheckList.Startup))]
namespace CheckList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
