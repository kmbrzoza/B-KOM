namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedOrder : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Orders", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            AddColumn("dbo.Orders", "Address", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "UserData_City", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "UserData_PostCode", c => c.String(maxLength: 6));
            AlterColumn("dbo.Orders", "Phone", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.AspNetUsers", "UserData_FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "UserData_LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "UserData_Address", c => c.String(maxLength: 100));
            DropColumn("dbo.Orders", "Street");
            DropColumn("dbo.AspNetUsers", "UserData_Town");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserData_Town", c => c.String());
            AddColumn("dbo.Orders", "Street", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "UserData_Address", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "UserData_FirstName", c => c.String());
            AlterColumn("dbo.Orders", "Phone", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "UserData_PostCode");
            DropColumn("dbo.AspNetUsers", "UserData_City");
            DropColumn("dbo.Orders", "Address");
            RenameIndex(table: "dbo.Orders", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Orders", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
