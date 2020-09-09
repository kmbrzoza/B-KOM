using B_KOM_Sklep_internetowy.Migrations;
using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.DAL
{
    public class DatabaseInitializer : MigrateDatabaseToLatestVersion<InternetShopContext, Configuration>
    {
        public static void SeedDatabase(InternetShopContext context)
        {
            var mainCategories = new List<MainCategory>
            {
                new MainCategory() { MainCategoryId=1, Name="Laptopy i komputery", ImgPath="laptop.png" },
                new MainCategory() { MainCategoryId=2, Name="Podzespoły komputerowe", ImgPath="cpu.png" },
                new MainCategory() { MainCategoryId=3, Name="Urządzennia peryferyjne", ImgPath="peripherals.png" },
                new MainCategory() { MainCategoryId=4, Name="Smartfony i akcesoria", ImgPath="smartphone.png" },
                new MainCategory() { MainCategoryId=5, Name="Akcesoria", ImgPath="accessories.png" },
                new MainCategory() { MainCategoryId=6, Name="TV, Smarthome, Lifestyle", ImgPath="smarthome.png" },
            };

            mainCategories.ForEach(c => context.MainCategories.AddOrUpdate(c));
            context.SaveChanges();

            var categories = new List<Category>
            {
                new Category() { CategoryId=1, Name="Laptopy", MainCategoryId=1, LinkName="Laptopy"},
                new Category() { CategoryId=2, Name="Komputery", MainCategoryId=1, LinkName="Komputery"},
                new Category() { CategoryId=3, Name="Procesory", MainCategoryId=2, LinkName="Procesory"},
                new Category() { CategoryId=4, Name="Karty graficzne", MainCategoryId=2, LinkName="Karty_graficzne"},
                new Category() { CategoryId=5, Name="Pamięci RAM", MainCategoryId=2, LinkName="Pamieci_Ram"},
                new Category() { CategoryId=6, Name="Dyski", MainCategoryId=2, LinkName="Dyski"},
                new Category() { CategoryId=7, Name="Monitory", MainCategoryId=3, LinkName="Monitory"},
                new Category() { CategoryId=8, Name="Myszki", MainCategoryId=3, LinkName="Myszki"},
                new Category() { CategoryId=9, Name="Klawiatury", MainCategoryId=3, LinkName="Klawiatury"},
                new Category() { CategoryId=10, Name="Słuchawki i mikrofony", MainCategoryId=3, LinkName="Sluchawki_i_mikrofony"},
                new Category() { CategoryId=11, Name="Smartfony", MainCategoryId=4, LinkName="Smartfony"},
                new Category() { CategoryId=12, Name="Etui, szkła i folie do smartfonów", MainCategoryId=4, LinkName="Etui_Szkla_Folie"},
                new Category() { CategoryId=13, Name="Akcesoria do smartfonów", MainCategoryId=4, LinkName="Smartfony_akcesoria"},
                new Category() { CategoryId=14, Name="Smartwatche", MainCategoryId=4, LinkName="Smartwatche"},
                new Category() { CategoryId=15, Name="Kable i przejściówki", MainCategoryId=5, LinkName="Kable_i_przejsciowki"},
                new Category() { CategoryId=16, Name="Zasilanie", MainCategoryId=5, LinkName="Zasilanie"},
                new Category() { CategoryId=17, Name="Pamięci Flash", MainCategoryId=5, LinkName="Pamieci_Flash"},
                new Category() { CategoryId=18, Name="Telewizory", MainCategoryId=6, LinkName="Telewizory"},
                new Category() { CategoryId=19, Name="Aparaty fotograficzne i kamery", MainCategoryId=6, LinkName="Aparaty_Kamery"},
                new Category() { CategoryId=20, Name="Drony i akcesoria", MainCategoryId=6, LinkName="Drony"},
            };

            categories.ForEach(c => context.Categories.AddOrUpdate(c));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product() { ProductId=1, CategoryId=1, Name = "HP Pavilion Gaming i5-9300H/16GB/256 GTX1650 Green", Price=3499,
                                ImgPath="1/1.png", 
                                AddDate=DateTime.Now, Hidden=false, Recommended=true, Bestseller=true, Promo=true, PromoPrice=3249 },
                new Product() { ProductId=2, CategoryId=9, Name = "SPC Gear GK-530 Kailh Brown", Price=219,
                                ImgPath="2/1.png", 
                                AddDate=DateTime.Now, Hidden=false, Recommended=true, Bestseller=true, Promo=false, PromoPrice=0 },
                new Product() { ProductId=3, CategoryId=10, Name = "Edifier W820BT", Price=259,
                                ImgPath="3/1.png",
                                AddDate=DateTime.Now, Hidden=false, Recommended=true, Bestseller=true, Promo=true, PromoPrice=229 },
                new Product() { ProductId=4, CategoryId=7, Name = "Monitor Acer SA240YABI", Price=449,
                                ImgPath="4/1.png",
                                AddDate=DateTime.Now, Hidden=false, Recommended=true, Bestseller=true, Promo=false, PromoPrice=0 },
                new Product() { ProductId=5, CategoryId=8, Name = "Mysz Logitech G102", Price=119,
                                ImgPath="5/1.png",
                                AddDate=DateTime.Now, Hidden=false, Recommended=true, Bestseller=true, Promo=false, PromoPrice=0 },
                new Product() { ProductId=6, CategoryId=11, Name = "Xiaomi Mi 10 8/128", Price=3499,
                                ImgPath="6/1.png",
                                AddDate=DateTime.Now, Hidden=false, Recommended=true, Bestseller=true, Promo=true, PromoPrice=2999 },
                new Product() { ProductId=7, CategoryId=3, Name = "AMD Ryzen 5 3600", Price=849,
                                ImgPath="7/1.png",
                                AddDate=DateTime.Now, Hidden=false, Recommended=true, Bestseller=true, Promo=false, PromoPrice=0 },
                new Product() { ProductId=8, CategoryId=4, Name = "MSI Geforce RTX 2080 SUPER GAMING X TRIO 8GB GDDR6", Price=3899.9m,
                                ImgPath="8/1.png",
                                AddDate=DateTime.Now, Hidden=false, Recommended=true, Bestseller=true, Promo=false, PromoPrice=0 },
                new Product() { ProductId=9, CategoryId=11, Name = "Apple IPhone 11 Pro MAX", Price=5299,
                                ImgPath="9/1.png",
                                AddDate=DateTime.Now, Hidden=false, Recommended=false, Bestseller=false, Promo=false, PromoPrice=0 },
                new Product() { ProductId=10, CategoryId=11, Name = "Samsung Note 20 Ultra", Price=5999,
                                ImgPath="10/1.png",
                                AddDate=DateTime.Now, Hidden=false, Recommended=false, Bestseller=false, Promo=false, PromoPrice=0 },
                new Product() { ProductId=11, CategoryId=11, Name = "Xiaomi Redmi Note 9 Pro", Price=1229,
                                ImgPath="11/1.png",
                                AddDate=DateTime.Now, Hidden=false, Recommended=false, Bestseller=false, Promo=false, PromoPrice=0 },
                new Product() { ProductId=12, CategoryId=11, Name = "Xiaomi Mi Note 10 Lite 6/128", Price=1699,
                                ImgPath="12/1.png",
                                AddDate=DateTime.Now, Hidden=false, Recommended=false, Bestseller=false, Promo=false, PromoPrice=0 },

            };

            products.ForEach(c => context.Products.AddOrUpdate(c));
            context.SaveChanges();

            var productImg = new List<ProductImage>
            {
                new ProductImage() { ProductImageId=1, ProductId=1, ImgPath="1/1.png", MainImg=true },
                new ProductImage() { ProductImageId=2, ProductId=2, ImgPath="2/1.png", MainImg=true},
                new ProductImage() { ProductImageId=3, ProductId=3, ImgPath="3/1.png", MainImg=true},
                new ProductImage() { ProductImageId=4, ProductId=4, ImgPath="4/1.png", MainImg=true},
                new ProductImage() { ProductImageId=5, ProductId=5, ImgPath="5/1.png", MainImg=true},
                new ProductImage() { ProductImageId=6, ProductId=6, ImgPath="6/1.png", MainImg=true},
                new ProductImage() { ProductImageId=7, ProductId=7, ImgPath="7/1.png", MainImg=true},
                new ProductImage() { ProductImageId=8, ProductId=8, ImgPath="8/1.png", MainImg=true},
                new ProductImage() { ProductImageId=9, ProductId=9, ImgPath="9/1.png", MainImg=true},
                new ProductImage() { ProductImageId=10, ProductId=10, ImgPath="10/1.png", MainImg=true},
                new ProductImage() { ProductImageId=11, ProductId=11, ImgPath="11/1.png", MainImg=true},
                new ProductImage() { ProductImageId=12, ProductId=12, ImgPath="12/1.png", MainImg=true},
                new ProductImage() { ProductImageId=13, ProductId=6, ImgPath="2/1.png"},
                new ProductImage() { ProductImageId=14, ProductId=6, ImgPath="3/1.png"},
                new ProductImage() { ProductImageId=15, ProductId=6, ImgPath="4/1.png"},
                new ProductImage() { ProductImageId=16, ProductId=6, ImgPath="4/description.png", DescriptionImg=true},
            };

            productImg.ForEach(c => context.ProductImages.AddOrUpdate(c));
            context.SaveChanges();
        }
    }
}