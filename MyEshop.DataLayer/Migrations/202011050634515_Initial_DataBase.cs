namespace MyEshop.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_DataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        PermissionId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        Name = c.String(nullable: false, maxLength: 250),
                        ParentId = c.Int(),
                        DisplayPriority = c.Int(),
                    })
                .PrimaryKey(t => t.PermissionId)
                .ForeignKey("dbo.Permissions", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        RolePermissionId = c.Int(nullable: false, identity: true),
                        UserRoleId = c.Int(nullable: false),
                        Permission = c.Int(nullable: false),
                        UserPermission_PermissionId = c.Int(),
                        UserRole_RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.RolePermissionId)
                .ForeignKey("dbo.Permissions", t => t.UserPermission_PermissionId)
                .ForeignKey("dbo.UserRoles", t => t.UserRole_RoleId)
                .Index(t => t.UserPermission_PermissionId)
                .Index(t => t.UserRole_RoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        RoleTitle = c.String(nullable: false, maxLength: 250),
                        RoleName = c.String(nullable: false, maxLength: 150),
                        IsDefaultRole = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 250),
                        Password = c.String(nullable: false, maxLength: 200),
                        ActiveCode = c.String(nullable: false, maxLength: 50),
                        RegisterDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        ViewByAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Sex = c.String(maxLength: 20),
                        Address = c.String(maxLength: 500),
                        FullName = c.String(maxLength: 250),
                        Mobile = c.String(maxLength: 20),
                        PhoneNumber = c.String(maxLength: 20),
                        NationalCode = c.String(maxLength: 20),
                        BirthDate = c.DateTime(nullable: false),
                        Bio = c.String(maxLength: 100),
                        CardNumber = c.String(maxLength: 16),
                        UserImageName = c.String(maxLength: 200),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserProfiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.RolePermissions", "UserRole_RoleId", "dbo.UserRoles");
            DropForeignKey("dbo.RolePermissions", "UserPermission_PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.Permissions", "ParentId", "dbo.Permissions");
            DropIndex("dbo.UserProfiles", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.RolePermissions", new[] { "UserRole_RoleId" });
            DropIndex("dbo.RolePermissions", new[] { "UserPermission_PermissionId" });
            DropIndex("dbo.Permissions", new[] { "ParentId" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Permissions");
        }
    }
}
