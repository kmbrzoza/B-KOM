namespace B_KOM_Sklep_internetowy.Migrations
{
    using B_KOM_Sklep_internetowy.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<B_KOM_Sklep_internetowy.DAL.InternetShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "B_KOM_Sklep_internetowy.DAL.InternetShopContext";
        }

        protected override void Seed(B_KOM_Sklep_internetowy.DAL.InternetShopContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            DatabaseInitializer.SeedDatabase(context);
            DatabaseInitializer.SeedUsers(context);
        }
    }
}
