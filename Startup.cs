using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodStore.Startup))]
namespace FoodStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
