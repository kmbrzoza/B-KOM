using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.Infrastructure;
using B_KOM_Sklep_internetowy.Models;
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
            ICacheProvider cache = new DefaultCacheProvider();
            List<MainCategory> mainCategoriesList;

            if(cache.IsSet(Consts.mainCategoriesLayoutCacheKey))
            {
                mainCategoriesList = cache.Get(Consts.mainCategoriesLayoutCacheKey) as List<MainCategory>;
            }
            else
            {
                mainCategoriesList = db.MainCategories.ToList();
                cache.Set(Consts.mainCategoriesLayoutCacheKey, mainCategoriesList, 1); //Just for 1 min
            }

            ViewData["mainCategories"] = mainCategoriesList;
            return PartialView("_Menu");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}