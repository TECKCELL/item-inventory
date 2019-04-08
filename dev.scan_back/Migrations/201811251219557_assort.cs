namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assort : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.assorts", "categorie", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.assorts", "categorie");
        }
    }
}
