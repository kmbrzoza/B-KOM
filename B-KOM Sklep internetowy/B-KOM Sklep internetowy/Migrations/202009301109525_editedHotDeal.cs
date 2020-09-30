namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedHotDeal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PromotionHotDeals", "AmountLeft", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PromotionHotDeals", "AmountLeft");
        }
    }
}
