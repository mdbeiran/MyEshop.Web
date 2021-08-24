namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migChangeProductGalleryThird : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductGalleries", "ImageName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductGalleries", "ImageName", c => c.String());
        }
    }
}
