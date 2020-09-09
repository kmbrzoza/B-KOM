using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B_KOM_Sklep_internetowy.Controllers
{
    public class ProductController : Controller
    {
        // GET: Produkty/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: produkt/kategorie{categoryLinkName}
        public ActionResult Categories(string categoryLinkName)
        {
            return View(categoryLinkName);
        }

        // GET: produkt/{id}
        public ActionResult Details(string id)
        {
            return View(id);
        }
    }
}