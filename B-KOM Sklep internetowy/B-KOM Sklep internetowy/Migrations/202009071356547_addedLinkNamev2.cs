namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLinkNamev2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MainCategories", "LinkName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MainCategories", "LinkName", c => c.String());
        }
    }
}
