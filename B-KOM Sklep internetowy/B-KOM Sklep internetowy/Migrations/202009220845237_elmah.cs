namespace B_KOM_Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elmah : DbMigration
    {
        public override void Up()
        {
            SqlFile("../B-KOM Sklep internetowy/Migrations/ELMAH-1.2-db-SQLServer.sql");
        }
        
        public override void Down()
        {
        }
    }
}
