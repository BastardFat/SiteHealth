using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SiteHealth.Web.Startup))]

namespace SiteHealth.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("DefaultConnection");

            app.UseHangfireDashboard();

            // Use this if you can set your IIS in Always Runing mode
            // app.UseHangfireServer();
        }
    }
}
