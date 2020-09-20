namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrdersv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserData_Phone", c => c.String(maxLength: 20));
            DropColumn("dbo.AspNetUsers", "UserData_PhoneNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserData_PhoneNumber", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserData_Phone");
        }
    }
}
