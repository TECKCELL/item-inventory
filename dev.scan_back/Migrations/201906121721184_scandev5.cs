namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scandev5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OperationViewModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Long(nullable: false),
                        beginDate = c.DateTime(nullable: false),
                        endDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OperationViewModels");
        }
    }
}
