using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.DTO
{
    public class CartItemDTO
    {
        public CartItem CartItem{ get; set; }


        public string GetCartItemProductPrice()
        {
            string price = CartItem.Product.Price.ToString("0.00") + " zł";
            return price;
        }

        public string GetCartItemProductPromoPrice()
        {
            string price = CartItem.Product.PromoPrice.ToString("0.00") + " zł";
            return price;
        }

        
        public string GetCartItemProductImg()
        {
            ProductImage productMainImg = CartItem.Product.ProductImages.Where(c => c.MainImg == true).FirstOrDefault();
            if (productMainImg == null)
                return "";
            return productMainImg.ImgPath;
        }
    }
}