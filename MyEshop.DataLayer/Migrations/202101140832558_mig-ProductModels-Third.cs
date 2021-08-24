namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migProductModelsThird : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductSelectedFeatures",
                c => new
                    {
                        PF_ID = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        FeatureId = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 200),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PF_ID)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductFeatures", t => t.FeatureId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.FeatureId);
            
            CreateTable(
                "dbo.ProductFeatures",
                c => new
                    {
                        FeatureId = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(),
                        FeatureTitle = c.String(nullable: false, maxLength: 150),
                        IsDelete = c.Boolean(nullable: false),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.FeatureId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .ForeignKey("dbo.ProductGroups", t => t.GroupId)
                .Index(t => t.GroupId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductFeatures", "GroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductSelectedFeatures", "FeatureId", "dbo.ProductFeatures");
            DropForeignKey("dbo.ProductFeatures", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductSelectedFeatures", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductFeatures", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductFeatures", new[] { "GroupId" });
            DropIndex("dbo.ProductSelectedFeatures", new[] { "FeatureId" });
            DropIndex("dbo.ProductSelectedFeatures", new[] { "ProductId" });
            DropTable("dbo.ProductFeatures");
            DropTable("dbo.ProductSelectedFeatures");
        }
    }
}
