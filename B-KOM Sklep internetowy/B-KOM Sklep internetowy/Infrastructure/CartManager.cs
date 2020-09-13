using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Infrastructure
{
    public class CartManager
    {
        private InternetShopContext db;
        private ISessionManager session;

        public CartManager(ISessionManager session, InternetShopContext db)
        {
            this.session = session;
            this.db = db;
        }

        public List<CartItem> GetCart()
        {
            List<CartItem> cart;

            if (session.Get<List<CartItem>>(Consts.cartSessionKey) == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = session.Get<List<CartItem>>(Consts.cartSessionKey);
            }

            return cart;
        }

        public void AddToCart(int productId)
        {
            var cart = GetCart();
            var cartItem = cart.Find(c => c.Product.ProductId == productId);

            if (cartItem != null)
                cartItem.Amount++;
            else
            {
                var productToAdd = db.Products.Where(c => c.ProductId == productId).SingleOrDefault();

                if (productToAdd != null)
                {
                    var newCartItem = new CartItem()
                    {
                        Product = productToAdd,
                        Amount = 1,

                    };
                    cart.Add(newCartItem);
                }
            }
            session.Set(Consts.cartSessionKey, cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var cartItem = cart.Find(c => c.Product.ProductId == productId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
            }
        }

        public decimal GetValueCart()
        {
            var cart = GetCart();
            decimal sum = 0;
            foreach(var cartItem in cart)
            {
                if(cartItem.Product.Promo)
                {
                    sum = sum + cartItem.Product.PromoPrice;
                }
                else
                {
                    sum = sum + cartItem.Product.Price;
                }
            }
            return sum;
        }

        public int GetAmountCartItems()
        {
            var cart = GetCart();
            return cart.Sum(c => c.Amount);
        }

        public Order CreateOrder(Order newOrder, string userId)
        {
            var cart = GetCart();
            newOrder.OrderDate = DateTime.Now;

            db.Orders.Add(newOrder);

            if (newOrder.OrderItems == null)
                newOrder.OrderItems = new List<OrderItem>();

            decimal cartValue = 0;

            foreach (var cartItem in cart)
            {
                var orderItem = new OrderItem()
                {
                    ProductId = cartItem.Product.ProductId,
                    Amount = cartItem.Amount,
                    Price = cartItem.Product.Price //ITS BAD I THINK (Promo)
                };

                cartValue += (cartItem.Amount * cartItem.Product.Price);
                newOrder.OrderItems.Add(orderItem);
            }

            newOrder.OrderValue = cartValue;
            db.SaveChanges();

            return newOrder;
        }

        public void ClearCart()
        {
            session.Set<List<CartItem>>(Consts.cartSessionKey, null);
        }
    }
}