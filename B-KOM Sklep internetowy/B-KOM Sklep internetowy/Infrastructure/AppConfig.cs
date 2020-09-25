using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Infrastructure
{
    public class AppConfig
    {
        private static string _mainCategoryIconsFolder = ConfigurationManager.AppSettings["MainCategoryIconsFolder"];

        public static string MainCategoryIconsFolder
        {
            get
            {
                return _mainCategoryIconsFolder;
            }
        }

        private static string _productImgFolder = ConfigurationManager.AppSettings["ProductImgFolder"];

        public static string ProductImgFolder
        {
            get
            {
                return _productImgFolder;
            }
        }

        private static string _promotionImgFolder = ConfigurationManager.AppSettings["PromotionImgFolder"];

        public static string PromotionImgFolder
        {
            get
            {
                return _promotionImgFolder;
            }
        }
    }
}