using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.DTO
{
    public class ProductDTO
    {
        public Product Product { get; set; }

        //   PRICES 
        #region PRICES
        public string GetProductPrice()
        {
            string price = Product.Price.ToString("0.00") + " zł";
            return price;
        }

        public string GetProductPromoPrice()
        {
            string price = Product.PromoPrice.ToString("0.00") + " zł";
            return price;
        }

        public string GetProductSavedPrice()
        {
            if (Product.Promo)
            {
                decimal price = (Product.Price - Product.PromoPrice);
                string strPrice = price.ToString("0.00") + " zł";
                return strPrice;
            }
            else
                return "";
        }
        #endregion

        //   IMAGES
        #region Images
        public string GetMainImg()
        {
            ProductImage productMainImg = Product.ProductImages.Where(c => c.MainImg == true).FirstOrDefault();
            if (productMainImg == null)
                return "";
            return productMainImg.ImgPath;
        }

        public List<ProductImage> GetProductImagesList()
        {
            List<ProductImage> list = Product.ProductImages.ToList();
            return list;
        }
        #endregion

        //   OPINIONS
        #region OPINIONS
        public int GetNumberOpinionInt()
        {
            int numb = Product.Opinions.Where(c => c.Accepted == true).Count();
            return numb;
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

        public string GetOpinionScoreStr()
        {
            decimal starRate = GetOpinionScoreDec();
            string starRateStr = (starRate).ToString("0.0");
            return starRateStr;
        }
        public int GetNumberScoreYellowStarsInt()
        {
            decimal starRate = GetOpinionScoreDec();
            string starRateStr = (starRate).ToString("0");
            int starRateInt = int.Parse(starRateStr);
            return starRateInt;
        }

        public int GetNumberStatisticStarInt(int starValue)
        {
            int stars = Product.Opinions.Where(c => c.StarsValue == starValue).Count();
            return stars;
        }
        public string GetPercentageStatisticStarStr(int starValue)
        {
            int stars = GetNumberStatisticStarInt(starValue);
            int opinionNumber = GetNumberOpinionInt();
            if (opinionNumber == 0)
            {
                return "0%";
            }
            int percentage = (int)(decimal.Divide(stars, opinionNumber) * 100);
            string strPer = percentage.ToString() + "%";
            return strPer;
        }
        #endregion

        public bool FavouriteForUser { get; set; }
    }
}