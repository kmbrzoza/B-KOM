using Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[assembly: OwinStartupAttribute(typeof(B_KOM_Sklep_internetowy.Startup))]

namespace B_KOM_Sklep_internetowy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalConfiguration.Configuration.UseSqlServerStorage("InternetShopContext");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}