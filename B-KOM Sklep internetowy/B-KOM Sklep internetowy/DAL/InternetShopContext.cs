using B_KOM_Sklep_internetowy.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.DAL
{
    public class InternetShopContext: IdentityDbContext<ApplicationUser>
    {
        public InternetShopContext():base("InternetShopContext")
        {

        }
        static InternetShopContext()
        {
            Database.SetInitializer<InternetShopContext>(new DatabaseInitializer());
        }

        public static InternetShopContext Create()
        {
            return new InternetShopContext();
        }

        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionProduct> PromotionProducts { get; set; }
    }
}