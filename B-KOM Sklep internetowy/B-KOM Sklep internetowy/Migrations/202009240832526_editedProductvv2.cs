namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedProductvv2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ImgPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImgPath", c => c.String());
        }
    }
}
