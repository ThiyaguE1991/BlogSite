namespace BloggerPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedby : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBL_User", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.TBL_User", "CreatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.TBL_User", "IsDeleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBL_User", "IsDeleted");
            DropColumn("dbo.TBL_User", "CreatedBy");
            DropColumn("dbo.TBL_User", "DeletedOn");
        }
    }
}
