namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migChangeProductFeatures : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductFeatures", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.ProductFeatures", new[] { "Product_ProductId" });
            DropColumn("dbo.ProductFeatures", "Product_ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductFeatures", "Product_ProductId", c => c.Int());
            CreateIndex("dbo.ProductFeatures", "Product_ProductId");
            AddForeignKey("dbo.ProductFeatures", "Product_ProductId", "dbo.Products", "ProductId");
        }
    }
}
