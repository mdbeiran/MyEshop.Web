namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migChangeProductGallerySecond : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductGalleries", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductGalleries", "ImageName", c => c.String(nullable: false));
        }
    }
}
