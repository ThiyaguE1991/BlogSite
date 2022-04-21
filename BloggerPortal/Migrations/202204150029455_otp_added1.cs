namespace BloggerPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class otp_added1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBL_Comment", "IsReply", c => c.Boolean());
            AlterColumn("dbo.TBL_Comment", "ReplyCommentId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBL_Comment", "ReplyCommentId", c => c.Int(nullable: false));
            DropColumn("dbo.TBL_Comment", "IsReply");
        }
    }
}
