namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migChangeProductGroup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductGroups", "ParentID", "dbo.ProductGroups");
            DropIndex("dbo.ProductGroups", new[] { "ParentID" });
            AddColumn("dbo.ProductGroups", "ProductGroup_GroupID", c => c.Int());
            CreateIndex("dbo.ProductGroups", "ProductGroup_GroupID");
            AddForeignKey("dbo.ProductGroups", "ProductGroup_GroupID", "dbo.ProductGroups", "GroupID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductGroups", "ProductGroup_GroupID", "dbo.ProductGroups");
            DropIndex("dbo.ProductGroups", new[] { "ProductGroup_GroupID" });
            DropColumn("dbo.ProductGroups", "ProductGroup_GroupID");
            CreateIndex("dbo.ProductGroups", "ParentID");
            AddForeignKey("dbo.ProductGroups", "ParentID", "dbo.ProductGroups", "GroupID");
        }
    }
}
