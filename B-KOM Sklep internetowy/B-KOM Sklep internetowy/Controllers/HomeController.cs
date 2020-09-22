﻿using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.DTO;
using B_KOM_Sklep_internetowy.Models;
using B_KOM_Sklep_internetowy.ViewModels;
using NLog;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // GET: Home
        public ActionResult Index()
        {
            //logger.Info("test trace");
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

        //GET: /
        public ActionResult Search(string wyszukaj)
        {
            ViewBag.searchQuery = wyszukaj;

            var products = db.Products.Where(c => c.Name.ToLower().Contains(wyszukaj.ToLower()) && c.Hidden != true);
            var productsDTO = new List<ProductDTO>();

            foreach (var prod in products)
                productsDTO.Add(new ProductDTO() { Product = prod });

            return View(productsDTO);
        }
    }
}