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
                name: "Favourities",
                url: "Ulubione",
                defaults: new { controller = "Manage", action = "Favourities" }
                ); 

                routes.MapRoute(
                name: "AdminPromotionDetails",
                url: "Admin/promocje-szczegoly/{id}",
                defaults: new { controller = "Admin", action = "PromotionDetails" }
                );

            routes.MapRoute(
                name: "AdminSearchPromotion",
                url: "Admin/promocje-szukaj",
                defaults: new { controller = "Admin", action = "SearchPromotion" }
                );

            routes.MapRoute(
                name: "AdminAddPromotion",
                url: "Admin/promocje-dodaj",
                defaults: new { controller = "Admin", action = "AddPromotion" }
                );

            routes.MapRoute(
                name: "AdminSearch",
                url: "Admin/szukaj",
                defaults: new { controller = "Admin", action = "Search" }
                );

            routes.MapRoute(
                name: "AdminProductDetails",
                url: "Admin/produkt-szczegoly/{id}",
                defaults: new { controller = "Admin", action = "ProductDetails" }
                );

            routes.MapRoute(
                name: "AdminAddProduct",
                url: "Admin/produkt-dodaj/",
                defaults: new { controller = "Admin", action = "AddProduct" }
                );

            routes.MapRoute(
                name: "AdminOrderDetails",
                url: "Admin/zamowienia/szczegoly/{id}",
                defaults: new { controller = "Admin", action = "OrderDetails" }
                );

            routes.MapRoute(
                name: "AdminOrderList",
                url: "Admin/zamowienia",
                defaults: new { controller = "Admin", action = "OrdersList" }
                );

            routes.MapRoute(
                name: "Admin",
                url: "Admin",
                defaults: new { controller = "Admin", action = "Index" }
                );

            routes.MapRoute(
                name: "ManageOrderDetails",
                url: "konto/zamowienia/{id}",
                defaults: new { controller = "Manage", action = "OrderDetails" }
                );

            routes.MapRoute(
                name: "ManageOrdersList",
                url: "konto/zamowienia",
                defaults: new { controller = "Manage", action = "OrdersList" }
                );

            routes.MapRoute(
                name: "ManageAccountSettings",
                url: "konto/ustawienia",
                defaults: new { controller = "Manage", action = "AccountSettings" }
                );

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
                defaults: new { controller = "Home", action = "Search" }
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
