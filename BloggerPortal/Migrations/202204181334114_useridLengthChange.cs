namespace BloggerPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useridLengthChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBL_Blog", "CreatedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Blog", "DeletedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Blog", "ModifiedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Comment", "CreatedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Comment", "DeletedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Comment", "ModifiedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_MediaFile", "CreatedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_MediaFile", "DeletedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_MediaFile", "ModifiedBy", "dbo.TBL_User");
            DropIndex("dbo.TBL_Blog", new[] { "CreatedBy" });
            DropIndex("dbo.TBL_Blog", new[] { "ModifiedBy" });
            DropIndex("dbo.TBL_Blog", new[] { "DeletedBy" });
            DropIndex("dbo.TBL_Comment", new[] { "CreatedBy" });
            DropIndex("dbo.TBL_Comment", new[] { "ModifiedBy" });
            DropIndex("dbo.TBL_Comment", new[] { "DeletedBy" });
            DropIndex("dbo.TBL_MediaFile", new[] { "CreatedBy" });
            DropIndex("dbo.TBL_MediaFile", new[] { "ModifiedBy" });
            DropIndex("dbo.TBL_MediaFile", new[] { "DeletedBy" });
            DropPrimaryKey("dbo.TBL_User");
            AlterColumn("dbo.TBL_Blog", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.TBL_Blog", "ModifiedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.TBL_Blog", "DeletedBy", c => c.Int());
            AlterColumn("dbo.TBL_Comment", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.TBL_Comment", "ModifiedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.TBL_Comment", "DeletedBy", c => c.Int());
            AlterColumn("dbo.TBL_User", "UserId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.TBL_MediaFile", "CreatedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.TBL_MediaFile", "ModifiedBy", c => c.Int());
            AlterColumn("dbo.TBL_MediaFile", "DeletedBy", c => c.Int());
            AddPrimaryKey("dbo.TBL_User", "UserId");
            CreateIndex("dbo.TBL_Blog", "CreatedBy");
            CreateIndex("dbo.TBL_Blog", "ModifiedBy");
            CreateIndex("dbo.TBL_Blog", "DeletedBy");
            CreateIndex("dbo.TBL_Comment", "CreatedBy");
            CreateIndex("dbo.TBL_Comment", "ModifiedBy");
            CreateIndex("dbo.TBL_Comment", "DeletedBy");
            CreateIndex("dbo.TBL_MediaFile", "CreatedBy");
            CreateIndex("dbo.TBL_MediaFile", "ModifiedBy");
            CreateIndex("dbo.TBL_MediaFile", "DeletedBy");
            AddForeignKey("dbo.TBL_Blog", "CreatedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_Blog", "DeletedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_Blog", "ModifiedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_Comment", "CreatedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_Comment", "DeletedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_Comment", "ModifiedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_MediaFile", "CreatedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_MediaFile", "DeletedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_MediaFile", "ModifiedBy", "dbo.TBL_User", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBL_MediaFile", "ModifiedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_MediaFile", "DeletedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_MediaFile", "CreatedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Comment", "ModifiedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Comment", "DeletedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Comment", "CreatedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Blog", "ModifiedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Blog", "DeletedBy", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Blog", "CreatedBy", "dbo.TBL_User");
            DropIndex("dbo.TBL_MediaFile", new[] { "DeletedBy" });
            DropIndex("dbo.TBL_MediaFile", new[] { "ModifiedBy" });
            DropIndex("dbo.TBL_MediaFile", new[] { "CreatedBy" });
            DropIndex("dbo.TBL_Comment", new[] { "DeletedBy" });
            DropIndex("dbo.TBL_Comment", new[] { "ModifiedBy" });
            DropIndex("dbo.TBL_Comment", new[] { "CreatedBy" });
            DropIndex("dbo.TBL_Blog", new[] { "DeletedBy" });
            DropIndex("dbo.TBL_Blog", new[] { "ModifiedBy" });
            DropIndex("dbo.TBL_Blog", new[] { "CreatedBy" });
            DropPrimaryKey("dbo.TBL_User");
            AlterColumn("dbo.TBL_MediaFile", "DeletedBy", c => c.Byte());
            AlterColumn("dbo.TBL_MediaFile", "ModifiedBy", c => c.Byte());
            AlterColumn("dbo.TBL_MediaFile", "CreatedBy", c => c.Byte(nullable: false));
            AlterColumn("dbo.TBL_User", "UserId", c => c.Byte(nullable: false, identity: true));
            AlterColumn("dbo.TBL_Comment", "DeletedBy", c => c.Byte());
            AlterColumn("dbo.TBL_Comment", "ModifiedBy", c => c.Byte(nullable: false));
            AlterColumn("dbo.TBL_Comment", "CreatedBy", c => c.Byte(nullable: false));
            AlterColumn("dbo.TBL_Blog", "DeletedBy", c => c.Byte());
            AlterColumn("dbo.TBL_Blog", "ModifiedBy", c => c.Byte(nullable: false));
            AlterColumn("dbo.TBL_Blog", "CreatedBy", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.TBL_User", "UserId");
            CreateIndex("dbo.TBL_MediaFile", "DeletedBy");
            CreateIndex("dbo.TBL_MediaFile", "ModifiedBy");
            CreateIndex("dbo.TBL_MediaFile", "CreatedBy");
            CreateIndex("dbo.TBL_Comment", "DeletedBy");
            CreateIndex("dbo.TBL_Comment", "ModifiedBy");
            CreateIndex("dbo.TBL_Comment", "CreatedBy");
            CreateIndex("dbo.TBL_Blog", "DeletedBy");
            CreateIndex("dbo.TBL_Blog", "ModifiedBy");
            CreateIndex("dbo.TBL_Blog", "CreatedBy");
            AddForeignKey("dbo.TBL_MediaFile", "ModifiedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_MediaFile", "DeletedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_MediaFile", "CreatedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_Comment", "ModifiedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_Comment", "DeletedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_Comment", "CreatedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_Blog", "ModifiedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_Blog", "DeletedBy", "dbo.TBL_User", "UserId");
            AddForeignKey("dbo.TBL_Blog", "CreatedBy", "dbo.TBL_User", "UserId");
        }
    }
}
