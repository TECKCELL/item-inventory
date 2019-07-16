namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Operation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Number = c.Long(nullable: false),
                        beginDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ProductFiles", "OperationId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ProductFiles", "OperationId");
            AddForeignKey("dbo.ProductFiles", "OperationId", "dbo.Operations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductFiles", "OperationId", "dbo.Operations");
            DropIndex("dbo.ProductFiles", new[] { "OperationId" });
            DropColumn("dbo.ProductFiles", "OperationId");
            DropTable("dbo.Operations");
        }
    }
}
