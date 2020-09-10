using B_KOM_Sklep_internetowy.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B_KOM_Sklep_internetowy.Controllers
{
    public class CommonController : Controller
    {
        InternetShopContext db = new InternetShopContext();

        [ChildActionOnly]
        public ActionResult Menu()
        {
            //var mainCategories = db.MainCategories.ToList();
            ViewData["mainCategories"] = db.MainCategories.ToList();
            return PartialView("_Menu");
        }
    }
}