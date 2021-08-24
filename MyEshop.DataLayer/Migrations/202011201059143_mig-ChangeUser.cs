namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migChangeUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "ActiveCode", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "ActiveCode", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
