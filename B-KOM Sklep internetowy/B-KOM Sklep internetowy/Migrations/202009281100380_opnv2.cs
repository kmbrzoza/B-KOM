namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class opnv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Opinions", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Opinions", "DateTime");
        }
    }
}
