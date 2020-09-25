using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B_KOM_Sklep_internetowy.Infrastructure
{
    public static class UrlHelpers
    {
        public static string MainCategoryIconsPath(this UrlHelper helper, string iconPath)
        {
            var mainCategoryIconsFolder = AppConfig.MainCategoryIconsFolder;
            var path = Path.Combine(mainCategoryIconsFolder, iconPath);
            var pathRelative = helper.Content(path);

            return pathRelative;
        }

        public static string ProductImgPath(this UrlHelper helper, string productPath)
        {
            var productImgFolder = AppConfig.ProductImgFolder;
            var path = Path.Combine(productImgFolder, productPath);
            var pathRelative = helper.Content(path);

            return pathRelative;
        }

        public static string PromotionImgPath(this UrlHelper helper, string promotionPath)
        {
            var promotionImgFolder = AppConfig.PromotionImgFolder;
            var path = Path.Combine(promotionImgFolder, promotionPath);
            var pathRelative = helper.Content(path);

            return pathRelative;
        }

    }
}