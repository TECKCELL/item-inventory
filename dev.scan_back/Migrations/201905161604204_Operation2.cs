namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Operation2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductFiles", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductFiles", "Description");
        }
    }
}
