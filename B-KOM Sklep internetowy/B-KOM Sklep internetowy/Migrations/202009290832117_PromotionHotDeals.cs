namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PromotionHotDeals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PromotionHotDeals",
                c => new
                    {
                        PromotionHotDealId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        PromotionPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Code = c.String(nullable: false, maxLength: 50),
                        Amount = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PromotionHotDealId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PromotionHotDeals", "ProductId", "dbo.Products");
            DropIndex("dbo.PromotionHotDeals", new[] { "ProductId" });
            DropTable("dbo.PromotionHotDeals");
        }
    }
}
