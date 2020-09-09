namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedProductImgV3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductImages", "MainImg", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductImages", "DescriptionImg", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductImages", "DescriptionImg");
            DropColumn("dbo.ProductImages", "MainImg");
        }
    }
}
