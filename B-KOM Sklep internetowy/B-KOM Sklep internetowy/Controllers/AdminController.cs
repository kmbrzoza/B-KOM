﻿using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.Infrastructure;
using B_KOM_Sklep_internetowy.Models;
using B_KOM_Sklep_internetowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B_KOM_Sklep_internetowy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        InternetShopContext db = new InternetShopContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrdersList(int? idZamowienia)
        {
            var orders = db.Orders.Where(c => c.OrderId.ToString().Contains(idZamowienia.ToString())).Include("OrderItems").OrderByDescending(c => c.OrderId).ToList();

            return View(orders);
        }

        public ActionResult OrderDetails(int id)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            var order = db.Orders.Where(c => c.OrderId == id).Include("OrderItems").SingleOrDefault();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeOrderStatus(Order order)
        {
            Order orderToChange = db.Orders.Find(order.OrderId);
            orderToChange.OrderStatus = order.OrderStatus;
            db.SaveChanges();

            return RedirectToAction("OrderDetails", new { id = order.OrderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeOrderDetails(Order order)
        {
            if(!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("OrderDetails", new { id = order.OrderId });
            }

            Order orderToChange = db.Orders.Find(order.OrderId);

            orderToChange.FirstName = order.FirstName;
            orderToChange.LastName = order.LastName;
            orderToChange.Address = order.Address;
            orderToChange.City = order.City;
            orderToChange.PostCode = order.PostCode;
            orderToChange.Email = order.Email;
            orderToChange.Phone = order.Phone;
            orderToChange.Comment = order.Comment;

            db.SaveChanges();

            return RedirectToAction("OrderDetails", new { id = order.OrderId });
        }

        
        public ActionResult AddProduct()
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            AdminAddProductViewModel vm;
            vm = new AdminAddProductViewModel()
            {
                Product = new Product(),
                MainCategories = db.MainCategories.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(AdminAddProductViewModel model, HttpPostedFileBase productMainImg, 
            List<HttpPostedFileBase> productImages, HttpPostedFileBase productDescritpionImg)
        {
            //Checking if admin added file (productMainImg) its required
            if(productMainImg != null && productMainImg.ContentLength > 0)
            {
                if(ModelState.IsValid)
                {
                    model.Product.AddDate = DateTime.Now;

                    db.Products.Add(model.Product);
                    db.SaveChanges();

                    var fileExtMainImg = Path.GetExtension(productMainImg.FileName);
                    var fileNameMainImg = Guid.NewGuid() + fileExtMainImg;
                    var pathMainImg = Path.Combine(Server.MapPath(AppConfig.ProductImgFolder), fileNameMainImg);
                    productMainImg.SaveAs(pathMainImg);

                    db.ProductImages.Add(new ProductImage() { ImgPath = fileNameMainImg, MainImg = true, ProductId = model.Product.ProductId });

                    foreach(var img in productImages)
                    {
                        var fileExtImage = Path.GetExtension(img.FileName);
                        var fileNameImage = Guid.NewGuid() + fileExtImage;
                        var pathImage = Path.Combine(Server.MapPath(AppConfig.ProductImgFolder), fileNameImage);
                        img.SaveAs(pathImage);

                        db.ProductImages.Add(new ProductImage() { ImgPath = fileNameImage, ProductId = model.Product.ProductId });
                    }

                    var fileExtDescImg = Path.GetExtension(productDescritpionImg.FileName);
                    var fileNameDescImg = Guid.NewGuid() + fileExtDescImg;
                    var pathDescImg = Path.Combine(Server.MapPath(AppConfig.ProductImgFolder), fileNameDescImg);
                    productDescritpionImg.SaveAs(pathDescImg);

                    db.ProductImages.Add(new ProductImage() { ImgPath = fileNameDescImg, ProductId = model.Product.ProductId, DescriptionImg = true });

                    db.SaveChanges();

                    //When success
                    return RedirectToAction("AddProductSuccess", "Admin", new { id = model.Product.ProductId });
                }
                else
                {
                    var MainCat = db.MainCategories.ToList();
                    model.MainCategories = MainCat;

                    TempData["ViewData"] = ViewData;
                    return View(model);
                }
            }
            else
            {
                ViewBag.ErrorImg = "Musisz dodać główne zdjęcie!";
                //ModelState.AddModelError("", "Wskaż główne zdjęcie");
                var MainCat = db.MainCategories.ToList();
                model.MainCategories = MainCat;

                TempData["ViewData"] = ViewData;
                return View(model);
            }
        }

        public ActionResult AddProductSuccess(int id)
        {
            var product = db.Products.Find(id);
            return View(product);
        }

        public ActionResult ProductDetails(int id)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            var product = db.Products.Find(id);
            var mainCat = db.MainCategories.ToList();
            AdminProductDetailsViewModel vm = new AdminProductDetailsViewModel()
            {
                Product = product,
                MainCategories = mainCat
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var prodToEdit = db.Products.Find(product.ProductId);
                if (prodToEdit != null)
                {
                    prodToEdit.Name = product.Name;
                    prodToEdit.CategoryId = product.CategoryId;
                    prodToEdit.ShortDescription = product.ShortDescription;
                    prodToEdit.Description = product.Description;
                    prodToEdit.Price = product.Price;
                    prodToEdit.Promo = product.Promo;
                    prodToEdit.PromoPrice = product.PromoPrice;
                    prodToEdit.Recommended = product.Recommended;
                    prodToEdit.Bestseller = product.Bestseller;
                    prodToEdit.Hidden = product.Hidden;

                    db.SaveChanges();
                }
                
                return RedirectToAction("ProductDetails", new { id = product.ProductId });
            }
            else
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("ProductDetails", new { id = product.ProductId });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetMainImgProduct(Product product, HttpPostedFileBase productMainImg)
        {
            var productToChange = db.Products.Find(product.ProductId);

            if (productMainImg != null && productMainImg.ContentLength > 0)
            {
                // REMOVING
                ProductImage imageToRemove = productToChange.ProductImages.Where(c => c.MainImg == true).FirstOrDefault();

                string fullPath = Path.Combine(Server.MapPath(AppConfig.ProductImgFolder), imageToRemove.ImgPath);
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);

                db.ProductImages.Remove(imageToRemove);
                ////

                //NEW IMAGE
                var fileExtMainImg = Path.GetExtension(productMainImg.FileName);
                var fileNameMainImg = Guid.NewGuid() + fileExtMainImg;
                var pathMainImg = Path.Combine(Server.MapPath(AppConfig.ProductImgFolder), fileNameMainImg);
                productMainImg.SaveAs(pathMainImg);

                db.ProductImages.Add(new ProductImage() { ImgPath = fileNameMainImg, MainImg = true, ProductId = productToChange.ProductId });

                db.SaveChanges();

                TempData["Success"] = "Udalo sie zmienic zdjecie";
                return RedirectToAction("ProductDetails", new { id = productToChange.ProductId });
            }
            TempData["Success"] = "Error";
            return RedirectToAction("ProductDetails", new { id = productToChange.ProductId });
        }
    }
}