namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migChangeProductBrand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductBrands", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductBrands", "IsDelete");
        }
    }
}
