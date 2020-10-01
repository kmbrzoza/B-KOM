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
        public ActionResult CategoryProducts(string categoryLinkName, SortProductList? sortBy)
        {
            var category = db.Categories.Include("Products").Where(c => c.LinkName.ToLower() == categoryLinkName).Single();
            ViewBag.categoryName = category.Name;
            ViewBag.mainCategoryName = category.MainCategory.Name;

            var products = new List<Product>();

            if (sortBy == null || sortBy == SortProductList.Default)
                products = category.Products.ToList();
            else if (sortBy == SortProductList.ByPriceUp)
                products = category.Products.OrderBy(c => c.Price).ToList();
            else if (sortBy == SortProductList.ByPriceDown)
                products = category.Products.OrderByDescending(c => c.Price).ToList();
            else if (sortBy == SortProductList.ByNameUp)
                products = category.Products.OrderBy(c => c.Name).ToList();
            else if (sortBy == SortProductList.ByNameDown)
                products = category.Products.OrderByDescending(c => c.Name).ToList();
            else if(sortBy == SortProductList.Bests)
                products = category.Products.OrderByDescending(c => (c.Opinions.Sum(d => d.StarsValue) / (c.Opinions.Count() + 1))).ToList(); //cannot divide by 0


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

                
                db.Opinions.Add(opinion);
                db.SaveChanges();
                TempData["AddOpinion"] = true;
                return RedirectToAction("Details", new { id = opinion.ProductId });
            }
            return RedirectToAction("Details", new { id = opinion.ProductId });
        }
    }
}