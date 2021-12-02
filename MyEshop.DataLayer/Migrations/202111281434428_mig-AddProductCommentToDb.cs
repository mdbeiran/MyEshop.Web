namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migAddProductCommentToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductComments",
                c => new
                    {
                        ProductCommentId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 500),
                        CreateDate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductCommentId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductComments", t => t.ParentId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.UserId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProductComments", "ParentId", "dbo.ProductComments");
            DropForeignKey("dbo.ProductComments", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductComments", new[] { "ParentId" });
            DropIndex("dbo.ProductComments", new[] { "UserId" });
            DropIndex("dbo.ProductComments", new[] { "ProductId" });
            DropTable("dbo.ProductComments");
        }
    }
}
