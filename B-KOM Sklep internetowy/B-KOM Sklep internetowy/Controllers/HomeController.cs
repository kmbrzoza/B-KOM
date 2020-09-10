using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.DTO;
using B_KOM_Sklep_internetowy.Models;
using B_KOM_Sklep_internetowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace B_KOM_Sklep_internetowy.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        InternetShopContext db = new InternetShopContext();
        // GET: Home
        public ActionResult Index()
        {
            var recommended = db.Products.Where(p => !p.Hidden && p.Recommended).OrderBy(p => Guid.NewGuid()).Take(8).ToList();
            var bestsellers = db.Products.Where(p => !p.Hidden && p.Bestseller).OrderBy(p => Guid.NewGuid()).Take(5).ToList();

            var recommendedExtModel = new List<ProductDTO>();
            var bestsellersExtModel = new List<ProductDTO>();

            foreach (var rec in recommended)
                recommendedExtModel.Add(new ProductDTO() { Product = rec });

            foreach (var bests in bestsellers)
                bestsellersExtModel.Add(new ProductDTO() { Product = bests });

            var homeVM = new HomeViewModel()
            {
                Recommended = recommendedExtModel,
                Bestsellers = bestsellersExtModel
            };

            return View(homeVM);
        }

        // GET: /{FooterPage} (Contacts, delivery etc.)
        public ActionResult FooterPage(string footerPageName)
        {
            return View(footerPageName);
        }
    }
}