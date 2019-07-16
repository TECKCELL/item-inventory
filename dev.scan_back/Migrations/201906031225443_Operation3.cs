namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Operation3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Operations", "endDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Operations", "endDate");
        }
    }
}
