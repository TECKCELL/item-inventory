namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProcurementClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Procurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ean13 = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.assorts", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.assorts", "Price");
            DropTable("dbo.Procurements");
        }
    }
}
