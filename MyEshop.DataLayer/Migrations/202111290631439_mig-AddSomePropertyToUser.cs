namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migAddSomePropertyToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FullName", c => c.String(maxLength: 250));
            AddColumn("dbo.Users", "UserImageName", c => c.String(maxLength: 200));
            AddColumn("dbo.Users", "Website", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Website");
            DropColumn("dbo.Users", "UserImageName");
            DropColumn("dbo.Users", "FullName");
        }
    }
}
