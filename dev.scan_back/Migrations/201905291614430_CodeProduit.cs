namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeProduit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductFiles", "CodeProduit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductFiles", "CodeProduit");
        }
    }
}
