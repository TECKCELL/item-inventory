namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.assorts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ean13 = c.String(nullable: false),
                        Label = c.String(nullable: false),
                        ImportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Imports", t => t.ImportId, cascadeDelete: true)
                .Index(t => t.ImportId);
            
            CreateTable(
                "dbo.Imports",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SCANS",
                c => new
                    {
                        ImportId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Ean13 = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Imports", t => t.ImportId, cascadeDelete: true)
                .Index(t => t.ImportId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.assorts", "ImportId", "dbo.Imports");
            DropForeignKey("dbo.SCANS", "ImportId", "dbo.Imports");
            DropForeignKey("dbo.Imports", "ClientId", "dbo.Clients");
            DropIndex("dbo.SCANS", new[] { "ImportId" });
            DropIndex("dbo.Imports", new[] { "ClientId" });
            DropIndex("dbo.assorts", new[] { "ImportId" });
            DropTable("dbo.SCANS");
            DropTable("dbo.Clients");
            DropTable("dbo.Imports");
            DropTable("dbo.assorts");
        }
    }
}
