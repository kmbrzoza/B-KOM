namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedCats : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "LinkName", c => c.String(nullable: false));
            AlterColumn("dbo.MainCategories", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MainCategories", "Name", c => c.String());
            AlterColumn("dbo.Categories", "LinkName", c => c.String());
        }
    }
}
