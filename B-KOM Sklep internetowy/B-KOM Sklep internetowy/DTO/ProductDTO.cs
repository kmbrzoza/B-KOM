using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.DTO
{
    public class ProductDTO
    {
        public Product Product { get; set; }

        public string getProductPrice()
        {
            string price = Product.Price.ToString("0.00") + " zł";
            return price;
        }

        public string getProductPromoPrice()
        {
            string price = Product.PromoPrice.ToString("0.00") + " zł";
            return price;
        }

        public string getMainImg()
        {
            ProductImage productMainImg = Product.ProductImages.Where(c => c.MainImg == true).FirstOrDefault();
            return productMainImg.ImgPath;
        }
    }
}