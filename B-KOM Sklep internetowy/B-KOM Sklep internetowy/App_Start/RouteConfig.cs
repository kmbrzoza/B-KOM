using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace B_KOM_Sklep_internetowy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AddToCart",
                url: "koszyk/dodaj/{id}",
                defaults: new { controller = "Cart", action = "AddToCart" }
                );

            routes.MapRoute(
                name: "Cart",
                url: "koszyk/",
                defaults: new { controller = "Cart", action = "Index" }
                );

            routes.MapRoute(
                name: "Product",
                url: "produkt/{id}",
                defaults: new { controller = "Product", action = "Details" }
                );

            routes.MapRoute(
                name: "CategoryProducts",
                url: "produkt/kategorie/{categoryLinkName}",
                defaults: new { controller = "Product", action = "CategoryProducts" }
                );

            routes.MapRoute(
                name: "Search",
                url: "Szukaj",
                defaults: new { controller = "Home", action = "Search"}
            );

            routes.MapRoute(
                name: "FooterPages",
                url: "{footerPageName}",
                defaults: new { controller = "Home", action = "FooterPage" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
