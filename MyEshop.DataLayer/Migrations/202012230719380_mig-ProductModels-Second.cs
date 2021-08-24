namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migProductModelsSecond : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductBrands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandTitle = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        BrandId = c.Int(nullable: false),
                        ProductCode = c.Int(nullable: false),
                        ProductTitle = c.String(nullable: false, maxLength: 400),
                        ShortDescription = c.String(nullable: false, maxLength: 500),
                        Text = c.String(nullable: false),
                        ProductImageName = c.String(nullable: false),
                        ProductCount = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        SellCount = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsExist = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductBrands", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.ProductGalleries",
                c => new
                    {
                        GalleryId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ImageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GalleryId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductSelectedGroups",
                c => new
                    {
                        PG_Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductGroupId = c.Int(nullable: false),
                        ProductGroup_GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.PG_Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroup_GroupID)
                .Index(t => t.ProductId)
                .Index(t => t.ProductGroup_GroupID);
            
            CreateTable(
                "dbo.ProductTags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        TagTitle = c.String(nullable: false, maxLength: 70),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTags", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductSelectedGroups", "ProductGroup_GroupID", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductSelectedGroups", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductGalleries", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "BrandId", "dbo.ProductBrands");
            DropIndex("dbo.ProductTags", new[] { "ProductId" });
            DropIndex("dbo.ProductSelectedGroups", new[] { "ProductGroup_GroupID" });
            DropIndex("dbo.ProductSelectedGroups", new[] { "ProductId" });
            DropIndex("dbo.ProductGalleries", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropTable("dbo.ProductTags");
            DropTable("dbo.ProductSelectedGroups");
            DropTable("dbo.ProductGalleries");
            DropTable("dbo.Products");
            DropTable("dbo.ProductBrands");
        }
    }
}
