namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductFile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductFiles",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AccountNumber = c.Int(nullable: false),
                        RefImmo = c.String(),
                        Code = c.String(),
                        NumFiche = c.String(),
                        LabelImmo = c.String(),
                        Matricule = c.String(),
                        AcquiDate = c.DateTime(nullable: false),
                        ServiceDate = c.DateTime(nullable: false),
                        Amort = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValBrutImmo = c.Long(nullable: false),
                        ValBrutElOut = c.Long(nullable: false),
                        CumulOpenExer = c.Long(nullable: false),
                        ExcerDotation = c.Long(nullable: false),
                        ValTotal = c.Long(nullable: false),
                        RelativeAmount = c.Long(nullable: false),
                        CumulAmort = c.Long(nullable: false),
                        ValResid = c.Long(nullable: false),
                        ValElemOut = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductFiles");
        }
    }
}
