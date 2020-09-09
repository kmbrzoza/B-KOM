namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLinkName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "LinkName", c => c.String());
            AddColumn("dbo.MainCategories", "LinkName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MainCategories", "LinkName");
            DropColumn("dbo.Categories", "LinkName");
        }
    }
}
