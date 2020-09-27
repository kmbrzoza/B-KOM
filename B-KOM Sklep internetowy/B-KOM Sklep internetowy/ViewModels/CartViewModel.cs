using B_KOM_Sklep_internetowy.DTO;
using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.ViewModels
{
    public class CartViewModel
    {
        public List<CartItemDTO> CartItemsDTO { get; set; }

        public string PromoCode { get; set; }

        private decimal GetTotalPriceDcm()
        {
            decimal sum = 0;
            foreach (var cartItem in CartItemsDTO)
            {
                if(cartItem.CartItem.PromoCode)
                {
                    sum = sum + (cartItem.CartItem.PromoCodePrice);
                    if(cartItem.CartItem.Amount > 1)
                    {
                        if (cartItem.CartItem.Product.Promo)
                        {
                            sum = sum + (cartItem.CartItem.Product.PromoPrice * (cartItem.CartItem.Amount - 1));
                        }
                        else
                        {
                            sum = sum + (cartItem.CartItem.Product.Price * (cartItem.CartItem.Amount - 1 ));
                        }
                    }
                }
                else if (cartItem.CartItem.Product.Promo)
                {
                    sum = sum + (cartItem.CartItem.Product.PromoPrice * cartItem.CartItem.Amount);
                }
                else
                {
                    sum = sum + (cartItem.CartItem.Product.Price * cartItem.CartItem.Amount);
                }
            }
            return sum;
        }
        public string GetTotalPrice()
        {
            decimal sum = GetTotalPriceDcm();

            string price = sum.ToString("0.00") + " zł";
            return price;
        }
        public string GetTotalSavedPrice()
        {
            decimal sumWithoutPromo = 0;
            foreach (var cartItem in CartItemsDTO)
            {
                sumWithoutPromo = sumWithoutPromo + (cartItem.CartItem.Product.Price * cartItem.CartItem.Amount);
            }

            decimal sumWithPromo = GetTotalPriceDcm();

            decimal saved = sumWithoutPromo - sumWithPromo;
            string savedStr = saved.ToString("0.00") + " zł";
            return savedStr;
        }
    }
}