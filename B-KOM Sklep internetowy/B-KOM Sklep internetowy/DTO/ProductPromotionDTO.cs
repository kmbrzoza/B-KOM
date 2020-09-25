using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.DTO
{
    public class ProductPromotionDTO
    {
        public Product Product { get; set; }
        public int PromotionProductId { get; set; }
        public int PromotionId { get; set; }
        public int Amount { get; set; }
        public decimal PromotionPrice { get; set; }

        public string GetMainImg()
        {
            ProductImage productMainImg = Product.ProductImages.Where(c => c.MainImg == true).FirstOrDefault();
            if (productMainImg == null)
                return "";
            return productMainImg.ImgPath;
        }

        public string GetProductNormalPrice()
        {
            string price;
            if(Product.Promo)
            {
                price = Product.PromoPrice.ToString("0.00") + " zł";
            }
            else
            {
                price = Product.Price.ToString("0.00") + " zł";
            }
            return price;
        }

        //OPINIONS
        #region OPINIONS
        public int GetNumberScoreYellowStarsInt()
        {
            decimal starRate = GetOpinionScoreDec();
            string starRateStr = (starRate).ToString("0");
            int starRateInt = int.Parse(starRateStr);
            return starRateInt;
        }
        public decimal GetOpinionScoreDec()
        {
            decimal starRate = 0;
            int opinionNumber = GetNumberOpinionInt();
            if (opinionNumber > 0)
            {
                decimal sum = 0;
                foreach (var opinion in Product.Opinions.Where(c => c.Accepted == true))
                {
                    sum = sum + opinion.StarsValue;
                }
                starRate = (sum / opinionNumber);
            }
            return starRate;
        }

        public int GetNumberOpinionInt()
        {
            int numb = Product.Opinions.Where(c => c.Accepted == true).Count();
            return numb;
        }
        #endregion
    }
}