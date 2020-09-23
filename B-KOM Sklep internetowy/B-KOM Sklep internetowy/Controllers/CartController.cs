using B_KOM_Sklep_internetowy.App_Start;
using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.DTO;
using B_KOM_Sklep_internetowy.Infrastructure;
using B_KOM_Sklep_internetowy.Models;
using B_KOM_Sklep_internetowy.ViewModels;
using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace B_KOM_Sklep_internetowy.Controllers
{
    public class CartController : Controller
    {
        private CartManager cartManager;
        private ISessionManager session { get; set; }
        private InternetShopContext db { get; set; }
        private IMailService mailService;

        public CartController(IMailService mailService)
        {
            db = new InternetShopContext();
            session = new SessionManager();
            cartManager = new CartManager(session, db);
            this.mailService = mailService;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cart = cartManager.GetCart();
            var cartItemsDTOList = new List<CartItemDTO>();

            foreach (var cartItem in cart)
                cartItemsDTOList.Add(new CartItemDTO() { CartItem = cartItem });

            CartViewModel CartVM = new CartViewModel()
            {
                CartItemsDTO = cartItemsDTOList
            };
            return View(CartVM);
        }

        // GET: Dodaj
        public ActionResult AddToCart(int id)
        {
            cartManager.AddToCart(id);
            return RedirectToAction("Index");
        }

        public ActionResult ClearCart()
        {
            cartManager.ClearCart();
            return RedirectToAction("Index");
        }

        public ActionResult RemoveCartItem(int id)
        {
            cartManager.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public int GetAmountCartItems()
        {
            return cartManager.GetAmountCartItems();
        }


        public async Task<ActionResult> Order()
        {
            if (Request.IsAuthenticated)
            {
                //if user has no items in cart
                //then he can't create an order
                var cart = cartManager.GetCart();
                if (cart.Count <= 0)
                    return RedirectToAction("Index", "Cart");

                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                var order = new Order
                {
                    FirstName = user.UserData.FirstName,
                    LastName = user.UserData.LastName,
                    Address = user.UserData.Address,
                    City = user.UserData.City,
                    PostCode = user.UserData.PostCode,
                    Email = user.UserData.Email,
                    Phone = user.UserData.Phone,
                };
                return View(order);
            }
            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Order", "Cart") });
        }

        [HttpPost]
        public async Task<ActionResult> Order(Order orderDetails)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var newOrder = cartManager.CreateOrder(orderDetails, userId);

                //UPDATING USERDATE FOR USER
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.UserData);
                await UserManager.UpdateAsync(user);

                cartManager.ClearCart();

                //SENDING E-MAIL
                var order = db.Orders.Where(c => c.OrderId == newOrder.OrderId).Include("OrderItems").Include("OrderItems.Product").Include("OrderItems.Product.ProductImages").SingleOrDefault();
                mailService.SendOrderConfirmationEmail(order);

                return RedirectToAction("OrderConfirmation");
            }
            else
                return View(orderDetails);
        }

        public ActionResult OrderConfirmation()
        {
            return View();
        }


        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}