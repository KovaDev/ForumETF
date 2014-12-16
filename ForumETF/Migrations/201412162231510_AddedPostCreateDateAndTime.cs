namespace ForumETF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPostCreateDateAndTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "CreatedAt");
        }
    }
}
