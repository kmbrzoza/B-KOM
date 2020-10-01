using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.DTO;
using B_KOM_Sklep_internetowy.Infrastructure;
using B_KOM_Sklep_internetowy.Models;
using B_KOM_Sklep_internetowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Configuration;
using System.Web;
using System.Web.Mvc;

namespace B_KOM_Sklep_internetowy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        InternetShopContext db = new InternetShopContext();
        ICacheProvider cache = new DefaultCacheProvider();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //ORDERS
        #region ORDERS
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
            if (!ModelState.IsValid)
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
        #endregion

        //PRODUCTS
        #region PRODUCTS
        public ActionResult AddProduct()
        {
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
            List<HttpPostedFileBase> productImages)
        {
            //Checking if admin added file (productMainImg) its required
            if (productMainImg != null && productMainImg.ContentLength > 0)
            {
                if (ModelState.IsValid)
                {
                    model.Product.AddDate = DateTime.Now;

                    db.Products.Add(model.Product);
                    db.SaveChanges();

                    var fileExtMainImg = Path.GetExtension(productMainImg.FileName);
                    var fileNameMainImg = Guid.NewGuid() + fileExtMainImg;
                    var pathMainImg = Path.Combine(Server.MapPath(AppConfig.ProductImgFolder), fileNameMainImg);
                    productMainImg.SaveAs(pathMainImg);

                    db.ProductImages.Add(new ProductImage() { ImgPath = fileNameMainImg, MainImg = true, ProductId = model.Product.ProductId });

                    foreach (var img in productImages)
                    {
                        var fileExtImage = Path.GetExtension(img.FileName);
                        var fileNameImage = Guid.NewGuid() + fileExtImage;
                        var pathImage = Path.Combine(Server.MapPath(AppConfig.ProductImgFolder), fileNameImage);
                        img.SaveAs(pathImage);

                        db.ProductImages.Add(new ProductImage() { ImgPath = fileNameImage, ProductId = model.Product.ProductId });
                    }
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

                TempData["MainImg"] = "Udalo sie zmienic główne zdjecie";
                return RedirectToAction("ProductDetails", new { id = productToChange.ProductId });
            }
            TempData["MainImg"] = "Błąd w zmianie nowego zdjęcia";
            return RedirectToAction("ProductDetails", new { id = productToChange.ProductId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImagesProduct(Product product, List<HttpPostedFileBase> productImages)
        {
            foreach (var img in productImages)
            {
                if (img != null && img.ContentLength > 0)
                    continue;
                else
                {
                    TempData["Images"] = "Błąd w dodawaniu nowych zdjęć!";
                    return RedirectToAction("ProductDetails", new { id = product.ProductId });
                }
            }

            foreach (var img in productImages)
            {
                var fileExtImage = Path.GetExtension(img.FileName);
                var fileNameImage = Guid.NewGuid() + fileExtImage;
                var pathImage = Path.Combine(Server.MapPath(AppConfig.ProductImgFolder), fileNameImage);
                img.SaveAs(pathImage);
                db.ProductImages.Add(new ProductImage() { ImgPath = fileNameImage, ProductId = product.ProductId });
            }
            db.SaveChanges();
            TempData["Images"] = "Udało się dodać nowe zdjęcia!";
            return RedirectToAction("ProductDetails", new { id = product.ProductId });

        }

        public ActionResult DeleteImgProduct(int productId, int imageId)
        {
            var img = db.ProductImages.Find(imageId);
            if (img != null)
            {
                db.ProductImages.Remove(img);
                db.SaveChanges();
                TempData["Images"] = "Udało się usunąć zdjęcie!";
                return RedirectToAction("ProductDetails", new { id = productId });
            }
            TempData["Images"] = "Błąd w usuwaniu zdjęcia!";
            return RedirectToAction("ProductDetails", new { id = productId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSpecsProduct(ProductSpecification prodSpecs)
        {
            if (prodSpecs.Value != null && prodSpecs.Specification.Name != null && prodSpecs.Product.ProductId != 0)
            {
                var specification = new Specification()
                {
                    Name = prodSpecs.Specification.Name
                };
                db.Specifications.Add(specification);
                db.SaveChanges();

                var productSpecification = new ProductSpecification()
                {
                    ProductId = prodSpecs.Product.ProductId,
                    SpecificationId = specification.SpecificationId,
                    Value = prodSpecs.Value
                };
                db.ProductSpecifications.Add(productSpecification);
                db.SaveChanges();

                TempData["Specs"] = "Udało się dodać nową specyfikację!";
                return RedirectToAction("ProductDetails", new { id = prodSpecs.Product.ProductId });
            }
            TempData["Specs"] = "Błąd dodawania specyfikacji! Nazwa i wartość są wymagane!";
            return RedirectToAction("ProductDetails", new { id = prodSpecs.Product.ProductId });
        }

        public ActionResult RemoveSpecsProduct(int prodId, int prodSpecsId)
        {
            if (prodSpecsId != 0)
            {
                var productSpecification = db.ProductSpecifications.Find(prodSpecsId);
                var specification = db.Specifications.Find(productSpecification.SpecificationId);
                db.ProductSpecifications.Remove(productSpecification);
                db.Specifications.Remove(specification);
                db.SaveChanges();

                TempData["Specs"] = "Udało się usunąć specyfikację!";
                return RedirectToAction("ProductDetails", new { id = prodId });
            }
            TempData["Specs"] = "Błąd usuwania specyfikacji!";
            return RedirectToAction("ProductDetails", new { id = prodId });
        }
        #endregion

        //CATEGORIES
        #region CATEGORIES
        public ActionResult Categories()
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            var vm = new AdminCategoriesViewModel()
            {
                MainCategories = db.MainCategories.ToList(),
                Categories = db.Categories.ToList(),
                MainCategory = new MainCategory(),
                Category = new Category()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMainCategory(MainCategory mainCategory, HttpPostedFileBase mainCategoryImg)
        {
            //if there is an image
            if (mainCategoryImg != null && mainCategoryImg.ContentLength > 0)
            {
                //and model is valid
                if (ModelState.IsValid)
                {
                    var fileExtMainCatImg = Path.GetExtension(mainCategoryImg.FileName);
                    var fileNameMainCatImg = Guid.NewGuid() + fileExtMainCatImg;
                    var pathMainImg = Path.Combine(Server.MapPath(AppConfig.MainCategoryIconsFolder), fileNameMainCatImg);
                    mainCategoryImg.SaveAs(pathMainImg);

                    mainCategory.ImgPath = fileNameMainCatImg;

                    db.MainCategories.Add(mainCategory);
                    db.SaveChanges();

                    cache.Invalidate(Consts.mainCategoriesLayoutCacheKey); // reset cache

                    TempData["MCSuccess"] = "Dodano nową główną kategorię!";
                    return RedirectToAction("Categories");
                }
                else
                {
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Categories");
                }
            }
            else
            {
                TempData["MCImage"] = "Musisz dodać zdjęcie głownej kategorii!";
                return RedirectToAction("Categories");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveMainCategory(int mainCategoryId)
        {
            if (ModelState.IsValid)
            {
                var mc = db.MainCategories.Find(mainCategoryId);

                string fullPath = Path.Combine(Server.MapPath(AppConfig.MainCategoryIconsFolder), mc.ImgPath);
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);

                db.MainCategories.Remove(mc);
                db.SaveChanges();

                cache.Invalidate(Consts.mainCategoriesLayoutCacheKey); //reset cache

                TempData["RemoveMCSuccess"] = "Udało się usunąć główną kategorię!";
                return RedirectToAction("Categories");
            }
            else
            {
                return RedirectToAction("Categories");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();

                cache.Invalidate(Consts.mainCategoriesLayoutCacheKey); //reset cache

                TempData["AddCSuccess"] = "Udało się dodać kategorię!";
                return RedirectToAction("Categories");
            }
            else
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Categories");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCategory(int categoryId)
        {
            if (categoryId > 0)
            {
                var cat = db.Categories.Find(categoryId);
                db.Categories.Remove(cat);
                db.SaveChanges();

                cache.Invalidate(Consts.mainCategoriesLayoutCacheKey); //reset cache

                TempData["RemoveCSuccess"] = "Udało się usunąć kategorię!";
                return RedirectToAction("Categories");
            }
            else
            {
                return RedirectToAction("Categories");
            }
        }
        #endregion

        public ActionResult Search(string search)
        {
            var products = db.Products.Where(c => c.Name.ToLower().Contains(search.ToLower()) || c.ProductId.ToString().Contains(search));
            var productsDTO = new List<ProductDTO>();

            foreach (var prod in products)
                productsDTO.Add(new ProductDTO() { Product = prod });

            return View(productsDTO);
        }


        //PROMOTIONS
        #region PROMOTIONS
        public ActionResult AddPromotion()
        {
            var prom = new Promotion();
            return View(prom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPromotion(Promotion promo, HttpPostedFileBase promotionImg)
        {
            //if there is an image
            if (promotionImg != null && promotionImg.ContentLength > 0)
            {
                //and model is valid
                if (ModelState.IsValid)
                {
                    var fileExtPromoImg = Path.GetExtension(promotionImg.FileName);
                    var fileNamePromoImg = Guid.NewGuid() + fileExtPromoImg;
                    var pathPromoImg = Path.Combine(Server.MapPath(AppConfig.PromotionImgFolder), fileNamePromoImg);
                    promotionImg.SaveAs(pathPromoImg);

                    promo.ImgPath = fileNamePromoImg;

                    db.Promotions.Add(promo);
                    db.SaveChanges();

                    TempData["PromoSuccess"] = "Dodano nową promocję!";
                    return RedirectToAction("AddPromotion");
                }
                else
                {
                    return View(promo);
                }
            }
            else
            {
                TempData["PromImage"] = "Musisz dodać zdjęcie promocji!";
                return View(promo);
            }

        }

        public ActionResult RemovePromotion(int promotionId)
        {
            var promo = db.Promotions.Find(promotionId);
            if (promo != null)
            {
                db.Promotions.Remove(promo);
                db.SaveChanges();
                return RedirectToAction("SearchPromotion");
            }
            else
                return RedirectToAction("PromotionDetails", new { id = promotionId });
        }

        public ActionResult SearchPromotion(string search)
        {
            var promotions = db.Promotions.Where(c => c.Name.ToLower().Contains(search) || c.PromotionId.ToString().Contains(search)).ToList();
            if (promotions.Any())
            {
                return View(promotions);
            }
            else
            {
                var prom = db.Promotions.Where(c => c.Hidden != true).ToList();
                return View(prom);
            }
        }

        public ActionResult PromotionDetails(int id)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            var promotion = db.Promotions.Find(id);

            //HERE I'm getting a list of PromotionProducts
            var promotionProdsList = promotion.PromotionProducts.ToList();
            //Next, I setting PromotionProducts.Product and PromotionProduct details to ProductPromotionDTO
            //ProductPromotionDTO helps in view (for example in showing opinions, prices, etc.)
            List<ProductPromotionDTO> promoProdsDTOList = new List<ProductPromotionDTO>();
            foreach (var promProd in promotionProdsList)
                promoProdsDTOList.Add(new ProductPromotionDTO()
                {
                    Product = promProd.Product,
                    Amount = promProd.Amount,
                    PromotionPrice = promProd.PromotionPrice,
                    PromotionProductId = promProd.PromotionProductId,
                    PromotionId = promotion.PromotionId
                });

            var vm = new AdminPromotionDetailsViewModel()
            {
                Promotion = promotion,
                PromotionProduct = new PromotionProduct(),
                ActualProductsList = promoProdsDTOList
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPromotion(Promotion promotion, HttpPostedFileBase promotionImg)
        {
            if (ModelState.IsValid)
            {
                var promoToChange = db.Promotions.Find(promotion.PromotionId);
                promoToChange.Name = promotion.Name;
                promoToChange.Code = promotion.Code;
                promoToChange.Hidden = promotion.Hidden;

                //If admin want to change image
                if (promotionImg != null && promotionImg.ContentLength > 0)
                {
                    //removing image
                    string fullPath = Path.Combine(Server.MapPath(AppConfig.PromotionImgFolder), promoToChange.ImgPath);
                    if (System.IO.File.Exists(fullPath))
                        System.IO.File.Delete(fullPath);
                    //adding new image
                    var fileExtPromoImg = Path.GetExtension(promotionImg.FileName);
                    var fileNamePromoImg = Guid.NewGuid() + fileExtPromoImg;
                    var pathPromoImg = Path.Combine(Server.MapPath(AppConfig.PromotionImgFolder), fileNamePromoImg);
                    promotionImg.SaveAs(pathPromoImg);

                    promoToChange.ImgPath = fileNamePromoImg;
                }

                db.SaveChanges();
                TempData["EditPromo"] = "Udało się edytować promocję!";
                return RedirectToAction("PromotionDetails", new { id = promoToChange.PromotionId });
            }
            else
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("PromotionDetails", new { id = promotion.PromotionId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPromotionProduct(PromotionProduct promotionProduct)
        {
            if (ModelState.IsValid)
            {
                if (promotionProduct.Amount <= 0)
                {
                    TempData["PromoProdAmount"] = "Ilość produktów musi być większa od 0!";
                    return RedirectToAction("PromotionDetails", new { id = promotionProduct.PromotionId });
                }
                if (promotionProduct.PromotionPrice <= 0)
                {
                    TempData["PromoProdPrice"] = "Cena produktów musi być większa od 0!";
                    return RedirectToAction("PromotionDetails", new { id = promotionProduct.PromotionId });
                }
                //////////////

                var prod = db.Products.Find(promotionProduct.ProductId);
                if (prod != null)
                {
                    if (db.PromotionProducts.Where(c => c.ProductId == promotionProduct.ProductId && c.PromotionId == promotionProduct.PromotionId).Any())
                    {
                        TempData["PromoProdIsAlready"] = "Produkt o takim ID już jest w promocji!";
                        return RedirectToAction("PromotionDetails", new { id = promotionProduct.PromotionId });
                    }

                    db.PromotionProducts.Add(promotionProduct);
                    db.SaveChanges();

                    TempData["PromoProdSuccess"] = "Udało się dodać produkt do promocji!";
                    return RedirectToAction("PromotionDetails", new { id = promotionProduct.PromotionId });
                }
                else
                {
                    TempData["PromoProdNotExist"] = "Produkt o takim ID nie istnieje";
                    return RedirectToAction("PromotionDetails", new { id = promotionProduct.PromotionId });
                }
            }
            else
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("PromotionDetails", new { id = promotionProduct.PromotionId });
            }
        }

        public ActionResult RemovePromotionProduct(int promotionId, int promotionProductId)
        {
            var promoProd = db.PromotionProducts.Find(promotionProductId);
            if (promoProd != null)
            {
                db.PromotionProducts.Remove(promoProd);
                db.SaveChanges();
            }
            return RedirectToAction("PromotionDetails", new { id = promotionId });
        }


        public ActionResult HotDeal(string search)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            var newHotDeal = new PromotionHotDeal();

            List<PromotionHotDeal> listHotDeals;

            if (search == null || search == "")
            {
                listHotDeals = db.PromotionHotDeals.Where(c => c.Active == true).ToList();
            }
            else
            {
                listHotDeals = db.PromotionHotDeals.Where(c => c.Product.Name.ToLower().Contains(search.ToLower()) || c.PromotionHotDealId.ToString().Contains(search)).ToList();
            }

            var vm = new AdminHotDealViewModel()
            {
                listHotDeals = listHotDeals,
                newHotDeal = newHotDeal
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddHotDeal(PromotionHotDeal newHotDeal)
        {
            if (ModelState.IsValid)
            {
                newHotDeal.AmountLeft = newHotDeal.Amount;
                if (newHotDeal.Active)
                {
                    var hdActive = db.PromotionHotDeals.Where(c => c.Active == true).SingleOrDefault();
                    if (hdActive != null)
                        hdActive.Active = false;

                    db.PromotionHotDeals.Add(newHotDeal);
                    db.SaveChanges();
                    TempData["HotDealSuccess"] = "Udało się dodać nową gorącą okazję!";
                    return RedirectToAction("HotDeal");
                }
                else
                {
                    db.PromotionHotDeals.Add(newHotDeal);
                    db.SaveChanges();
                    TempData["HotDealSuccess"] = "Udało się dodać nową gorącą okazję!";
                    return RedirectToAction("HotDeal");
                }
            }
            else
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("HotDeal");
            }
        }

        public ActionResult HotDealDetails(int id)
        {
            var hd = db.PromotionHotDeals.Where(c => c.PromotionHotDealId == id).SingleOrDefault();
            if (hd != null)
                return View(hd);
            else
                return RedirectToAction("HotDeal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotDealDetails(PromotionHotDeal hotdeal)
        {
            if(ModelState.IsValid)
            {
                if(hotdeal.AmountLeft > hotdeal.Amount)
                {
                    TempData["HotDealEditAmount"] = "Pozostała ilość musi być mniejsza od ilości!";
                    return View(hotdeal);
                }
                if(hotdeal.Active)
                {
                    var hdActive = db.PromotionHotDeals.Where(c => c.Active == true).SingleOrDefault();
                    if (hdActive != null)
                        hdActive.Active = false;
                }
                db.PromotionHotDeals.AddOrUpdate(hotdeal);
                db.SaveChanges();

                TempData["HotDealEdit"] = "Udało się edytować gorącą okazję!";
                return View();
            }
            else
            {
                return View(hotdeal);
            }
        }


        #endregion

        //OPINIONS
        #region Opinions
        public ActionResult Opinions()
        {
            var opinions = db.Opinions.Where(c => c.Accepted == false).ToList();
            return View(opinions);
        }


        public ActionResult AddOpinion(int id)
        {
            var opinion = db.Opinions.Where(c => c.Opinionid == id).SingleOrDefault();
            if(opinion != null)
            {
                opinion.Accepted = true;
                db.SaveChanges();
                TempData["OpinionSuccess"] = "Udało się zaakceptować komentarz";
            }
            return RedirectToAction("Opinions");
        }

        public ActionResult RemoveOpinion(int id)
        {
            var opinion = db.Opinions.Where(c => c.Opinionid == id).SingleOrDefault();
            if(opinion != null)
            {
                db.Opinions.Remove(opinion);
                db.SaveChanges();
                TempData["OpinionSuccess"] = "Udało się usunąć komentarz";
            }
            return RedirectToAction("Opinions");
        }
        #endregion

        // E-MAILS / MAILING
        [AllowAnonymous]
        public ActionResult OrderConfirmationEmail(int orderId)
        {
            var order = db.Orders.Where(c => c.OrderId == orderId).Include("OrderItems").Include("OrderItems.Product").Include("OrderItems.Product.ProductImages").SingleOrDefault();

            if (order == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            OrderConfirmationEmail email = new OrderConfirmationEmail();
            email.To = order.Email;
            email.From = Consts.shopEmail;
            email.OrderValue = order.OrderValue;
            email.OrderId = order.OrderId;
            email.OrderItems = order.OrderItems;

            email.Send();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}