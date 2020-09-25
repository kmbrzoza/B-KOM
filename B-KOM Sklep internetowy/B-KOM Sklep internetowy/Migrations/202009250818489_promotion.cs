namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class promotion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PromotionProducts",
                c => new
                    {
                        PromotionProductId = c.Int(nullable: false, identity: true),
                        PromotionId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        PromotionPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PromotionProductId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Promotions", t => t.PromotionId, cascadeDelete: true)
                .Index(t => t.PromotionId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        PromotionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImgPath = c.String(),
                        Hidden = c.Boolean(nullable: false),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.PromotionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PromotionProducts", "PromotionId", "dbo.Promotions");
            DropForeignKey("dbo.PromotionProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.PromotionProducts", new[] { "ProductId" });
            DropIndex("dbo.PromotionProducts", new[] { "PromotionId" });
            DropTable("dbo.Promotions");
            DropTable("dbo.PromotionProducts");
        }
    }
}
