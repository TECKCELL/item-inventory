namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Operation1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductFiles", "Supplier", c => c.String());
            AddColumn("dbo.ProductFiles", "Tva", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ProductFiles", "ValBuy", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ProductFiles", "ValBrutImmo");
            DropColumn("dbo.ProductFiles", "ValBrutElOut");
            DropColumn("dbo.ProductFiles", "CumulOpenExer");
            DropColumn("dbo.ProductFiles", "ExcerDotation");
            DropColumn("dbo.ProductFiles", "ValTotal");
            DropColumn("dbo.ProductFiles", "RelativeAmount");
            DropColumn("dbo.ProductFiles", "CumulAmort");
            DropColumn("dbo.ProductFiles", "ValResid");
            DropColumn("dbo.ProductFiles", "ValElemOut");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductFiles", "ValElemOut", c => c.Long(nullable: false));
            AddColumn("dbo.ProductFiles", "ValResid", c => c.Long(nullable: false));
            AddColumn("dbo.ProductFiles", "CumulAmort", c => c.Long(nullable: false));
            AddColumn("dbo.ProductFiles", "RelativeAmount", c => c.Long(nullable: false));
            AddColumn("dbo.ProductFiles", "ValTotal", c => c.Long(nullable: false));
            AddColumn("dbo.ProductFiles", "ExcerDotation", c => c.Long(nullable: false));
            AddColumn("dbo.ProductFiles", "CumulOpenExer", c => c.Long(nullable: false));
            AddColumn("dbo.ProductFiles", "ValBrutElOut", c => c.Long(nullable: false));
            AddColumn("dbo.ProductFiles", "ValBrutImmo", c => c.Long(nullable: false));
            DropColumn("dbo.ProductFiles", "ValBuy");
            DropColumn("dbo.ProductFiles", "Tva");
            DropColumn("dbo.ProductFiles", "Supplier");
        }
    }
}
