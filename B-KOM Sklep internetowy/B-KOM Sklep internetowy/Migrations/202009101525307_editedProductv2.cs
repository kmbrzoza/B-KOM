namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedProductv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShortDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ShortDescription");
        }
    }
}
