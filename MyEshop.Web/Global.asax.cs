using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Threading;
using System.Data.Entity;


namespace MyEshop.Web
{

    using MyEshop.Ioc;
    using MyEshop.Utility;
    using MyEshop.DataLayer;

    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {

            //Delete Engins Aspx
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Config Ninject
            ConfigNinjectIoC();
        }


        // متدی برای فعال سازی ماژول PersianCulture
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var persianCulture = new PersianCulture();
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;
        }


        private void ConfigNinjectIoC()
        {
            ControllerBuilder.Current.SetControllerFactory(new NinjectController());
        }
    }
}