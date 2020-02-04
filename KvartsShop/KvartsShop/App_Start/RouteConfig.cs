﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KvartsShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{otherId}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, otherId = UrlParameter.Optional },
                namespaces: new[] { "KvartsShop.Controllers" }
            );
        }
    }
}