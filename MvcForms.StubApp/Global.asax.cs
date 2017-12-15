﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcForms.StubApp.Utility;

namespace MvcForms.StubApp
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalFilters.Filters.Add(new PjaxFilter());
            Styler.Set(new MultiStyler());
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
