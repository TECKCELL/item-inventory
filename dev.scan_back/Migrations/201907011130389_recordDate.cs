namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recordDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Operations", "RecordDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Operations", "RecordDate");
        }
    }
}
