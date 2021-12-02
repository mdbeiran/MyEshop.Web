namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migDeleteRelationBetweenUserAndUserProfile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfiles", "UserId", "dbo.Users");
            DropIndex("dbo.UserProfiles", new[] { "UserId" });
            DropPrimaryKey("dbo.UserProfiles");
            AlterColumn("dbo.UserProfiles", "UserId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserProfiles", "UserId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserProfiles");
            AlterColumn("dbo.UserProfiles", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserProfiles", "UserId");
            CreateIndex("dbo.UserProfiles", "UserId");
            AddForeignKey("dbo.UserProfiles", "UserId", "dbo.Users", "UserId");
        }
    }
}
