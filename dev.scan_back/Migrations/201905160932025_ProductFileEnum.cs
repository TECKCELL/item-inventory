namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductFileEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductFiles", "Immobilisation", c => c.Int(nullable: false));
            AddColumn("dbo.ProductFiles", "Amortissement", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductFiles", "Amortissement");
            DropColumn("dbo.ProductFiles", "Immobilisation");
        }
    }
}
