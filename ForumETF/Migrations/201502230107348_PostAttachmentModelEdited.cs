namespace ForumETF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostAttachmentModelEdited : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Title", c => c.String());
        }
    }
}
