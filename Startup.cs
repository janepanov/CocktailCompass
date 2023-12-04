using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CocktailCompass.Startup))]
namespace CocktailCompass
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
