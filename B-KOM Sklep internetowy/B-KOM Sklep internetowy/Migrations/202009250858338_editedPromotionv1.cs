namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedPromotionv1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Promotions", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Promotions", "Code", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Promotions", "Code", c => c.String());
            AlterColumn("dbo.Promotions", "Name", c => c.String());
        }
    }
}
