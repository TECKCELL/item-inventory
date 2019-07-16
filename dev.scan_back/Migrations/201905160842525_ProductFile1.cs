namespace dev.scan_back.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductFile1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductFiles", "CLabelImmo", c => c.String());
            AddColumn("dbo.ProductFiles", "MotifOut", c => c.String());
            AddColumn("dbo.ProductFiles", "Localisation", c => c.String());
            AddColumn("dbo.ProductFiles", "Familly", c => c.String());
            AddColumn("dbo.ProductFiles", "SFamilly", c => c.String());
            AddColumn("dbo.ProductFiles", "OutDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductFiles", "PriceSes", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ProductFiles", "Coeff", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ProductFiles", "NumInvent", c => c.Long(nullable: false));
            AlterColumn("dbo.ProductFiles", "AccountNumber", c => c.Long(nullable: false));
            AlterColumn("dbo.ProductFiles", "NumFiche", c => c.Long(nullable: false));
            DropColumn("dbo.ProductFiles", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductFiles", "Code", c => c.String());
            AlterColumn("dbo.ProductFiles", "NumFiche", c => c.String());
            AlterColumn("dbo.ProductFiles", "AccountNumber", c => c.Int(nullable: false));
            DropColumn("dbo.ProductFiles", "NumInvent");
            DropColumn("dbo.ProductFiles", "Coeff");
            DropColumn("dbo.ProductFiles", "PriceSes");
            DropColumn("dbo.ProductFiles", "OutDate");
            DropColumn("dbo.ProductFiles", "SFamilly");
            DropColumn("dbo.ProductFiles", "Familly");
            DropColumn("dbo.ProductFiles", "Localisation");
            DropColumn("dbo.ProductFiles", "MotifOut");
            DropColumn("dbo.ProductFiles", "CLabelImmo");
        }
    }
}
