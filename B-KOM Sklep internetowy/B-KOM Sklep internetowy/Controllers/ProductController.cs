﻿using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.DTO;
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
        public ActionResult Categories(string categoryLinkName)
        {
            var category = db.Categories.Include("Products").Where(c => c.LinkName.ToLower() == categoryLinkName).Single();
            ViewBag.categoryName = category.Name;
            var products = category.Products.ToList();

            var productsDTO = new List<ProductDTO>();

            foreach (var prod in products)
            {
                productsDTO.Add(new ProductDTO() { Product = prod });
            }

            return View(productsDTO);
        }

        // GET: produkt/{id}
        public ActionResult Details(string id)
        {
            return View(id);
        }
    }
}