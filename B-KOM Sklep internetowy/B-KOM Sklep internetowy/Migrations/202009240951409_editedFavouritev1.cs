namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedFavouritev1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Favourites", new[] { "User_Id" });
            DropColumn("dbo.Favourites", "UserId");
            RenameColumn(table: "dbo.Favourites", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Favourites", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Favourites", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Favourites", new[] { "UserId" });
            AlterColumn("dbo.Favourites", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Favourites", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Favourites", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Favourites", "User_Id");
        }
    }
}
