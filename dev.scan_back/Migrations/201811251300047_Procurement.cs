namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Procurement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Procurements", "Label", c => c.String());
            AddColumn("dbo.Procurements", "categorie", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Procurements", "categorie");
            DropColumn("dbo.Procurements", "Label");
        }
    }
}
