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
                new MainCategory() { MainCategoryId=1, Name="Laptopy i komputery", ImgPath="images\\Categories\\laptop.png" },
                new MainCategory() { MainCategoryId=2, Name="Podzespoły komputerowe", ImgPath="images\\Categories\\cpu.png" },
                new MainCategory() { MainCategoryId=3, Name="Urządzennia peryferyjne", ImgPath="images\\Categories\\peripherals.png" },
                new MainCategory() { MainCategoryId=4, Name="Smartfony i akcesoria", ImgPath="images\\Categories\\smartphone.png" },
                new MainCategory() { MainCategoryId=5, Name="Akcesoria", ImgPath="images\\Categories\\accessories.png" },
                new MainCategory() { MainCategoryId=6, Name="TV, Smarthome, Lifestyle", ImgPath="images\\Categories\\smarthome.png" },
            };

            mainCategories.ForEach(c => context.MainCategories.AddOrUpdate(c));
            context.SaveChanges();

            var categories = new List<Category>
            {
                new Category() { CategoryId=1, Name="Laptopy", MainCategoryId=1},
                new Category() { CategoryId=2, Name="Komputery", MainCategoryId=1},
                new Category() { CategoryId=3, Name="Procesory", MainCategoryId=2},
                new Category() { CategoryId=4, Name="Karty graficzne", MainCategoryId=2},
                new Category() { CategoryId=5, Name="Pamięci RAM", MainCategoryId=2},
                new Category() { CategoryId=6, Name="Dyski", MainCategoryId=2},
                new Category() { CategoryId=7, Name="Monitory", MainCategoryId=3},
                new Category() { CategoryId=8, Name="Myszki", MainCategoryId=3},
                new Category() { CategoryId=9, Name="Klawiatury", MainCategoryId=3},
                new Category() { CategoryId=10, Name="Słuchawki i mikrofony", MainCategoryId=3},
                new Category() { CategoryId=11, Name="Smartfony", MainCategoryId=4},
                new Category() { CategoryId=12, Name="Etui, szkła i folie do smartfonów", MainCategoryId=4},
                new Category() { CategoryId=13, Name="Akcesoria do smartfonów", MainCategoryId=4},
                new Category() { CategoryId=14, Name="Smartwatche", MainCategoryId=4},
                new Category() { CategoryId=15, Name="Kable i przejściówki", MainCategoryId=5},
                new Category() { CategoryId=16, Name="Zasilanie", MainCategoryId=5},
                new Category() { CategoryId=17, Name="Pamięci Flash", MainCategoryId=5},
                new Category() { CategoryId=18, Name="Telewizory", MainCategoryId=6},
                new Category() { CategoryId=19, Name="Aparaty fotograficzne i kamery", MainCategoryId=6},
                new Category() { CategoryId=20, Name="Drony i akcesoria", MainCategoryId=6},
            };

            categories.ForEach(c => context.Categories.AddOrUpdate(c));
            context.SaveChanges();
        }
    }
}