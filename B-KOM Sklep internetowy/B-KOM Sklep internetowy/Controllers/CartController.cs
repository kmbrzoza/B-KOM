using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.DTO;
using B_KOM_Sklep_internetowy.Infrastructure;
using B_KOM_Sklep_internetowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B_KOM_Sklep_internetowy.Controllers
{
    public class CartController : Controller
    {
        private CartManager cartManager;
        private ISessionManager session { get; set; }
        private InternetShopContext db { get; set; }

        public CartController()
        {
            db = new InternetShopContext();
            session = new SessionManager();
            cartManager = new CartManager(session, db);
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
    }
}