namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProcurementClass1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Procurements", "LastQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Procurements", "LastQuantity");
        }
    }
}
