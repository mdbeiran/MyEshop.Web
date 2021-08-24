namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migProductModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupTitle = c.String(nullable: false, maxLength: 200),
                        ParentID = c.Int(),
                        NameInUrl = c.String(nullable: false, maxLength: 200),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.ProductGroups", t => t.ParentID)
                .Index(t => t.ParentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductGroups", "ParentID", "dbo.ProductGroups");
            DropIndex("dbo.ProductGroups", new[] { "ParentID" });
            DropTable("dbo.ProductGroups");
        }
    }
}
