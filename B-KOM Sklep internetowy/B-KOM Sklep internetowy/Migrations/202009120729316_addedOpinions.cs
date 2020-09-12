namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedOpinions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Opinions",
                c => new
                    {
                        Opinionid = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserName = c.String(),
                        StarsValue = c.Int(nullable: false),
                        OpinionText = c.String(),
                        Accepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Opinionid)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Opinions", "ProductId", "dbo.Products");
            DropIndex("dbo.Opinions", new[] { "ProductId" });
            DropTable("dbo.Opinions");
        }
    }
}
