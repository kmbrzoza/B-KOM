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

            //PROMOCODE
            foreach(var cartItem in cart)
            {
                if(cartItem.PromoCode)
                {
                    var code = GetPromoCode();
                    var promo = db.Promotions.Where(c => c.Code == code).SingleOrDefault();
                    var promoProd = promo.PromotionProducts.Where(c => c.ProductId == cartItem.Product.ProductId).SingleOrDefault();

                    if(promoProd.Amount < 1)
                    {
                        cartItem.PromoCode = false;
                        session.Set<string>(Consts.cartSessionPromoCode, null);
                        break;
                    }
                }
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
                if(cartItem.PromoCode)
                {
                    cartItem.PromoCode = false;
                    session.Set<string>(Consts.cartSessionPromoCode, null);
                }
                cart.Remove(cartItem);
            }
        }

        //public decimal GetValueCart()
        //{
        //    var cart = GetCart();
        //    decimal sum = 0;
        //    foreach(var cartItem in cart)
        //    {
        //        if(cartItem.Product.Promo)
        //        {
        //            sum = sum + cartItem.Product.PromoPrice;
        //        }
        //        else
        //        {
        //            sum = sum + cartItem.Product.Price;
        //        }
        //    }
        //    return sum;
        //}

        public int GetAmountCartItems()
        {
            var cart = GetCart();
            return cart.Sum(c => c.Amount);
        }

        public Order CreateOrder(Order newOrder, string userId)
        {
            var cart = GetCart();
            newOrder.OrderDate = DateTime.Now;
            newOrder.UserId = userId;

            db.Orders.Add(newOrder);

            if (newOrder.OrderItems == null)
                newOrder.OrderItems = new List<OrderItem>();

            decimal cartValue = 0;

            foreach (var cartItem in cart)
            {
                if (cartItem.PromoCode)
                {
                    var code = GetPromoCode();
                    var promo = db.Promotions.Where(c => c.Code == code).SingleOrDefault();
                    var promoProd = promo.PromotionProducts.Where(c => c.ProductId == cartItem.Product.ProductId).SingleOrDefault();

                    if(promoProd != null && promoProd.Amount>0)
                    {
                        decimal price = cartItem.PromoCodePrice;
                        if (cartItem.Amount > 1)
                        {
                            if (cartItem.Product.Promo)
                                price = price + cartItem.Product.PromoPrice * (cartItem.Amount - 1);
                            else
                                price = price + cartItem.Product.Price * (cartItem.Amount - 1);
                        }

                        var orderItem = new OrderItem()
                        {
                            ProductId = cartItem.Product.ProductId,
                            Amount = cartItem.Amount,
                            Price = price
                        };
                        cartValue += price;
                        newOrder.OrderItems.Add(orderItem);

                        promoProd.Amount--;
                        db.SaveChanges();
                        session.Set<string>(Consts.cartSessionPromoCode, null);

                        continue;
                    }
                    else
                    {
                        cartItem.PromoCode = false;
                        session.Set<string>(Consts.cartSessionPromoCode, null);
                    }    

                }
                
                if (cartItem.Product.Promo)
                {
                    var orderItem = new OrderItem()
                    {
                        ProductId = cartItem.Product.ProductId,
                        Amount = cartItem.Amount,
                        Price = cartItem.Product.PromoPrice
                    };
                    cartValue += (cartItem.Amount * cartItem.Product.PromoPrice);
                    newOrder.OrderItems.Add(orderItem);
                }
                else
                {
                    var orderItem = new OrderItem()
                    {
                        ProductId = cartItem.Product.ProductId,
                        Amount = cartItem.Amount,
                        Price = cartItem.Product.Price
                    };
                    cartValue += (cartItem.Amount * cartItem.Product.Price);
                    newOrder.OrderItems.Add(orderItem);
                }

            }

            newOrder.OrderValue = cartValue;
            db.SaveChanges();

            return newOrder;
        }

        public void ClearCart()
        {
            session.Set<List<CartItem>>(Consts.cartSessionKey, null);
            session.Set<string>(Consts.cartSessionPromoCode, null);
        }

        public bool PromoCode(string code)
        {
            var promo = db.Promotions.Where(c => c.Code.ToLower() == code.ToLower()).FirstOrDefault();
            if (promo != null)
            {
                var cart = GetCart();

                foreach (var cartItem in cart)
                {
                    var promoProd = promo.PromotionProducts.Where(c => c.ProductId == cartItem.Product.ProductId).SingleOrDefault();
                    if (promoProd != null && promoProd.Amount > 0)
                    {
                        cartItem.PromoCode = true;
                        cartItem.PromoCodePrice = promoProd.PromotionPrice;
                        session.Set<string>(Consts.cartSessionPromoCode, code.ToLower());
                        session.Set<List<CartItem>>(Consts.cartSessionKey, cart);

                        return true;
                    }
                }
            }
            return false;
        }

        public string GetPromoCode()
        {
            if (session.Get<string>(Consts.cartSessionPromoCode) != null)
            {
                return session.Get<string>(Consts.cartSessionPromoCode);
            }
            else
                return null;
        }
    }
}