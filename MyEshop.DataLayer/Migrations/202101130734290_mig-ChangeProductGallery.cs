namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migChangeProductGallery : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductGalleries", "ImageTitle", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductGalleries", "ImageTitle");
        }
    }
}
