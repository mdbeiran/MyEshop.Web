namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migAddWebsiteToUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Website", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "Website");
        }
    }
}
