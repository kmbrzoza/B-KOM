namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedProduct1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductImages", "DescriptionImg");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductImages", "DescriptionImg", c => c.Boolean(nullable: false));
        }
    }
}
