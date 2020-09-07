namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        MainCategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.MainCategories", t => t.MainCategoryId, cascadeDelete: true)
                .Index(t => t.MainCategoryId);
            
            CreateTable(
                "dbo.MainCategories",
                c => new
                    {
                        MainCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImgPath = c.String(),
                    })
                .PrimaryKey(t => t.MainCategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImgPath = c.String(),
                        AddDate = c.DateTime(nullable: false),
                        Hidden = c.Boolean(nullable: false),
                        Recommended = c.Boolean(nullable: false),
                        Bestseller = c.Boolean(nullable: false),
                        Promo = c.Boolean(nullable: false),
                        PromoPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductSpecifications",
                c => new
                    {
                        ProductSpecificationId = c.Int(nullable: false, identity: true),
                        SpecificationId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ProductSpecificationId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Specifications", t => t.SpecificationId, cascadeDelete: true)
                .Index(t => t.SpecificationId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        SpecificationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SpecificationId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Street = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 100),
                        PostCode = c.String(nullable: false, maxLength: 6),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Comment = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        OrderValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.ProductSpecifications", "SpecificationId", "dbo.Specifications");
            DropForeignKey("dbo.ProductSpecifications", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "MainCategoryId", "dbo.MainCategories");
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.ProductSpecifications", new[] { "ProductId" });
            DropIndex("dbo.ProductSpecifications", new[] { "SpecificationId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "MainCategoryId" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Specifications");
            DropTable("dbo.ProductSpecifications");
            DropTable("dbo.Products");
            DropTable("dbo.MainCategories");
            DropTable("dbo.Categories");
        }
    }
}
