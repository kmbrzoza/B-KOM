using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.Models;
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
        InternetShopContext db = new InternetShopContext();
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult FooterPage(string footerPageName)
        {
            return View(footerPageName);
        }
    }
}