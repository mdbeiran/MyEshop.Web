namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migChangeProductGroupSecond : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductGroups", "ParentID");
            RenameColumn(table: "dbo.ProductGroups", name: "ProductGroup_GroupID", newName: "ParentID");
            RenameIndex(table: "dbo.ProductGroups", name: "IX_ProductGroup_GroupID", newName: "IX_ParentID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProductGroups", name: "IX_ParentID", newName: "IX_ProductGroup_GroupID");
            RenameColumn(table: "dbo.ProductGroups", name: "ParentID", newName: "ProductGroup_GroupID");
            AddColumn("dbo.ProductGroups", "ParentID", c => c.Int());
        }
    }
}
