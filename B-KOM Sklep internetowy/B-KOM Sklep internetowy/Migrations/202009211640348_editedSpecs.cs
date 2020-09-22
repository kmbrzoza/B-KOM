namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedSpecs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductSpecifications", "Value", c => c.String(nullable: false));
            AlterColumn("dbo.Specifications", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Specifications", "Name", c => c.String());
            AlterColumn("dbo.ProductSpecifications", "Value", c => c.String());
        }
    }
}
