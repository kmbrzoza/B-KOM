using B_KOM_Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.DAL
{
    public class InternetShopContext: DbContext
    {
        public InternetShopContext():base("InternetShopContext")
        {

        }
        static InternetShopContext()
        {
            Database.SetInitializer<InternetShopContext>(new DatabaseInitializer());
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
    }
}