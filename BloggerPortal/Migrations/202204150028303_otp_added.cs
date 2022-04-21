namespace BloggerPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class otp_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBL_Comment", "ReplyCommentId", c => c.Int(nullable: false));
            AddColumn("dbo.TBL_User", "OTP", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBL_User", "OTP");
            DropColumn("dbo.TBL_Comment", "ReplyCommentId");
        }
    }
}
