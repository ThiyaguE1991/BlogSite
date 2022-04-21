namespace BloggerPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class descriptionupdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TBL_Blog", "Description", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBL_Blog", "Description", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
