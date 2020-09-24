using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.DTO;
using B_KOM_Sklep_internetowy.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B_KOM_Sklep_internetowy.Controllers
{
    public class ProductController : Controller
    {
        public ProductController()
        {

        }

        InternetShopContext db = new InternetShopContext();


        // GET: produkt/kategorie/{categoryLinkName}
        public ActionResult CategoryProducts(string categoryLinkName)
        {
            var category = db.Categories.Include("Products").Where(c => c.LinkName.ToLower() == categoryLinkName).Single();
            ViewBag.categoryName = category.Name;
            ViewBag.mainCategoryName = category.MainCategory.Name;

            var products = category.Products.ToList();

            var productsDTO = new List<ProductDTO>();

            foreach (var prod in products)
            {
                productsDTO.Add(new ProductDTO() { Product = prod });
            }
            
            return View(productsDTO);
        }

        // GET: produkt/{id}
        public ActionResult Details(int id)
        {
            bool contains = false;
            if(Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                contains = db.Favourites.Where(c => c.UserId == userId && c.ProductId == id).Any();
            }

            var product = db.Products.Find(id);
            var productDTO = new ProductDTO() { Product = product, FavouriteForUser = contains };
            return View(productDTO);
        }

        [HttpPost]
        public ActionResult AddOpinion(Opinion opinion)
        {
            if(ModelState.IsValid)
            {
                opinion.DateTime = DateTime.Now;

                opinion.Accepted = true; //change in the future
                db.Opinions.Add(opinion);
                db.SaveChanges();
                TempData["AddOpinion"] = true;
                return RedirectToAction("Details", new { id = opinion.ProductId });
            }
            return RedirectToAction("Details", new { id = opinion.ProductId });
        }
    }
}