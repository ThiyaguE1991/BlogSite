namespace BloggerPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBL_Blog",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 200),
                        MediaType = c.Byte(nullable: false),
                        MediaFileId = c.Int(nullable: false),
                        PublishedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsVisible = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Byte(nullable: false),
                        ModifiedBy = c.Byte(nullable: false),
                        DeletedBy = c.Byte(),
                        IsDeleted = c.Boolean(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.TBL_User", t => t.CreatedBy)
                .ForeignKey("dbo.TBL_User", t => t.DeletedBy)
                .ForeignKey("dbo.TBL_User", t => t.ModifiedBy)
                .ForeignKey("dbo.TBL_MediaFile", t => t.MediaFileId)
                .Index(t => t.MediaFileId)
                .Index(t => t.CreatedBy)
                .Index(t => t.ModifiedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.TBL_Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false),
                        BlogId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeletedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Byte(nullable: false),
                        ModifiedBy = c.Byte(nullable: false),
                        DeletedBy = c.Byte(),
                        IsDeleted = c.Boolean(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.TBL_User", t => t.CreatedBy)
                .ForeignKey("dbo.TBL_User", t => t.DeletedBy)
                .ForeignKey("dbo.TBL_User", t => t.ModifiedBy)
                .ForeignKey("dbo.TBL_Blog", t => t.BlogId)
                .Index(t => t.BlogId)
                .Index(t => t.CreatedBy)
                .Index(t => t.ModifiedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.TBL_User",
                c => new
                    {
                        UserId = c.Byte(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 200),
                        Password = c.String(nullable: false, maxLength: 200),
                        EmailId = c.String(maxLength: 200),
                        MobileNo = c.String(maxLength: 20),
                        Address = c.String(maxLength: 1000),
                        City = c.String(maxLength: 200),
                        RoleId = c.Byte(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.TBL_Role", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TBL_MediaFile",
                c => new
                    {
                        MediaFileId = c.Int(nullable: false, identity: true),
                        MediaFileName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Extension = c.String(nullable: false, maxLength: 50, unicode: false),
                        FileSize = c.String(nullable: false, maxLength: 50, unicode: false),
                        Width = c.Int(),
                        Height = c.Int(),
                        MimeType = c.String(nullable: false, maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Byte(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedBy = c.Byte(),
                        IsDeleted = c.Boolean(),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedBy = c.Byte(),
                    })
                .PrimaryKey(t => t.MediaFileId)
                .ForeignKey("dbo.TBL_User", t => t.CreatedBy)
                .ForeignKey("dbo.TBL_User", t => t.DeletedBy)
                .ForeignKey("dbo.TBL_User", t => t.ModifiedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.ModifiedBy)
                .Index(t => t.DeletedBy);
            
            CreateTable(
                "dbo.TBL_Role",
                c => new
                    {
                        RoleId = c.Byte(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBL_Comment", "BlogId", "dbo.TBL_Blog");
            DropForeignKey("dbo.TBL_User", "RoleId", "dbo.TBL_Role");
            DropForeignKey("dbo.TBL_MediaFile", "ModifiedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_MediaFile", "DeletedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_MediaFile", "CreatedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Blog", "MediaFileId", "dbo.TBL_MediaFile");
            DropForeignKey("dbo.TBL_Comment", "ModifiedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Comment", "DeletedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Comment", "CreatedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Blog", "ModifiedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Blog", "DeletedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Blog", "CreatedBy", "dbo.TBL_User");
            DropIndex("dbo.TBL_MediaFile", new[] { "DeletedBy" });
            DropIndex("dbo.TBL_MediaFile", new[] { "ModifiedBy" });
            DropIndex("dbo.TBL_MediaFile", new[] { "CreatedBy" });
            DropIndex("dbo.TBL_User", new[] { "RoleId" });
            DropIndex("dbo.TBL_Comment", new[] { "DeletedBy" });
            DropIndex("dbo.TBL_Comment", new[] { "ModifiedBy" });
            DropIndex("dbo.TBL_Comment", new[] { "CreatedBy" });
            DropIndex("dbo.TBL_Comment", new[] { "BlogId" });
            DropIndex("dbo.TBL_Blog", new[] { "DeletedBy" });
            DropIndex("dbo.TBL_Blog", new[] { "ModifiedBy" });
            DropIndex("dbo.TBL_Blog", new[] { "CreatedBy" });
            DropIndex("dbo.TBL_Blog", new[] { "MediaFileId" });
            DropTable("dbo.TBL_Role");
            DropTable("dbo.TBL_MediaFile");
            DropTable("dbo.TBL_User");
            DropTable("dbo.TBL_Comment");
            DropTable("dbo.TBL_Blog");
        }
    }
}
